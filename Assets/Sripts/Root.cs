using System;
using UnityEngine;

public class Root : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Start");
        DialogueManager.Instance.StartDialogue();
    }
}
