using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
//using System;

public class ObjectDetection : MonoBehaviour
{
    /* Manages object detection and actions taken based on the type of object:

            * Doors: if all the puzzles in the room were completed, load the next room
            * Puzzles: load a corresponding puzzle scene
            * TVs: display the memory fragments currently obtained by the player
            * Fragment Robots: load a memory fragment puzzle
 
    */

    //FOR RAYCASTING (OBJECT DETECTION)
    RaycastHit hitData;
    private Vector3 hitPoint;
    private float hitDistance;
    private string hitTag = "";
    private GameObject hitObject;

    //FOR MANAGING ROOM COMPLETION CONDITIONS
    public int currentPuzzleIndex;

    //FOR UI
    public TMP_Text playerMessage;
    
    //IMPORT SCRIPTS
    private RoomRandomisation rrandomisationScript;
    private RoomPopulation populationScript;

    void Start()
    {
        playerMessage.text = "";

        rrandomisationScript = GetComponent<RoomRandomisation>();
        populationScript = GetComponent<RoomPopulation>();
        
    }

    void Update()
    {

        //reset the message every frame
        playerMessage.text = "";

        //create a ray from the mouse position
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mouseRay.origin, mouseRay.direction * 8);

        //fire the ray to detect interactible objects 
        hitData = FireRay(mouseRay);

        //get information about the object that was hit by the ray
        hitPoint = hitData.point;
        hitDistance = hitData.distance;

        //acount for objects with no collider
        if (hitData.collider != null)
        {
            hitTag = hitData.collider.tag;
            hitObject = hitData.transform.gameObject;

            switch (hitTag)
            {
                case "Door":
                    //check if all puzzles in the room have been completed
                    if (populationScript.nrActivePuzzles == 0)
                    {
                        playerMessage.text = "Click to leave";

                        //on click, load the next room
                        if (Input.GetMouseButtonDown(0))
                        {
                            rrandomisationScript.NextRoom();
                        }
                    }
                    else
                    {
                        playerMessage.text = "I can't leave yet...";

                        // TO DO: REMOVE WHEN GAME IS COMPLETED
                        if (Input.GetMouseButtonDown(0))
                        {
                            rrandomisationScript.NextRoom();
                        }
                    }
                   
                    break;

                case "ActivePuzzle":
                    playerMessage.text = "Click to inspect";
                    if (!ManageScenes.Instance.gamePaused && Input.GetMouseButtonDown(0))
                    {
                        //pause game
                        ManageScenes.Instance.previousScene = SceneManager.GetActiveScene().buildIndex;
                        Time.timeScale = 0;
                        ManageScenes.Instance.gamePaused = true;

                        //get the Puzzle object that the player has detected
                        currentPuzzleIndex = populationScript.activePuzzles.FindIndex(x => x.pLocation == hitObject);

                        //load corresponding puzzle scene
                        SceneManager.LoadScene(populationScript.activePuzzles[currentPuzzleIndex].pContent, LoadSceneMode.Additive);
                    }
                    break;

                case "ActiveHint":
                    //playerMessage.text = "Click to inspect";

                    /* TO DO:
                     *        - show interaction message
                     *        - on click, load corresponding hint
                     */
                    break;
            }
        }

    }
   
    RaycastHit FireRay(Ray ray)
    {
        RaycastHit data;
        Physics.Raycast(ray, out data, 10);
        return data;
    }
}
