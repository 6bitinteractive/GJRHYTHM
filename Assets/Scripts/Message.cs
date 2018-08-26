using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "transMISSION/Create Message", fileName = "Message00", order = 0)]
public class Message : ScriptableObject
{
    [Tooltip("Content of the message. Case does not matter but avoid punctuation marks for now.")]
    public string Content;

    public string RemoveWhiteSpace(string content)
    {
        return content.Replace(" ", null);
    }
}
