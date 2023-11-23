using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    private Scene currentPuzzle;

    private GameObject playerObj;

    //UI
    public Button submitButton;

    //SCRIPTS
    private ObjectDetection detectionScript;
    private RoomPopulation populationScript;

    void Start()
    {
        submitButton.onClick.AddListener(Submit);

        ManageScenes.Instance.UnlockMouse();
        currentPuzzle = SceneManager.GetSceneAt(1);
        string name = currentPuzzle.name;

        //TO DO: GET CORRESPONDING SCRIPT

        //get the player game object and its relevant attached scripts
        playerObj = GameObject.FindWithTag("Player");
        if(playerObj != null)
        {
            detectionScript = playerObj.GetComponent<ObjectDetection>();
            populationScript = playerObj.GetComponent<RoomPopulation>();
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            ManageScenes.Instance.gamePaused = false;
            ManageScenes.Instance.LockMouse();
            SceneManager.UnloadSceneAsync(currentPuzzle.buildIndex);
        }
    }

    public void Submit()
    {
        //unload puzzle scene and resume gameplay
        Time.timeScale = 1;
        ManageScenes.Instance.gamePaused = false;
        ManageScenes.Instance.LockMouse();
        SceneManager.UnloadSceneAsync(currentPuzzle.buildIndex);

        //TO DO: CHECK IF SOLUTION IS CORRECT

        //MARK PUZZLE AS COMPLETED
        populationScript.CompletePuzzle(detectionScript.currentPuzzleIndex);




    }
}
