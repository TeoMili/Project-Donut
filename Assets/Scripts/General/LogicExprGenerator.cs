using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicExprGenerator : MonoBehaviour
{

    public string GenerateExpression(int l, int L, string[] operators, string[] variables)
    {
        //l is the current length of the expression
        //L is the target length of the expression

        string var1, var2, op;

        var1 = variables[Random.Range(0, variables.Length)];
        var2 = variables[Random.Range(0, variables.Length)];
        op = operators[Random.Range(0, operators.Length)];
        l++;

        if(l < L)
        {
            //expand a random variable (either var1 or var2)
            if (decision())
            {
                //expand var1
                var1 = GenerateExpression(l, L, operators, variables);
            }
            else{
                //expand var2 
                var2 = GenerateExpression(l, L, operators, variables);
            }
        }

        //decide whether to negate the variables or not 
        if (decision())
        {
            var1 = "7" + "," + var1;
        }
        if (decision())
        {
            var2 = "7" + "," + var2;
        }


        //check if the algorithm is at the first block, if so don't use ()
        if(l == 1)
        {
            return var1 + "," + op + "," + var2;
        }

        return "( " + "," + var1 + "," + op + "," + var2 + "," + " )";
    }

    public void EvaluateExpression(ref Stack<string> operations, ref Stack<bool> values)
    { 
        bool var1, var2;
        string op;

        //go through the stacks until there is only one value in values and compute operations


        do
        {
            op = (string)operations.Pop();
            if (op == "(")
            {
                //call function recurssively
                EvaluateExpression(ref operations, ref values);
            }
            else if (op == "7")
            {
                if (operations.Count > 0 && operations.Peek() == "(")
                {
                    operations.Pop();
                    //call function recurssively
                    EvaluateExpression(ref operations, ref values);
                    //negate result
                    var1 = (bool)values.Pop();
                    values.Push(!var1);
                }
                else
                {
                    //negate variable on top of the stack
                    var1 = (bool)values.Pop();
                    values.Push(!var1);
                }
            }
            else if (op == "V")
            {
                var1 = (bool)values.Pop();
                if (operations.Count > 0 && operations.Peek() == "(")
                {
                    operations.Pop();
                    //call function
                    EvaluateExpression(ref operations, ref values);
                    //get variable on top of the stack and compute result
                    var2 = (bool)values.Pop();
                    values.Push(Or(var1, var2));
                }
                else if (operations.Count > 0 && operations.Peek() == "7")
                {
                    operations.Pop();
                    var2 = !(bool)values.Pop();
                    values.Push(Or(var1, var2));
                }
                else
                {
                    var2 = (bool)values.Pop();
                    values.Push(Or(var1, var2));
                }
            }
            else if (op == "^")
            {
                var1 = (bool)values.Pop();
                if (operations.Count > 0 && operations.Peek() == "(")
                {
                    operations.Pop();
                    //call function
                    EvaluateExpression(ref operations, ref values);
                    //get variable on top of the stack and compute result
                    var2 = (bool)values.Pop();
                    values.Push(And(var1, var2));
                }
                else if (operations.Count > 0 && operations.Peek() == "7")
                {
                    operations.Pop();
                    var2 = !(bool)values.Pop();
                    values.Push(And(var1, var2));
                }
                else
                {
                    var2 = (bool)values.Pop();
                    values.Push(And(var1, var2));
                }
            }
            else if (op == "->")
            {
                var1 = (bool)values.Pop();
                if (operations.Count > 0 && operations.Peek() == "(")
                {
                    operations.Pop();
                    //call function
                    EvaluateExpression(ref operations, ref values);
                    //get variable on top of the stack and compute result
                    var2 = (bool)values.Pop();
                    values.Push(Imp(var1, var2));
                }
                else if (operations.Count > 0 && operations.Peek() == "7")
                {
                    operations.Pop();
                    var2 = !(bool)values.Pop();
                    values.Push(Imp(var1, var2));
                }
                else
                {
                    var2 = (bool)values.Pop();
                    values.Push(Imp(var1, var2));
                }
            }
            else if (op == "<-")
            {
                var1 = (bool)values.Pop();
                if (operations.Count > 0 && operations.Peek() == "(")
                {
                    operations.Pop();
                    //call function
                    EvaluateExpression(ref operations, ref values);
                    //get variable on top of the stack and compute result
                    var2 = (bool)values.Pop();
                    values.Push(RevImp(var1, var2));
                }
                else if (operations.Count > 0 && operations.Peek() == "7")
                {
                    operations.Pop();
                    var2 = !(bool)values.Pop();
                    values.Push(RevImp(var1, var2));
                }
                else
                {
                    var2 = (bool)values.Pop();
                    values.Push(RevImp(var1, var2));
                }
            }




        } while (op != ")" && operations.Count != 0);

    }

    public bool Or(bool x, bool y)
    {
        if (x || y)
            return true;
        return false;
    }

    public bool And(bool x, bool y)
    {
        if (x && y)
            return true;
        return false;
    }

    public bool Imp(bool x, bool y)
    {
        if (x && !y)
            return false;
        return true;
    }

    public bool RevImp(bool x, bool y)
    {
        if (y && !x)
            return false;
        return true;
    }

    public bool decision()
    {
        int i = Random.Range(0, 2);
        if (i == 0)
            return false;
        else
            return true;
    }


       
}



