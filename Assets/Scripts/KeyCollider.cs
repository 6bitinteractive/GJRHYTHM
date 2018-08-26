using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollider : MonoBehaviour
{
    public KeyCode UserInput;
    public float Range = 3.8f;

    private Vector2 direction; // Direction the raycast is going to be facing
    private Vector2 lineOfSight; // Location of the raycast's line of sight

    void Start()
    {
        // Place this at Start() since these values don't change anyway
        direction = new Vector2(0, 1);
        lineOfSight = transform.position;
    }

    void Update()
    {
        // Displays the raycast for debugging purposes
        //Debug.DrawRay(lineOfSight, direction * range, Color.red);

        // Creates a raycast using an object's origin point, the direction the ray cast is facing, and length of the raycast
        RaycastHit2D hit = Physics2D.Raycast(lineOfSight, direction, Range);

        if (hit.collider != null) // Fixes null reference errors
        {
            Symbol symbol = hit.collider.gameObject.GetComponent<Symbol>();
            if (hit.distance <= Range && symbol != null)
            {
                // userInput is adjustable through unity's inspector
                if (Input.GetKeyDown(UserInput)) // Use GetKeyDown instead of GetKey
                {
                    Enums.HitType type = DetermineHitType(hit.distance);
                    Debug.Log("HIT: " + UserInput + " | Type: " + type + " | Distance:" + hit.distance);
                    symbol.Hit(type);
                }
            }
        }
    }

    private Enums.HitType DetermineHitType(float distance)
    {
        // Note: Be aware of floating point imprecision https://docs.unity3d.com/ScriptReference/Mathf.Approximately.html
        if (distance <= Range && distance > 1.0f)
            return Enums.HitType.Bad;
        else if (distance <= 1.0f && distance > 0.5f)
            return Enums.HitType.Good;
        else if (distance <= 0.5f && distance > 0.0f)
            return Enums.HitType.Great;
        else if (Mathf.Approximately(distance, 0.0f))
            return Enums.HitType.Perfect;

        Debug.LogError("This should not print!");
        return Enums.HitType.Miss; // It can't ever be a miss though because we only check input if it's within the hit range
    }
}
