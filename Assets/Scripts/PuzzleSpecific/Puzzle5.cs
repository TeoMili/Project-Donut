using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Puzzle5 : MonoBehaviour
{
    public TMP_Text piece1;
    public TMP_Text piece2;
    public TMP_Text piece3;
    public TMP_Text piece4;
    public TMP_Text piece5;

    public Image place1;
    public Image place2;
    public Image place3;
    public Image place4;
    public Image place5;

    public TMP_Text feedbackElement;



    void Update()
    {
        //check if all the puzzle pieces are in the correct spots
        int ok = 1;

        if (piece1.transform.parent.gameObject.GetComponent<Image>() != place1 || piece2.transform.parent.gameObject.GetComponent<Image>() != place2 || piece3.transform.parent.gameObject.GetComponent<Image>() != place3 || piece4.transform.parent.gameObject.GetComponent<Image>() != place4 || piece5.transform.parent.gameObject.GetComponent<Image>() != place5)
        {
            ok = 0;
        }

        if (ok == 1)
        {
            feedbackElement.text = "Puzzle Complete!";
        }
    }
}
