using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Puzzle2 : MonoBehaviour
{
    // Object oriented programming begginer level puzzle:
    //compute code output given input

    public TMP_Text textElement1;
    public TMP_Text textElement2;
    public TMP_Text varElement1;
    public TMP_Text varElement2;
    public TMP_Text feedbackElement;
    private int sol;


    public TMP_InputField inputField;
    
    void Start()
    {
        //generate random input
        int m = Random.Range(2, 9);
        int n = m;
        while (n == m)
        {
            n = Random.Range(2, 9);
        }
        //generate random variables
        int var1 = Random.Range(10, 90);
        int var2 = Random.Range(var1 + 1, var1 + 15);

        //display input and variables on canvas
        textElement1.text = m.ToString();
        textElement2.text = n.ToString();
        varElement1.text = var1.ToString();
        varElement2.text = var2.ToString();

        //compute result of code
        sol = 0;
        for(int x = var1; x <= var2; x++)
        {
            if(x % m == 0 || x % n == 0)
                sol = sol + x;
            if (x % m == 0 && x % n == 0)
                sol = sol - x;
        }
        Debug.Log(sol);

        inputField.onEndEdit.AddListener(CheckSol);

    }

    public void CheckSol(string userInput)
    {
        //check if the user input matches the actual solution
        // Debug.Log(userInput);
        if (userInput.Equals(sol.ToString())){
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
