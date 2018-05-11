using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCode : MonoBehaviour
{
    // Only have one instance
    static MorseCode _instance;
    public static MorseCode Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<MorseCode>();

            return _instance;
        }
    }

    [Tooltip("Symbols to represent a character. Expected values, in order: Dot, Dash.")]
    public Symbol[] symbols;

    private char[] characters;
    private List<List<Symbol>> codes;

    // Order matters. Follow the order of Array/List<> of characters/codes.
    // This is just for convinience in case you want to manually get the code of a certain character.
    // This avoids having to get the code by index.
    //public enum Character
    //{
    //    A, B, C, D, E,
    //    F, G, H, I, J,
    //    K, L, M, N, O,
    //    P, Q, R, S, T,
    //    U, V, W, X, Y,
    //    Z,
    //    ONE, TWO, THREE, FOUR, FIVE,
    //    SIX, SEVEN, EIGHT, NINE, ZERO,
    //}

    // Placed in awake to avoid null reference exceptions (characters array/codes list)
    void Awake()
    {
        Symbol dot = null;
        Symbol dash = null;

        if (symbols.Length > 2)
        {
            Debug.Log("Only two symbols are expected--dot and dash.");
        }
        else if (symbols[0].type == Symbol.Type.Dot) // Check that the first object is actually the dot symbol.
        {
            dot = symbols[0];
            dash = symbols[1];
        }
        else
        {
            Debug.Log("Check symbols and make sure the first object is the dot symbol.");
        }

        // Keep characters and codes in the same order.
        characters = new char[]
        {
            'A', 'B', 'C', 'D', 'E',
            'F', 'G', 'H', 'I', 'J',
            'K', 'L', 'M', 'N', 'O',
            'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y',
            'Z',
            '1', '2', '3', '4', '5',
            '6', '7', '8', '9', '0',
        };

        // Reference: https://commons.wikimedia.org/wiki/File:International_Morse_Code.svg
        codes = new List<List<Symbol>>
        {
            new List<Symbol> { dot, dash },                       // A
            new List<Symbol> { dash, dot, dot, dot },             // B
            new List<Symbol> { dash, dot, dash, dot },            // C
            new List<Symbol> { dash, dot, dot },                  // D
            new List<Symbol> { dot },                             // E
            new List<Symbol> { dot, dot, dash, dot },             // F
            new List<Symbol> { dash, dash, dot },                 // G
            new List<Symbol> { dot, dot, dot, dot },              // H
            new List<Symbol> { dot, dot },                        // I
            new List<Symbol> { dot, dash, dash, dash },           // J
            new List<Symbol> { dash, dot, dot, dot },             // K
            new List<Symbol> { dot, dash, dot, dot },             // L
            new List<Symbol> { dash, dash },                      // M
            new List<Symbol> { dash, dot },                       // N
            new List<Symbol> { dash, dash, dash },                // O
            new List<Symbol> { dot, dash, dash, dot },            // P
            new List<Symbol> { dash, dash, dot, dash },           // Q
            new List<Symbol> { dot, dash, dot },                  // R
            new List<Symbol> { dot, dot, dot },                   // S
            new List<Symbol> { dash },                            // T
            new List<Symbol> { dot, dot, dash },                  // U
            new List<Symbol> { dot, dot, dot, dash },             // V
            new List<Symbol> { dot, dash, dash },                 // W
            new List<Symbol> { dash, dot, dot, dash },            // X
            new List<Symbol> { dash, dot, dash, dash },           // Y
            new List<Symbol> { dash, dash, dot, dot },            // Z
            new List<Symbol> { dot, dash, dash, dash, dash },     // 1
            new List<Symbol> { dot, dot, dash, dash, dash },      // 2
            new List<Symbol> { dot, dot, dot, dash, dash },       // 3
            new List<Symbol> { dot, dot, dot, dot, dash },        // 4
            new List<Symbol> { dot, dot, dot, dot, dot },         // 5
            new List<Symbol> { dash, dot, dot, dot, dot },        // 6
            new List<Symbol> { dash, dash, dot, dot, dot },       // 7
            new List<Symbol> { dash, dash, dash, dot, dot },      // 8
            new List<Symbol> { dash, dash, dash, dash, dot },     // 9
            new List<Symbol> { dash, dash, dash, dash, dash },    // 0
        };
    }

    public Queue<List<Symbol>> Encode(string message)
    {
        Queue<List<Symbol>> encodedMessage = new Queue<List<Symbol>>();
        char[] msg = message.ToCharArray();

        for (int i = 0; i < msg.Length; i++)
        {
            encodedMessage.Enqueue(GetCharacterSymbols( GetIndex(msg[i]) ));
        }

        return encodedMessage;
    }

    int GetIndex(char character)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            // Character's case should not matter since we always check character in upper case
            if (char.ToUpperInvariant(character) == characters[i])
                return i;
        }

        Debug.Log("Did not find character match.");
        return 0;
    }

    //Character GetCharacterIndex(char character)
    //{
    //    int characterIndex = GetIndex(character);
    //    return (Character)characterIndex;
    //}

    List<Symbol> GetCharacterSymbols(int index)
    {
        return codes[index];
    }

    //List<Symbol> GetCharacterSymbols(Character character)
    //{
    //    return codes[(int)character];
    //}
}
