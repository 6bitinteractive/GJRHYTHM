using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioManager))]

public class Symbol : MonoBehaviour
{
    public enum Type
    {
        Dot, Dash
    }

    public Type SymbolType;

    // Avoids being detected by the raycast twice(?)
    // and also being considered a miss even when the symbol has been hit by the player
    private bool isHit;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = GetComponent<AudioManager>();
        isHit = false;
    }

    public void Hit(Enums.HitType hitType)
    {
        // Only let the symbol be hit once
        if (isHit) return;

        // Set to true when player does hit the Symbol
        isHit = true;

        // Play audio depending on the type of hit
        audioManager.Play(hitType);

        StartCoroutine(Deactivate());
    }

    private IEnumerator Deactivate()
    {
        // Hide the object for now because SetActive(false) deactivates all the object's components (consequence: audio doesn't play)
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitWhile(() => audioManager.SoundIsPlaying());
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log (other.gameObject.name);
        if (other.gameObject.CompareTag("MissCollider") && !isHit)
        {
            Debug.Log("Hit: Miss");
            Hit(Enums.HitType.Miss);
        }
    }

}

// https://docs.unity3d.com/ScriptReference/AudioSource.html
// http://answers.unity.com/answers/1159508/view.html
