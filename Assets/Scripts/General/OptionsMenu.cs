using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public TMP_Dropdown difficultySetting;

    public GameObject backToTitle;

    void Start()
    {
        //check if the previous scene was a room or a puzzle and if so, display the back to title button
        backToTitle.SetActive(false);
        if (ManageScenes.Instance.previousScene > 1)
            backToTitle.SetActive(true);

        //Display the current chosen difficulty
        difficultySetting.value = ManageScenes.Instance.difficulty;

        //Unlock cursor and allow for clicking on buttons
        ManageScenes.Instance.UnlockMouse();

        if (ManageScenes.Instance.previousScene != 0)
            backToTitle.SetActive(true);
    }

    public void Back()
    {
        //return to the previous scene
        SceneManager.LoadScene(ManageScenes.Instance.previousScene);
    }
    public void BackTitle()
    {
        //return to the title screen
        SceneManager.LoadScene(0);
    }

    public void SetDifficulty()
    {
        if (difficultySetting.options[difficultySetting.value].text == "Beginner")
            ManageScenes.Instance.difficulty = 0;
        else if (difficultySetting.options[difficultySetting.value].text == "Intermediate")
            ManageScenes.Instance.difficulty = 1;
        else
            ManageScenes.Instance.difficulty = 2;

        //reset the puzzle probabilities
        //ManageScenes.Instance.SetPuzzleProbability();
    }


}
