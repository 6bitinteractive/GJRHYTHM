using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MorseCode))]

public class StageManager : MonoBehaviour
{
    [Tooltip("Smaller number means a faster rate of spawning the symbols.")]
    public float TimeUntilNextSpawn = 1.0f;

    [Tooltip("x-Position where a symbol will be dropped. Specified from left to right.")]
    public float[] LaneDropPositions = new float[] { -1.97f, -1.0f, -0.0f, 1.0f, 2.0f };

    [Range(1, 6)]
    public int Stage = 1;

    public Text CharacterPanel;
    public Text DecodedMessagePanel;
    public Message[] Messages;

    private bool timerReset = true;
    private bool getNewCharacter = true;
    private int currentCharacter = 0;
    private int currentSymbol = 0;
    private List<Symbol> symbolsToSpawn;
    private GameObject spawnedObjects;
    private string message;
    private Queue<List<Symbol>> encodedMessage;
    private MorseCode morseCode;

    void Start()
    {
        if (CharacterPanel == null)
            Debug.LogError("characterPanel has no Text object associated with it.");

        morseCode = GetComponent<MorseCode>();

        int msgIndex = Stage - 1;
        message = Messages[msgIndex].Content;
        //DecodedMessagePanel.text = message;

        // Remove whitespace in message
        // TODO: Keep whitespace?
        message = Messages[msgIndex].RemoveWhiteSpace(message);

        // TODO: "Shuffle" message before encoding then save order;
        // this is to avoid manually setting the order and at the same time still making the order of the message remain the same after each play,
        // i.e. pseudo-predetermined
        // -- (Unimplemented) --

        encodedMessage = morseCode.Encode(message);

        // Create a container for spawned objects
        spawnedObjects = new GameObject("Spawned Symbols");
        spawnedObjects.transform.parent = transform;

        symbolsToSpawn = new List<Symbol>();
    }

    void Update()
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
                CharacterPanel.text = message[currentCharacter].ToString().ToUpperInvariant();

                // Get current character's code
                symbolsToSpawn = encodedMessage.Dequeue();

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
        yield return new WaitForSeconds(TimeUntilNextSpawn);

        // Spawn symbol one at a time
        if (currentSymbol < symbolsToSpawn.Count)
        {
            // Spawn at a random lane
            int randomLane = UnityEngine.Random.Range(0, LaneDropPositions.Length);
            GameObject spawnedSymbol = Instantiate(symbolsToSpawn[currentSymbol].gameObject, new Vector2(LaneDropPositions[randomLane], symbolsToSpawn[currentSymbol].transform.position.y), Quaternion.identity);
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
        return encodedMessage.Count == 0 && currentSymbol == symbolsToSpawn.Count;
    }
}

// Reference: https://www.youtube.com/watch?v=kyp3Ks5a6to