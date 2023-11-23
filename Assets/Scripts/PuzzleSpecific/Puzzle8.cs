using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Puzzle8 : MonoBehaviour
{
    // Object oriented programming begginer level puzzle:
    //compute code output given input

    public TMP_Text textElement1;
    public TMP_Text textElement2;
    public TMP_Text feedbackElement;
    //private double sol;
    private string sol;


    public TMP_InputField inputField;

    void Start()
    {
        //generate random input
        int x = Random.Range(1, 10);

        int y = x;
        
        while(x == y || Mathf.Abs(x - y) < 3 || Mathf.Abs(x - y) > 5)
        {
            y = Random.Range(1, 10);
        }

        //display input and variables on canvas
        textElement1.text = x.ToString();
        textElement2.text = y.ToString();

        //compute result of code
        sol = "";
       // double n = 1;
        if( x > y)
        {
            int a = x;
            x = y;
            y = a;
        }
        int nr = 1;
        for (int i = y; i >= x; i--)
        {
            //sol = sol * n + 1;
            sol = sol + "1";
            //n = n * 10;
            //Debug.Log("Initial " + n);
            if (nr >= x)
            {
                //sol = sol * n + 2;
                // n = n * 10;
                // Debug.Log("Inter " + n);
                sol = sol + "2";
            }
            nr = nr * 3;
            // sol = sol * n + 1;
            // n = n * 10;
            // Debug.Log("Final " + n);
            sol = sol + "1";
           
        }
        Debug.Log(sol);

        inputField.onEndEdit.AddListener(CheckSol);

    }

    public void CheckSol(string userInput)
    {
        //check if the user input matches the actual solution
        // Debug.Log(userInput);
        if (userInput.Equals(sol))
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
