using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollider : MonoBehaviour
{
    public KeyCode UserInput;
    public float HitRange = 7.6f;
    public float HalfwayRange = 3.8f;
    public float BadMin = 1.0f;
    public float GoodMin = 0.5f;
    public float GreatMin = 0.05f;

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
        Debug.DrawRay(lineOfSight, direction * HitRange, Color.red);

        // Creates a raycast using an object's origin point, the direction the ray cast is facing, and length of the raycast
        RaycastHit2D hit = Physics2D.Raycast(lineOfSight, direction, HitRange);

        if (hit.collider != null) // Fixes null reference errors
        {
            Symbol symbol = hit.collider.gameObject.GetComponent<Symbol>();
            if (hit.distance <= HitRange && symbol != null)
            {
                // UserInput is adjustable through unity's inspector
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
        if (distance <= HalfwayRange && distance > BadMin)
            return Enums.HitType.Bad;
        else if (distance <= BadMin && distance > GoodMin)
            return Enums.HitType.Good;
        else if (distance <= GoodMin && distance > GreatMin)
            return Enums.HitType.Great;
        else if (distance <= GreatMin)
            return Enums.HitType.Perfect;
        else
            return Enums.HitType.Miss;
    }
}
