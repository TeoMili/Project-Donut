using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   
    public Button playButton;
    public Button optionsButton;
    public Button exitButton;

    void Start()
    {
        playButton.onClick.AddListener(LoadGame);
        optionsButton.onClick.AddListener(OptionsMenu);
        exitButton.onClick.AddListener(Exit);

        ManageScenes.Instance.UnlockMouse();
    }

    public void Exit()
    {

#if UNITY_EDITOR
        
            EditorApplication.ExitPlaymode();
        
#else

        Application.Quit();
#endif


    }


    public void LoadGame()
    {
        SceneManager.LoadScene(2);
    }


    public void OptionsMenu()
    {
        // ManageScenes.Instance.previousScene = SceneManager.GetActiveScene().buildIndex;
        //previousScene = SceneManager.GetActiveScene().buildIndex;
        ManageScenes.Instance.previousScene = 0;
        SceneManager.LoadScene(1);
    }
}


