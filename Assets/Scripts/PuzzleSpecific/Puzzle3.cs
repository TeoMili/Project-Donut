using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Puzzle3 : MonoBehaviour
{

    public LogicExprGenerator script;

    public TMP_Text test;
   
    public TMP_Text feedback;
    public TMP_Text pValue;
    public TMP_Text qValue;
    public TMP_Text rValue;
    public TMP_Dropdown userInput;

    private string[] symbols = { "->", "<-", "v", "^" };
    private string[] variables = { "p", "q", "r" };

    private bool[] truth = new bool[3];

    Stack<bool> values = new Stack<bool>();
    Stack<string> operations = new Stack<string>();

    private bool sol;

    void Start()
    {

        //assign random truth values to the variables
        truth[0] = script.decision();
        truth[1] = script.decision();
        truth[2] = script.decision();

        pValue.text = "p = " + truth[0].ToString();
        qValue.text = "q = " + truth[1].ToString();
        rValue.text = "r = " + truth[2].ToString();

        //Generate

        int L = Random.Range(1, 3);

        string expression = script.GenerateExpression(0, L, symbols, variables);

        //clean the expression from the "," characters

        test.text = expression.Replace(",", "");

        //Debug.Log(expression);

        //Evaluate

        //separate the string into components
        string[] components = expression.Split(",");
        bool isVar;

        //put the components in the corresponding stacks
        for (int i = 0; i < components.Length; i++)
        {
            isVar = false;
            for (int j = variables.Length - 1; j >= 0; j--)
            {
                //check if the component is a variable
                if (components[i] == variables[j])
                {
                    //get the truth value of the variable and add it to the values stack
                    values.Push(truth[j]);
                    isVar = true;
                }
            }
            if (!isVar)
                operations.Push(components[i]);
        }

        script.EvaluateExpression(ref operations, ref values);

        Debug.Log(values.Pop());

    }


    public void CheckSol()
    {
        //Debug.Log(userInput.options[userInput.value].text);
        if (sol && userInput.options[userInput.value].text == "T")
            feedback.text = "Correct!";
        else if (!sol && userInput.options[userInput.value].text == "F")
            feedback.text = "Correct!";
        else
            feedback.text = "Incorrect!";

    }
}
