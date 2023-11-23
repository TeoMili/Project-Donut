using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Puzzle14 : MonoBehaviour
{

    public TMP_Text expressionField;
    public TMP_Text whileLoop1;
    public TMP_Text whileLoop2;
    public TMP_Text declaration1;
    public TMP_Text declaration2;

    //panels for the feedback
    public GameObject incorrectPanel;
    public GameObject correctPanel;

    private int correctAnswer;

    private bool incorrect = false;

    void Start()
    {
        //set feedback panels to inactive
        incorrectPanel.SetActive(false);
        correctPanel.SetActive(false);
        incorrect = false;


        //generate a random expression
        string expression = Random.Range(10, 101).ToString();
        expressionField.text = expression;

        //choose random declaration
        // int declaration = Random.Range(0, 2);
        int declaration = 1;
        declaration1.text = "i = " + declaration.ToString();
        declaration2.text = " i = " + declaration.ToString();

        //create do while loops
        string while1 = "do{ \n v[i] = " + expression + "\n i = i + 1 \n }while(i <";
        string while2 = while1;

        int decision = Random.Range(1, 3);
        if (decision == 1)
        {
            while1 = while1 + "= n)";
            while2 = while2 + " n)";

           // while1 = while1 + "i = i + 1 \n v[i] = " + expression + "\n }while(i <";
           // while2 = while2 + "v[i] = " + expression + "\n i = i + 1 \n }while(i <";
        }
        else
        {

            while1 = while1 + " n)";
            while2 = while2 + "= n)";
            // while1 = while1 + "v[i] = " + expression + "\n i = i + 1 \n }while(i <";
            // while2 = while2 + "i = i + 1 \n v[i] = " + expression + "\n }while(i <";
        }

        /*if(declaration == 1)
        {
            while1 = while1 + "=";
            while2 = while2 + "=";
        }

        while1 = while1 + " n)";
        while2 = while2 + " n)"; */

        whileLoop1.text = while1;
        whileLoop2.text = while2;

        if (declaration == 0)
            if (decision == 1)
                correctAnswer = 1;
            else
                correctAnswer = 2;
        else
            if (decision == 1)
            correctAnswer = 2;
        else
            correctAnswer = 1;

    }

    void Update()
    {
        //listen for mouse click
        if (incorrect && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Submit1()
    {
        if (correctAnswer == 1)
        {
            correctPanel.SetActive(true);
        }
        else
        {
            incorrectPanel.SetActive(true);
            incorrect = true;
        }
    }

    public void Submit2()
    {
        if (correctAnswer == 2)
        {
            correctPanel.SetActive(true);
        }
        else
        {
            incorrectPanel.SetActive(true);
            incorrect = true;
        }
    }
}
