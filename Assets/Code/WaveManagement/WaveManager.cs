using Assets.Code.WaveManagement;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Wave CurrentWave { get; set; }
    public int currentAlive;
    private bool wavePaused = false;
    public AudioSource audioSource = null;
    private bool isZombieSpawnTriggered = false;
    public int currentMusic;
    public AudioClip[] waveMusic;


    // Start is called before the first frame update
    #region Singleton
    private WaveManager()
    {

    }
    public static WaveManager Instance;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Instance = this;
        currentMusic = 0;
        waveMusic = Resources.LoadAll<AudioClip>("SoundTracks");
    }
    #endregion

    public IEnumerator InitializeWave(int i)
    {
        Debug.Log("Initializing Wave " + i);
        wavePaused = true;
        yield return new WaitForSeconds(2);

        Game.UI.TriggerAnnouncement("Wave " + i);
        switch (i)
        {
            case 1:
                CurrentWave = new WaveOne();
                break;
            case 2:
                CurrentWave = new WaveTwo();
                break;
            case 3:
                CurrentWave = new WaveThree();
                break;
            default:
                GameOver();
                yield break;
        }
        if (CurrentWave.NEXTTRACK)
        {
            audioSource.clip = waveMusic[currentMusic];
            audioSource.Play();
            currentMusic++;
        }
        yield return new WaitForSeconds(10);
        currentAlive = CurrentWave.GetZombieCount();
        CurrentWave.SpawnWave();
        wavePaused = false;
    }
    private void GameOver()
    {
        if(Game.PLAYER.healthPoints <= 0)
        {
            Game.UI.TriggerAnnouncement("You've failed :c");
        }
        else
        {
            Game.UI.TriggerAnnouncement("\\o/ YOU WIN \\o/");
        }
        wavePaused = true;
        Debug.Log("GAME OVER");
    }
    private IEnumerator WaitForZombieSpawn()
    {
        yield return new WaitForSeconds(4);
        CurrentWave.SpawnWave();
        isZombieSpawnTriggered = false;
        yield break;
    }
    public void NotifyDeath()
    {
        if (wavePaused)
            return;

        currentAlive--;
        if (currentAlive <= 0)
        {
            Debug.Log("Wave Complete");
            int nextSequence = CurrentWave.GetSequence() + 1;
            StartCoroutine(InitializeWave(nextSequence));
        }
        else
        {
            if (!isZombieSpawnTriggered)
            {
                isZombieSpawnTriggered = true;
                StartCoroutine(WaitForZombieSpawn());
            }
        }
    }
}
