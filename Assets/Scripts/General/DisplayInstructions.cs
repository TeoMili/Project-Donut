using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInstructions : MonoBehaviour
{

    public GameObject instructions;
    public GameObject puzzle;

    private bool listening = true;


    void Start()
    {
        instructions.SetActive(true);
        puzzle.SetActive(false);
        listening = true;
    }

    void Update()
    {
        //listen for mouse click
        if (listening && Input.GetMouseButtonDown(0))
        {
            instructions.SetActive(false);
            puzzle.SetActive(true);
            listening = false;
        }
    }
}
