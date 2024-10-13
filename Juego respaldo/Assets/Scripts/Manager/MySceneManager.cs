using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadNextScene()
    {
        //get the current scene index
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        //load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadFirstScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void LoadScene(int sceneIndex)
    {
        //load the scne with the given index
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    //make a function that will quit the game
    public void QuitGame()
    {
        //quit the game
        Application.Quit();
    }
        
}
