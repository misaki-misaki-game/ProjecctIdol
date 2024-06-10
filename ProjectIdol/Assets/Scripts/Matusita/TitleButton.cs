using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    [SerializeField] GameObject puzzleTutorialPanel;
    [SerializeField] GameObject rhythmTutorialPanel;

    public void OpenPuzzleTutorial()
    {
        puzzleTutorialPanel.SetActive(true);
    }
    public void ClosePuzzleTutorial()
    {
        puzzleTutorialPanel.SetActive(false);
    }
    public void OpenRhythmTutorial()
    {
        rhythmTutorialPanel.SetActive(true);
    }
    public void CloseRhythmTutorial()
    {
        rhythmTutorialPanel.SetActive(false);
    }
}