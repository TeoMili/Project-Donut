using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenOptions : MonoBehaviour
{
    void Update()
    {
        //listen for escape key event and display options menu if pressed 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //ManageScenes.Instance.blockRepop = true;
            ManageScenes.Instance.previousScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(1);
        }

        if (Input.GetMouseButtonDown(1))
            ManageScenes.Instance.UnlockMouse();
    }
}
