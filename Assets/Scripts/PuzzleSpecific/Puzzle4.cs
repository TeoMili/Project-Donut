using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puzzle4 : MonoBehaviour
{

    private int vSize;
    private Vector2 aSize; 

    private int x;
    private int[] v;
    private int[][] a;

    public TMP_InputField xInput;
    public TMP_InputField vInput;
    public TMP_InputField aInput;

    void Start()
    {

        //generate random sizes and values for x, v and a
        vSize = Random.Range(10, 20);
        aSize.x = Random.Range(10, 20);
        aSize.y = Random.Range(10, 20);

        x = Random.Range(1, 10000);

        for(int i = 0; i < vSize; i++)
        {
            v[i] = Random.Range(1, 10000);
        }

        for(int i = 0; i < aSize.x; i++)
            for(int j = 0; j < aSize.y; j++)
            {
                a[i][j] = Random.Range(1, 10000);
            }


        //xInput.onEndEdit.AddListener(GiveX);
        //vInput.onEndEdit.AddListener(GiveV);
        //aInput.onEndEdit.AddListener(GiveA);
    }

  
    void Update()
    {
        
    }

    public void CheckSol(string userInput)
    {
       /* //check if the user input matches the actual solution
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
        }*/

    }
}
