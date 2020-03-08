using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInit : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(LoadGame());
    }
    /*    private void Start()
        {
            //InitGame();
        }*/
    private IEnumerator LoadGame()
    {
        Debug.Log("Loading...");
        SceneManager.LoadScene(
            "Level_Street", LoadSceneMode.Additive);
        SceneManager.LoadScene(
            "Level_Items", LoadSceneMode.Additive);
        SceneManager.LoadScene(
            "Level_Player", LoadSceneMode.Additive);
        SceneManager.LoadScene(
            "Level_UI", LoadSceneMode.Additive);
        yield return null;
        Debug.Log("Loaded.");
        InitGame();
    }

    public void InitGame()
    {
        Debug.Log("Game init");


        // Player_street.
        Scene s = SceneManager.GetSceneByName("Level_Player");
        SceneManager.SetActiveScene(s);
        foreach (GameObject o in s.GetRootGameObjects())
        {
            Player p = o.GetComponent<Player>();
            o.SetActive(true);
            if (p != null)
            {
                Game.PLAYER = p;
                break;
            }
        }

        // Find zombie and assign to Game
        s = SceneManager.GetSceneByName("Level_Items");
        SceneManager.SetActiveScene(s);
        Weapon w = GameObject.Find("Rifle").GetComponent<Weapon>();
        ObjectPool pool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        Game.CURRENTWEAPON = w;
        Game.POOL = pool;


        foreach (GameObject go in s.GetRootGameObjects())
        {
            go.SetActive(false);
        }

        Game.CURRENTWEAPON.gameObject.SetActive(true);
        Game.POOL.gameObject.SetActive(true);

        s = SceneManager.GetSceneByName("Level_Street");
        SceneManager.SetActiveScene(s);
        foreach (GameObject go in s.GetRootGameObjects())
        {
            if (go.name == "LevelController")
            {
                Game.CURRENTLEVEL = go.GetComponent<LevelBase>();
            }
        }

        s = SceneManager.GetSceneByName("Level_UI");
        SceneManager.SetActiveScene(s);
        foreach (GameObject go in s.GetRootGameObjects())
        {
            if (go.name == "Controller")
            {
                Game.UI = go.GetComponent<LevelUI>();
            }
        }


        s = SceneManager.GetSceneByName("Level_Street");
        SceneManager.SetActiveScene(s);
        Game.CURRENTLEVEL.InitLevel();
        Game.UI.ChangeHpText(Game.PLAYER.healthPoints.ToString());
        StartCoroutine(WaveManager.Instance.InitializeWave(1));
        StartCoroutine(Intro());
    }

    private IEnumerator Intro()
    {
        Game.UI.TriggerAnnouncement("Destroy All Zombies");
        yield return new WaitForSeconds(5);
        Game.UI.TriggerAnnouncement("Press SHIFT to run!");
    }
}
