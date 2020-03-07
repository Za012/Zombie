using Assets.Code.WaveManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Wave CurrentWave { get; set; }
    public int currentAlive;
    private bool wavePaused = false;

    // Start is called before the first frame update
    #region Singleton
    private WaveManager()
    {

    }
    public static WaveManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    public IEnumerator InitializeWave(int i)
    {
        wavePaused = true;
        yield return new WaitForSeconds(5);
        Debug.Log("Initializing Wave " + i);
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
                break;
        }
        currentAlive = CurrentWave.GetZombieCount();
        CurrentWave.SpawnWave();
        wavePaused = false;
        yield break;
    }
    private void GameOver()
    {
        wavePaused = true;
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("GAME OVER");
        }
    }
    private bool isZombieSpawnTriggered = false;
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
            int nextSequence = CurrentWave.GetSequence()+1;
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
