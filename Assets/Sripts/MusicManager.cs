using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip musicStart;
    [SerializeField] private AudioClip musicBad;
    [SerializeField] private AudioClip musicEnd;

    public void PlayStart()
    {
        source.Stop();
        source.clip = musicStart;
        source.Play();
    }

    public void PlayBad()
    {
        source.Stop();
        source.clip = musicBad;
        source.Play();
    }

    public void PlayEnd()
    {
        source.Stop();
        source.clip = musicEnd;
        source.Play();
    }
}