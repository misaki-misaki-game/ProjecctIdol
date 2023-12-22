using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ChangePanel : MonoBehaviour
{
    public GameObject titlePuzzle;
    public GameObject puzzleTutorial;
    public GameObject titleRhythm;
    public GameObject rhythmTutorial;

    public void Switch_Puzzle()
    {
        if (titlePuzzle.activeSelf)
        {
            titlePuzzle.SetActive(false);
            puzzleTutorial.SetActive(true);
        }
    }

    public void Switch_Rhythm()
    {
        if(titleRhythm.activeSelf)
        {
            titleRhythm.SetActive(false);
            rhythmTutorial.SetActive(true);
        }
    }

}
