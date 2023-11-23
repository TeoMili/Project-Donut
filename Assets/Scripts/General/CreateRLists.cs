using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateRLists : MonoBehaviour
{
    
    void Start()
    {
        


        //FOR PUZZLE DIFFICULTY ADJUSTMENTS
        /*if(ManageScenes.Instance.nrActivePuzzles != 0)
        {
            //decrease the probability of appearance of unsolved puzzle
            for(int i = 0; i < ManageScenes.Instance.activePuzzles.Count; i++)
            {
               int j = ManageScenes.Instance.puzzleList.FindIndex(x => x.index == ManageScenes.Instance.activePuzzles[i].pContent);
                //ManageScenes.Instance.puzzleList[j].probability -= 50;
                //Debug.Log(ManageScenes.Instance.puzzleList[j].probability);

                int puzzleIndex = ManageScenes.Instance.puzzleList[j].index;
                int puzzleProb = ManageScenes.Instance.puzzleList[j].probability;

                ManageScenes.Instance.puzzleList.Remove(ManageScenes.Instance.puzzleList[j]);

                puzzleProb -= 50;

                ManageScenes.Instance.AddPuzzle(puzzleIndex, puzzleProb);
            }

            //decrease the probability of appearance of encountered puzzles
            for(int i = 0; i < ManageScenes.Instance.encounteredPuzzles.Count; i++)
            {
                int j = ManageScenes.Instance.puzzleList.FindIndex(x => x.index == ManageScenes.Instance.encounteredPuzzles[i]);
                // ManageScenes.Instance.puzzleList[j].probability -= 20;


                int puzzleIndex = ManageScenes.Instance.puzzleList[j].index;
                int puzzleProb = ManageScenes.Instance.puzzleList[j].probability;

                ManageScenes.Instance.puzzleList.Remove(ManageScenes.Instance.puzzleList[j]);

                puzzleProb -= 20;

                ManageScenes.Instance.AddPuzzle(puzzleIndex, puzzleProb);
            }
        }*/


}

   /* public bool displayChoice()
    {
        //makes a choice of whether to display the robot or not based on the number of successful escapes

        int upBound = ManageScenes.Instance.successfulEscapes / 2 + 1;
        int c = Random.Range(0, upBound);
        if (c == 0)
            return false;
        else
        {
            //reset the number of successful escapes
            ManageScenes.Instance.successfulEscapes = 0;
            return true;
        }
    } */
}
