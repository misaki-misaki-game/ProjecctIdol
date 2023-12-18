using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDestroy : MonoBehaviour
{
    UiManager uiManager;

    void OnBecameInvisible()
    {
        GameObject.Destroy(this.gameObject);
    }

}

