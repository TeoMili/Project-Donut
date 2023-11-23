using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Puzzle11 : MonoBehaviour
{
    //UI Game Objects
    public TMP_Text expressionField;
    public GameObject spawnPoint;
    public TMP_Text expressionPrefab;
    public GameObject goalPoint;
    public List<TMP_Text> correctAnswers = new List<TMP_Text>();
    public TMP_Text feedback;


    private string[] comparators = { "<", ">", "<=", ">=", "==", "!=" };
    private string[] function = { "$$", "||" };
    private string expression = "";

    private bool negated = false;

    //For generating possible answers

    //BEGINNER
    //0: negate/un-negate the entire expression (correct answer)  !()
    //1: negate/un-negate the comparators                         !()
    //2: negate/un-negate the function                            !()

    //INTERMEDIATE 
    //3: negate/un-negate the entire expression            !() !()
    //4: negate/un-negate the comparators (correct answer) !() !()
    //5: negate/un-negate the function                     !() !()

    //ADVANCED
    //6: negate/un-negate the entire expression            !(!() !())
    //7: negate/un-negate the comparators                  !(!() !())
    //8: negate/un-negate the function  (correct answer)   !(!() !())

    private string[] symbols = new string[3];
    private string[] candidates = new string[10];

    private int i;

    private int nrCandidates;

    //custom colors for the buttons
    private Color notSelected = new Color();
    private Color selected = new Color();
 

   
    void Start()
    {
        //create custom colors

        ColorUtility.TryParseHtmlString("#00ACC8", out notSelected);
        ColorUtility.TryParseHtmlString("#282878", out selected);

        //ManageScenes.Instance.difficulty = 2;

        //decide which random 2 comparators and which random function to use
        i = Random.Range(0, comparators.Length);
        symbols[0] = comparators[i];
        i = Random.Range(0, function.Length);
        symbols[1] = function[i];
        i = Random.Range(0, comparators.Length);
        symbols[2] = comparators[i];


        //initialize the number of answer candidates based on the selected difficulty
        nrCandidates = ManageScenes.Instance.difficulty * 3 + 3;

        //if the difficulty is 1 or 2 (intermediate or advanced), don't negate the expression
        if (ManageScenes.Instance.difficulty > 0)
            negated = false;
        else
            negated = decision();

        //choose 2 random integers to compare to
        int x = Random.Range(-101, 101);
        int y = Random.Range(-101, 101);

        expression = buildExpression(symbols, negated, x, y);

        //Debug.Log(expression);
        expressionField.text = expression;

        candidates = buildCandidates(symbols, negated, x, y, nrCandidates);

        //add candidates to the scene
        for(int i = 0; i <= ManageScenes.Instance.difficulty; i++)
        {
            //add three candidates for each difficulty level
            for (int j = 0; j < 3; j++)
            {
                TMP_Text childObject = Instantiate(expressionPrefab, spawnPoint.transform);
                childObject.text = candidates[i * 3 + j];

                if(i == j)
                {
                    //add element to the list of correct answers
                    correctAnswers.Add(childObject);
                }
            }

        }

    }

    void Update()
    {
        //check if the puzzle has been completed
        if(goalPoint.transform.childCount == ManageScenes.Instance.difficulty + 1)
        {
            bool ok = true;
            foreach (TMP_Text answer in correctAnswers)
                if (answer.transform.parent.gameObject != goalPoint)
                    ok = false;
            if (ok)
            {
                //give feedback that the puzzle is complete
                feedback.text = "Puzzle completed!";
            }
        }
    }

    private bool decision()
    {
        int i = Random.Range(0, 2);
        if (i == 0)
            return false;
        else
            return true;
    }

    private string opposite(string symbol)
    {
        //gives the symbol resulting from negating the given one
        switch (symbol)
        {
            case "<":
                return ">=";
 
            case ">":
                return "<=";

            case "<=":
                return ">";

            case ">=":
                return "<";

            case "==":
                return "!=";

            case "!=":
                return "==";

            case "$$":
                return "||";

            case "||":
                return "$$";
        }
        return "";
    }

    private string buildExpression(string[] symbols, bool negated, int x, int y) 
    {

        expression = "(x" + symbols[0] + x.ToString() + ")" + symbols[1] + "(y" + symbols[2] + y.ToString() + ")";

        if (negated)
            expression = "!(" + expression + ")";

        return expression;

    }

    private string[] buildCandidates(string[] symbols, bool negated, int x, int y, int nrCandidates)
    {

        string[] candidates = new string[nrCandidates];
        for(int i = 0; i < nrCandidates; i++)
        {
            if (i / 3 == 0)
            {
                if (i % 3 == 0)
                {
                    //negate entire expression
                    candidates[i] = "(x" + opposite(symbols[0]) + x.ToString() + ")" + opposite(symbols[1]) + "(y" + opposite(symbols[2]) + y.ToString() + ")";
                }
                else if (i % 3 == 1)
                {
                    //only negate the comparators
                    candidates[i] = "(x" + opposite(symbols[0]) + x.ToString() + ")" + symbols[1] + "(y" + opposite(symbols[2]) + y.ToString() + ")";
                }
                else
                {
                    //only negate the function
                    candidates[i] = "(x" + symbols[0] + x.ToString() + ")" + opposite(symbols[1]) + "(y" + symbols[2] + y.ToString() + ")";
                }

                if (!negated)
                    candidates[i] = "!(" + candidates[i] + ")";
            }
            else if(i / 3 == 1)
            {
                if (i % 3 == 0)
                {
                    //negate entire expression
                    candidates[i] =  "!(x" + opposite(symbols[0]) + x.ToString() + ")" + opposite(symbols[1]) + "!(y" + opposite(symbols[2]) + y.ToString() + ")";
                }
                else if (i % 3 == 1)
                {
                    //only negate the comparators
                    candidates[i] = "!(x" + opposite(symbols[0]) + x.ToString() + ")" + symbols[1] + "!(y" + opposite(symbols[2]) + y.ToString() + ")";

                }
                else
                {
                    //only negate the function
                    candidates[i] = "!(x" + symbols[0] + x.ToString() + ")" + opposite(symbols[1]) + "!(y" + symbols[2] + y.ToString() + ")";

                }

            }
            else
            {
                if (i % 3 == 0)
                {
                    //negate entire expression
                    candidates[i] = "!(x" + opposite(symbols[0]) + x.ToString() + ")" + opposite(symbols[1]) + "!(y" + opposite(symbols[2]) + y.ToString() + ")";
                }
                else if (i % 3 == 1)
                {
                    //only negate the comparators
                    candidates[i] = "!(x" + opposite(symbols[0]) + x.ToString() + ")" + symbols[1] + "!(y" + opposite(symbols[2]) + y.ToString() + ")";

                }
                else
                {
                    //only negate the function
                    candidates[i] = "!(x" + symbols[0] + x.ToString() + ")" + opposite(symbols[1]) + "!(y" + symbols[2] + y.ToString() + ")";
                }

                candidates[i] = "!(" + candidates[i] + ")";
            }
            
        }

        return candidates;

    }

    

}
