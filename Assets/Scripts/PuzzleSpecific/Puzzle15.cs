using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Puzzle15 : MonoBehaviour
{

    public GameObject initialTable;
    public GameObject resultTable;
    public TMP_Text buttonText;
    public TMP_Text textPrefab;

    private bool activeTable = false;


    //SQL table
    private string[,] table = new string[20, 5];
    private string[,] rTable = new string[20, 5];
    private string[] fnames = { "Nancy", "John", "William", "Kate", "Mary", "Jack", "Marvin", "Rose", "George", "Steven", "Lara", "David", "Tom", "Jane" };
    private string[] lnames = { "Green", "Adams", "Ward", "Stewart", "Miller", "Hill", "Jones", "Foster" };
    private string[] operators = { "<", ">", "=" };

    //for the possible questions
    private bool foundVar = false;
    private int valueVar;
    private string chosenOp;

    private int entries;

    void Start()
    {
        //hide the result table
        resultTable.SetActive(false);
        buttonText.text = "See resulting table";

        //generate random SQL table with 4 columns (id, first name, last name, age)
        entries = Random.Range(10, 16);

        //first line of the table contains the field names
        table[0, 0] = "ID";
        table[0, 1] = "FName";
        table[0, 2] = "LName";
        table[0, 3] = "Age";

        for(int i = 1; i <= entries; i++)
        {
            table[i, 0] = i.ToString();
            table[i, 1] = fnames[Random.Range(0, fnames.Length)];
            table[i, 2] = lnames[Random.Range(0, lnames.Length)];
            int placeholder = Random.Range(16, 40);
            table[i, 3] = placeholder.ToString();

            if (!foundVar && decision())
            {
                foundVar = true;
                valueVar = placeholder;
            }

        }

        if (!foundVar)
            valueVar = Random.Range(16, 40);

        //choose a random opertor for the condition
        chosenOp = operators[Random.Range(0, operators.Length)];

        for(int i = 0; i <= entries; i ++)
            for(int j = 0; j < 4; j++)
            {
                //Instantiate the prefab
                TMP_Text childObject = Instantiate(textPrefab, initialTable.transform);
                childObject.text = table[i, j];
            }

        //decide on a random number of fields to include in the resulting table:
        // 1 -> just the ids
        // 2 -> fname + lname
        // 3 -> all fields
        int nrFields = Random.Range(1, 4);

        //compute resulting table
        //int resultEntries = 1;
       /* for(int i = 0; i <= entries; i++)
        {
            switch (chosenOp)
            {
                case "<":
                    if(table[i, 3] < valueVar)
                    {
                        if(nrFields == 1)
                        {
                            //include just the id
                            rTable[resultEntries, 0] = table[i, 0];
                        }
                        else if(nrFields == 2)
                        {
                            //include fname and lname
                            rTable[resultEntries, 1] = table[i, 1];
                            rTable[resultEntries, 2] = table[i, 2];
                        }
                        else
                        {
                            //include all fields
                            rTable[resultEntries, 0] = table[i, 0];
                            rTable[resultEntries, 1] = table[i, 1];
                            rTable[resultEntries, 2] = table[i, 2];
                            rTable[resultEntries, 3] = table[i, 3];
                        }
                    }
                    break;
                case ">":
                    if (table[i, 3] > valueVar)
                    {
                        if (nrFields == 1)
                        {
                            //include just the id
                            rTable[resultEntries, 0] = table[i, 0];
                        }
                        else if (nrFields == 2)
                        {
                            //include fname and lname
                            rTable[resultEntries, 1] = table[i, 1];
                            rTable[resultEntries, 2] = table[i, 2];
                        }
                        else
                        {
                            //include all fields
                            rTable[resultEntries, 0] = table[i, 0];
                            rTable[resultEntries, 1] = table[i, 1];
                            rTable[resultEntries, 2] = table[i, 2];
                            rTable[resultEntries, 3] = table[i, 3];
                        }
                    }
                    break;
                case "=":
                    if (table[i, 3] == valueVar)
                    {
                        if (nrFields == 1)
                        {
                            //include just the id
                            rTable[resultEntries, 0] = table[i, 0];
                        }
                        else if (nrFields == 2)
                        {
                            //include fname and lname
                            rTable[resultEntries, 1] = table[i, 1];
                            rTable[resultEntries, 2] = table[i, 2];
                        }
                        else
                        {
                            //include all fields
                            rTable[resultEntries, 0] = table[i, 0];
                            rTable[resultEntries, 1] = table[i, 1];
                            rTable[resultEntries, 2] = table[i, 2];
                            rTable[resultEntries, 3] = table[i, 3];
                        }
                    }
                    break;
            }
        } */

        //complete the question
        //variable.text = valueVar;

    }

    public void toggleTable()
    {
        if (activeTable)
        {
            //deactivate result table and activate initial table
            initialTable.SetActive(true);
            resultTable.SetActive(false);
            activeTable = false;
            buttonText.text = "See resulting table";
        }
        else
        {
            //deactivate result table and activate initial table
            initialTable.SetActive(false);
            resultTable.SetActive(true);
            activeTable = true;
            buttonText.text = "See initial table";
        }
    }

    public bool decision()
    {
        int i = Random.Range(0, 5);
        if (i == 0)
            return true;
        return false;
    }
}
