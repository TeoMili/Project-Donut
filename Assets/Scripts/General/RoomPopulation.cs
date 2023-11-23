using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomPopulation : MonoBehaviour
{
    public struct Puzzle
    {
        public GameObject pLocation;
        public int pContent;
        //public GameObject hLocation;
        //public int hContent;

    }

    public int nrActivePuzzles = 0;

    //list of active puzzles in the scene
    public List<Puzzle> activePuzzles = new List<Puzzle>();


    //Lists for the different type of tags present in the scene
    private List<GameObject> puzzleObjects = new List<GameObject>();
    //public List<GameObject> hintObjects = new List<GameObject>();

    //chosen game objects where to place puzzles/hints
    private GameObject linkedP;
    //private GameObject linkedH;

    //IMPORT SCRIPTS
    private PuzzleRandomisation randScript;

    void Start()
    {
        //get scripts
        randScript = GetComponent<PuzzleRandomisation>();

        //get all objects with the PotentialPuzzle tag from the scene
        GameObject[] parray = GameObject.FindGameObjectsWithTag("PotentialPuzzle");
        for (int i = 0; i < parray.Length; i++)
            puzzleObjects.Add(parray[i]);

        /* Choose a number of puzzles to activate in the current scene, based on the chosen difficulty:
            
            * between 1 and 3 for beginner
            * between 2 and 4 for intermediate
            * between 3 and 5 for advanced

        */

        switch (ManageScenes.Instance.difficulty)
        {
            case 0:
                nrActivePuzzles = Random.Range(1, 4);
                //allow hints
                break;

            case 1:
                nrActivePuzzles = Random.Range(2, 5);
                break;

            case 2:
                nrActivePuzzles = Random.Range(3, 6);
                break;
        }

        //if the current scene is not a puzzle room, there are no puzzles that need activation
        if (SceneManager.GetActiveScene().buildIndex > 2)
            ActivatePuzzles(nrActivePuzzles);
        else
            nrActivePuzzles = 0;
    }

    private void ActivatePuzzles(int nrActivePuzzles)
    {
        for (int i = 1; i <= nrActivePuzzles; i++)
        {
            linkedP = null;

            //generate random index from the puzzleObjects list
            int puzzleIndex = Random.Range(0, puzzleObjects.Count);

            //change the tag of the selected object to ActivePuzzle
            puzzleObjects[puzzleIndex].GetComponent<Collider>().tag = "ActivePuzzle";

            //remove the object from the puzzleObjects list
            linkedP = puzzleObjects[puzzleIndex];
            puzzleObjects.Remove(puzzleObjects[puzzleIndex]);

            //choose a puzzle to link to the game object based on difficulty and previously used puzzles
            int assignedPuzzle = randScript.NextPuzzle();

            //create the puzzle struct
            Puzzle newPuzzle = new Puzzle();
            newPuzzle.pLocation = linkedP;
            newPuzzle.pContent = assignedPuzzle;

            //add puzzle struct to the list of active puzzles
            activePuzzles.Add(newPuzzle);
        }
    }

    public void CompletePuzzle(int index)
    {
        //MARKS A PUZZLE AS COMPLETED

        //untag the corresponding game object
        activePuzzles[index].pLocation.GetComponent<Collider>().tag = "InactivePuzzle";

        //remove from activePuzzles list
        activePuzzles.RemoveAt(index);

        //reduce number of active puzzles
        nrActivePuzzles--;
    }
}
