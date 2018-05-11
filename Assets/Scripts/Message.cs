using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour {

    [Tooltip("Content of the message. Case does not matter.")]
    public string content;
    public Queue<List<Symbol>> encodedContent;

    public string RemoveContentWhiteSpace(string content)
    {
        return content.Replace(" ", null);
    }
}
