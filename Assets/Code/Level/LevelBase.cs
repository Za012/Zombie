using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBase : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Level Start");
        Scene levelGame = SceneManager.GetSceneByName("Level_Game");
        if (!levelGame.isLoaded)
        {
            SceneManager.LoadScene("Level_Game", LoadSceneMode.Single);
        }
    }

    public virtual void InitLevel() { }

}

