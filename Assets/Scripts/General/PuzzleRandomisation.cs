using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PuzzleRandomisation : MonoBehaviour
{

    /* 
     - Manages the initialisation of the Puzzle objects (in ManageScenes), assigning starting probabilities to each puzzle in the game


     - NextPuzzle(): chooses a random puzzle accounting for probabilities in the following way:
           - from each probability list, choose: n / (x - i), where:
                   * n - number of puzzles with given probability
                   * x - number of possible probabilities
                   * i - index of current probability in the probability list (sorted in increasing order)

          - the probability of the chosen puzzle is reduced by 1
       */

    //list of candidate puzzles for the randomisation algorithm
    public List<int> puzzleCandidates = new List<int>();

    void Start()
    {
        //check if player is in the beginning of the game
        if (ManageScenes.Instance.puzzleList.Count == 0)
        {
            //add all puzzles in the game to the list with an assigned probability of 5
            for(int i = ManageScenes.Instance.roomCount + 3; i < ManageScenes.Instance.puzzleCount + ManageScenes.Instance.roomCount + 3; i++)
            {
                ManageScenes.Instance.CreatePuzzle(i, 5);
            }
        }
    }

    public int NextPuzzle()
    {
        //chooses a random puzzle based on the selected difficulty and the previously encountered puzzles

        //clear candidates list
        puzzleCandidates.Clear();

        //sort the existing probabilities in ascending order
        ManageScenes.Instance.probabilityList.Sort();

        //get the max probability
        int maxP = ManageScenes.Instance.probabilityList.Last();

        //for each probability add n / (x - i) elements to the candidates list
        for (int i = 0; i < ManageScenes.Instance.probabilityList.Count; i++)
        {
            //get all the puzzles with probability probabilityList[i]
            List<int> elements = new List<int>();
            elements = ManageScenes.Instance.GetPuzzles(ManageScenes.Instance.probabilityList[i]);

            //calculate the number of puzzles to add to the candidates list
            int toAdd = elements.Count / (ManageScenes.Instance.probabilityList.Count - i);

            //randomly add puzzles to the candidates list
            for (int j = 0; j < toAdd; j++)
            {
                int index = Random.Range(0, elements.Count);
                puzzleCandidates.Add(elements[index]);
            }
        }

        //choose a random puzzle from the candidates list
        int chosenPuzzle = puzzleCandidates[Random.Range(0, puzzleCandidates.Count)];

        //reduce the probability of the chosen puzzle
        ManageScenes.Instance.ReduceProbability(chosenPuzzle);

        return chosenPuzzle;

    }
}
