using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MorseCode))]
[RequireComponent(typeof(Message))]

public class StageManager : MonoBehaviour {

    public Text characterPanel;

    [Tooltip("x-Position where a symbol will be dropped. Specified from left to right.")]
    public float[] laneDropPositions = new float[]{ -1.97f, -1.0f, -0.0f, 1.0f, 2.0f };

    [Range(1, 5)]
    public int stage = 1;


    // This should be taken from a config... json data?
    // or temporarily create a class and create structs for each stage???
    private Message message;


    private bool timerReset = true;
    private bool getNewCharacter = true;
    private int currentCharacter = 0;
    private int currentSymbol = 0;
    private List<Symbol> symbolsToSpawn;
    private GameObject spawnedObjects;
    private string msg;

    void Start ()
    {
        if (characterPanel == null)
            Debug.Log("characterPanel has no Text object associated with it.");

        message = GetComponent<Message>();
        message.content = "HELLO MY NAME IS JOHN I AM FROM TEXAS";

        // Remove whitespace in message; placed in separate variable to keep original message
        msg = message.RemoveContentWhiteSpace(message.content);
        message.encodedContent = MorseCode.Instance.Encode(msg);

        // Create a container for spawned objects
        spawnedObjects = new GameObject("Spawned Symbols");
        spawnedObjects.transform.parent = transform;

        symbolsToSpawn = new List<Symbol>();
    }

	void Update ()
    {
        if (StageEnd())
        {
            // Stop spawning
            timerReset = false;

            StartCoroutine(NextStage());
        }

        if (timerReset)
        {
            // Sequential order
            if (getNewCharacter)
            {
                // Display current character at the top panel
                characterPanel.text = msg[currentCharacter].ToString();

                // Get current character's code
                symbolsToSpawn = message.encodedContent.Dequeue();

                getNewCharacter = false;
            }

            StartCoroutine(SpawnSymbol());
            timerReset = false;
        }
    }

    private IEnumerator NextStage()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Reached the end of the stage.");
        Debug.Break();
    }

    private IEnumerator SpawnSymbol()
    {
        // Waits for a certain amount of time then returns something
        yield return new WaitForSeconds(1.0f);

        // Spawn symbol one at a time
        if (currentSymbol < symbolsToSpawn.Count)
        {
            // Spawn at a random lane
            int randomLane = UnityEngine.Random.Range(0, laneDropPositions.Length);
            GameObject spawnedSymbol = Instantiate(symbolsToSpawn[currentSymbol].gameObject, new Vector2(laneDropPositions[randomLane], 3.3f), Quaternion.identity);
            spawnedSymbol.transform.parent = spawnedObjects.transform;

            // Move index to next symbol
            currentSymbol++;
        }
        else
        {
            // Time to move to the next character
            getNewCharacter = true;
            currentCharacter++;

            // Reset index counter
            currentSymbol = 0;
        }

        timerReset = true;
    }

    private bool StageEnd()
    {
        return message.encodedContent.Count == 0 && currentSymbol == symbolsToSpawn.Count;
    }
}

// https://www.youtube.com/watch?v=kyp3Ks5a6to