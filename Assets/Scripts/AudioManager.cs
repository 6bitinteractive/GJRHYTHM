using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip audioClipHit;
    public AudioClip audioClipMiss; // TODO: Change the AudioSource clip in the inspector

    private AudioSource audioSource;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }

	public void Play()
    {
        audioSource.clip = audioClipHit;
        audioSource.Play();
    }

    public void PlayMiss()
    {
        audioSource.clip = audioClipMiss;
        audioSource.Play();
    }

    public bool SoundIsPlaying()
    {
        return audioSource.isPlaying;
    }
}
