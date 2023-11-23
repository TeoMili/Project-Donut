using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Puzzle7 : MonoBehaviour
{
    // Object oriented programming begginer level puzzle:
    //compute code output given input

    public TMP_Text textElement1;
    public TMP_Text feedbackElement;
    private int sol;


    public TMP_InputField inputField;

    void Start()
    {
        //generate random input
        int n = Random.Range(10000, 100000);
       
        

        //display input and variables on canvas
        textElement1.text = n.ToString();

        //compute result of code
        sol = 0;
        do
        {
            int c = n % 10;
            n = n / 10;
            if (c > 5)
                c = c / 2;
            sol = sol * 10 + c;
        } while (n != 0);

        Debug.Log(sol);

        inputField.onEndEdit.AddListener(CheckSol);

    }

    public void CheckSol(string userInput)
    {
        //check if the user input matches the actual solution
        // Debug.Log(userInput);
        if (userInput.Equals(sol.ToString()))
        {
            feedbackElement.text = "Correct!";
            //Debug.Log("same");
        }
        else
        {
            feedbackElement.text = "Incorrect!";
            //Debug.Log("nope");
        }

    }


}
