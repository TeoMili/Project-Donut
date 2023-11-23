using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public static ManageScenes Instance;

    public int previousScene;
    private int currentScene;

    public bool gamePaused = false;

    //DIFFICULTY VALUES:
    // 0 FOR BEGINNER
    // 1 FOR INTERMEDIATE
    // 2 FOR ADVANCED
    public int difficulty = 0;

    //FOR ROOM RANDOMISATION ALGORITHM
    public int roomCount = 15; //the number of scenes containing rooms
    public int puzzleCount = 1; //the number of scenes containing puzzles

    public List<int> backupRooms = new List<int>(); 
    public List<int> mainRooms = new List<int>(); 
    public List<int> visitedRooms = new List<int>(); 

    //FOR PUZZLE RANDOMISATION ALGORITHM
    public struct Puzzle
    {
        public int index;
        public int probability;
    } 

    public List<Puzzle> puzzleList = new List<Puzzle>(); //list of all puzzles in the game (along with their probability
    public List<int> probabilityList = new List<int>();  //list of existing puzzle probabilities

    //list of puzzles encountered during a run
    //public List<int> encounteredPuzzles = new List<int>();

    //for tracking the number of successful escape attempts
    //public int successfulEscapes = -1;

    void Start()
    { 
        //FOR PUZZLE RANDOMISATION BASED ON CHOSEN DIFFICUTLY
        //SetPuzzleProbability(); 
    }

    private void Awake()
    {
        //create an instance of the ManageScenes class
        //if it already exists, replace the old one

        if (Instance != null)
        {
           Destroy(gameObject);
           return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void UnlockMouse()
    {
        //unlocks the cursor allowing the player to interact with the UI elements

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CreatePuzzle(int index, int probability)
    {
        //creates a new Puzzle struct with a given probability and adds it to the list of puzzles
        Puzzle p = new Puzzle();
        p.index = index;
        p.probability = probability;

        puzzleList.Add(p);

        //check if the current probability is in the probabilities list
        if (!probabilityList.Contains(probability))
        {
            probabilityList.Add(probability);
        }
    }

    public List<int> GetPuzzles(int probability)
    {
        //returns a list of all puzzle indexes with the given probability
        List<Puzzle> p = new List<Puzzle>();
        p = puzzleList.FindAll(x => x.probability == probability);

        List<int> elements = new List<int>();

        foreach(Puzzle i in p)
        {
            elements.Add(i.index);
        }

        return elements;
    }

    public void ReduceProbability(int index)
    {
        //reduces the probability of appearance of a given puzzle

        //find puzzle in the list
        Puzzle p = puzzleList.Find(x => x.index == index);

        if(p.probability != 0)
        {
            p.probability--;
        }
    }

    public void IncreaseProbability(int index)
    {
        //inncreases the probability of appearance of a given puzzle

        //find puzzle in the list
        Puzzle p = puzzleList.Find(x => x.index == index);

        if (p.probability != 10)
        {
            p.probability++;
        }
    }
}
