using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomRandomisation : MonoBehaviour
{
    /*
    
    - Manages the initialisation of the room lists for the room randomisation algorithm

    TYPES OF LISTS:
    mainRooms:      - rooms with highest probability of being chosen by the algorithm
                    - used to maximise the player's chance of encountering new rooms at every run

    backupRooms:    - rooms with lower probability of being chosen by the algorithm (at least 1/3 of total rooms)
                    - used to add randomness to the algorithm

    visitedRooms:   - recently visited rooms, with no chance of being chosen by the algorithm
                    - used to prevent the content from being repetitive

    - NextRoom(): function for choosing a random room accounting for the probabilities.
    Candidates list will contain all rooms from the main list and one randomly chosen room from the backup list.
    After each choice, a new instance of the PR is added to the backup list to increase the probability of the run ending early.
    
     */

    private List<int> choiceRooms = new List<int>();

    void Start()
    {
        //check if player is in PR
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (ManageScenes.Instance.visitedRooms.Count == 0)
            {
                //player is at the beginning of the game

                //retrieve all scenes containing rooms
                for (int i = 3; i < ManageScenes.Instance.roomCount + 3; i++)
                {
                    ManageScenes.Instance.mainRooms.Add(i);
                }

                //move 1/3 random rooms to the backup list
                for (int i = 0; i < ManageScenes.Instance.roomCount / 3; i++)
                {
                    int index = Random.Range(0, ManageScenes.Instance.mainRooms.Count);
                    int room = ManageScenes.Instance.mainRooms[index];
                    ManageScenes.Instance.backupRooms.Add(room);
                    ManageScenes.Instance.mainRooms.Remove(room);
                }
            }
            else
            {
                //remove all instances of the player room from the backup list
                ManageScenes.Instance.backupRooms.RemoveAll(x => x == 2);

                //move (backup.Count + visited.Count) - 1/3 * roomCount rooms from backup to main list
                int toMove = ManageScenes.Instance.backupRooms.Count + ManageScenes.Instance.visitedRooms.Count - ManageScenes.Instance.roomCount / 3;

                Debug.Log("To Move: " + toMove);

                for (int i = 0; i < toMove; i++)
                {
                    Debug.Log("Iteration: " + i);
                    //get random backup room index
                    int index = Random.Range(0, ManageScenes.Instance.backupRooms.Count);
                    Debug.Log("index: " + index);
                    int room = ManageScenes.Instance.backupRooms[index];
                    //add the room to the main list
                    ManageScenes.Instance.mainRooms.Add(room);
                    //remove the room from the backup list
                    ManageScenes.Instance.backupRooms.Remove(room);
                    Debug.Log("count: " + ManageScenes.Instance.backupRooms.Count);
                }

                //add the rooms from the visited list to the backup list
                ManageScenes.Instance.backupRooms.AddRange(ManageScenes.Instance.visitedRooms);

                //clear the visited list
                ManageScenes.Instance.visitedRooms.Clear();
            }
        }
    }

    public void NextRoom()
    {
        /* Algorithm that decides which room the player will encounter next based on the probabilities indicated by the different room lists */

        int currentScene = SceneManager.GetActiveScene().buildIndex;

        //if the current scene is a room (not the player room), add it to the list of visited rooms and remove it from its original list
        if (currentScene > 2)
        {
            ManageScenes.Instance.visitedRooms.Add(currentScene);
            ManageScenes.Instance.backupRooms.Remove(currentScene);
            ManageScenes.Instance.mainRooms.Remove(currentScene);

            //add an instance of the player room to the backup list to increase the probability of
            //it being encountered next
            ManageScenes.Instance.backupRooms.Add(2);
        }

        //check if the player has been in 5 rooms, if so go back to the main room
        if (ManageScenes.Instance.visitedRooms.Count == 5)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            choiceRooms.Clear();

            //add all the rooms from the main list to the choice list
            choiceRooms.AddRange(ManageScenes.Instance.mainRooms);

            //get one room from the backup list and add it to the choice list
            int room = ManageScenes.Instance.backupRooms[Random.Range(0, ManageScenes.Instance.backupRooms.Count)];
            choiceRooms.Add(room);

            //get a random room from the choice list
            int nextRoom = choiceRooms[Random.Range(0, choiceRooms.Count)];

            //load next room
            SceneManager.LoadScene(nextRoom);
        }

    }
}
