using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Puzzle1 : MonoBehaviour
{
    // Object oriented programming begginer level puzzle:
    //compute code output given input

    public TMP_Text textElement;
    public TMP_Text varElement1;
    public TMP_Text varElement2;
    public TMP_Text feedbackElement;
    private int sol;
    
    void Start()
    {
        //generate random input
        int x = Random.Range(1, 100);
        
        //generate random variables
        int var1 = Random.Range(1, 100);
        int var2 = Random.Range(1, 10);
        
        //display input and variables on canvas
        textElement.text = x.ToString();
        varElement1.text = var1.ToString();
        varElement2.text = var2.ToString();

        //compute result of code
        int y = x + var1;
        int z = y * var2;
        sol = z - y;
        Debug.Log(sol);
       
    }

    public void CheckSol(string userInput)
    {
        //check if the user input matches the actual solution
        if (userInput.Equals(sol.ToString())){
            feedbackElement.text = "Correct!";
            SceneManager.UnloadSceneAsync(18);
        }
        else
        {
            feedbackElement.text = "Incorrect!";
        }
        
    }

    
}
