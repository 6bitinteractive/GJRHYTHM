using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCode : MonoBehaviour
{
    [Tooltip("Symbols to represent a character. Expected values, in order: Dot, Dash.")]
    public Symbol[] Symbols;

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

    // Placed in Awake() to avoid null reference exceptions (characters array/codes list)
    void Awake()
    {
        Symbol dot = null;
        Symbol dash = null;

        if (Symbols.Length > 2)
        {
            Debug.LogError("Only two symbols are expected--dot and dash.");
        }
        else if (Symbols[0].SymbolType == Symbol.Type.Dot) // Check that the first object is actually the dot symbol.
        {
            dot = Symbols[0];
            dash = Symbols[1];
        }
        else
        {
            Debug.LogError("Check symbols and make sure the first object is the dot symbol.");
        }

        // TODO: Morse code for punctuation marks?

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
            '.', '’',
        };

        // Reference: https://en.wikipedia.org/wiki/Morse_code
        codes = new List<List<Symbol>>
        {
            new List<Symbol> { dot, dash },                         // A
            new List<Symbol> { dash, dot, dot, dot },               // B
            new List<Symbol> { dash, dot, dash, dot },              // C
            new List<Symbol> { dash, dot, dot },                    // D
            new List<Symbol> { dot },                               // E
            new List<Symbol> { dot, dot, dash, dot },               // F
            new List<Symbol> { dash, dash, dot },                   // G
            new List<Symbol> { dot, dot, dot, dot },                // H
            new List<Symbol> { dot, dot },                          // I
            new List<Symbol> { dot, dash, dash, dash },             // J
            new List<Symbol> { dash, dot, dot, dot },               // K
            new List<Symbol> { dot, dash, dot, dot },               // L
            new List<Symbol> { dash, dash },                        // M
            new List<Symbol> { dash, dot },                         // N
            new List<Symbol> { dash, dash, dash },                  // O
            new List<Symbol> { dot, dash, dash, dot },              // P
            new List<Symbol> { dash, dash, dot, dash },             // Q
            new List<Symbol> { dot, dash, dot },                    // R
            new List<Symbol> { dot, dot, dot },                     // S
            new List<Symbol> { dash },                              // T
            new List<Symbol> { dot, dot, dash },                    // U
            new List<Symbol> { dot, dot, dot, dash },               // V
            new List<Symbol> { dot, dash, dash },                   // W
            new List<Symbol> { dash, dot, dot, dash },              // X
            new List<Symbol> { dash, dot, dash, dash },             // Y
            new List<Symbol> { dash, dash, dot, dot },              // Z
            new List<Symbol> { dot, dash, dash, dash, dash },       // 1
            new List<Symbol> { dot, dot, dash, dash, dash },        // 2
            new List<Symbol> { dot, dot, dot, dash, dash },         // 3
            new List<Symbol> { dot, dot, dot, dot, dash },          // 4
            new List<Symbol> { dot, dot, dot, dot, dot },           // 5
            new List<Symbol> { dash, dot, dot, dot, dot },          // 6
            new List<Symbol> { dash, dash, dot, dot, dot },         // 7
            new List<Symbol> { dash, dash, dash, dot, dot },        // 8
            new List<Symbol> { dash, dash, dash, dash, dot },       // 9
            new List<Symbol> { dash, dash, dash, dash, dash },      // 0
            new List<Symbol> { dot, dash, dot, dash, dot, dash },   // . (period)
            new List<Symbol> { dot, dash, dash, dash, dash, dot },  // ’ (apostrophe)
        };
    }

    public Queue<List<Symbol>> Encode(string message)
    {
        Queue<List<Symbol>> encodedMessage = new Queue<List<Symbol>>();
        char[] msg = message.ToCharArray();

        for (int i = 0; i < msg.Length; i++)
        {
            encodedMessage.Enqueue(GetCharacterSymbols(GetIndex(msg[i])));
        }

        return encodedMessage;
    }

    private int GetIndex(char character)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            // Character's case should not matter since we always check character in upper case
            // We still check without using ToUpperInvariant for numbers
            if (character == characters[i] || char.ToUpperInvariant(character) == characters[i])
                return i;
        }

        Debug.LogWarning("Did not find character match: " + character);
        return -1;
    }

    //private Character GetCharacterIndex(char character)
    //{
    //    int characterIndex = GetIndex(character);
    //    return (Character)characterIndex;
    //}

    private List<Symbol> GetCharacterSymbols(int index)
    {
        return codes[index];
    }

    //private List<Symbol> GetCharacterSymbols(Character character)
    //{
    //    return codes[(int)character];
    //}
}
