using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip AudioClipHit;
    public AudioClip AudioClipMiss; // TODO: Change the AudioSource clip in the inspector

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(Enums.HitType type)
    {
        switch (type)
        {
            case Enums.HitType.Miss:
                audioSource.clip = AudioClipMiss;
                break;
            default:
                audioSource.clip = AudioClipHit;
                break;
        }

        audioSource.Play();
    }

    public bool SoundIsPlaying()
    {
        return audioSource.isPlaying;
    }
}
