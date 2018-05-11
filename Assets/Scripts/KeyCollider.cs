using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollider : MonoBehaviour
{
    public KeyCode userInput;
    public float range = 3.8f;

    private Symbol symbol;

    void Update()
    {
        Vector2 direction = new Vector2(0, 1); // Direction the raycast is going to be facing
        Vector2 lineOfSight = transform.position; // Location of the raycast's line of sight

        // Creates a raycast using an object's origin point, the direction the ray cast is facing, and length of the raycast
        RaycastHit2D hit = Physics2D.Raycast(lineOfSight, direction, range);

        // Displays the raycast for debugging purposes
        //Debug.DrawRay(lineOfSight, direction * range, Color.red);

        // TODO: This if-statement is too deep ;-;
        if (hit.collider != null) // Fixes null reference errors
        {
            // NOTE: We basically assume that the object that hit the raycast is a symbol
            // In case we add other objects (ex. powerups?), add a check that the object is a symbol or play with layer collision masks(???)
            if (hit.distance <= range)
            {
                // userInput is adjustable through unity's inspector
                if (Input.GetKeyDown(userInput)) // GetKeyDown instead of GetKey
                {
                    //Debug.Log(hit.collider.name);

                    Debug.Log("HIT: " + userInput);

                    symbol = hit.collider.gameObject.GetComponent<Symbol>();

                    if (!symbol.hit)
                    {
                        symbol.hit = true;
                        symbol.Play();
                        symbol.Hide();
                        StartCoroutine(symbol.Deactivate());
                    }
                }
            }
        }
    }
}
