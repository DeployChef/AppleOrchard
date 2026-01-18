using System;
using UnityEngine;
using UnityEngine.Playables;

public class Root : MonoBehaviour
{
    public PlayableDirector introDirector;
    public DialogTrigger introDialog;

    private void Awake()
    {
        Debug.Log("Start");
        //introDialog.TriggerDialog();
    }

    private void Start()
    {
        introDirector.Play();
    }
}
