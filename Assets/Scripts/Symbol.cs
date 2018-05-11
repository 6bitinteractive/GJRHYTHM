using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioManager))]

public class Symbol : MonoBehaviour
{
    public enum Type
    {
        Dot,
        Dash,
    }

    public Type type;

    // Avoids being detected by the raycast twice(?)
    // and also being considered a miss even when the symbol has been hit by the player
    public bool hit = false;

    private AudioManager audioManager;

    void Start()
    {
        audioManager = GetComponent<AudioManager>();
    }

    public void Play()
    {
        audioManager.Play();
    }

    public void PlayMiss()
    {
        audioManager.PlayMiss();
    }

    public void Hide()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public IEnumerator Deactivate()
    {
        yield return new WaitWhile(() => audioManager.SoundIsPlaying());
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log (other.gameObject.name);
        if (other.gameObject.CompareTag("MissCollider") && !hit)
        {
            Debug.Log("Hit: Miss");

            PlayMiss();
            Hide(); // Hide the object for now because SetActive(false) deactivates all the object's components (consequence: audio doesn't play)
            StartCoroutine(Deactivate());
        }
    }
}

// https://docs.unity3d.com/ScriptReference/AudioSource.html
// http://answers.unity.com/answers/1159508/view.html
