using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInstance : MonoBehaviour
{
    [SerializeField] NoteSelection noteSelection = default;

    public void NoteEvent()
    {
        noteSelection.SpawnNote();
    }
}
