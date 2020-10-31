using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class DialogueLine
{
    public DialogueType type = DialogueType.Dialogue;
    [Multiline] public string text = "";
    public int nextLineIndex = 0;
    public List<int> ChoiceIdx = new List<int>();
    public enum DialogueType{   Dialogue = 0, Choice = 1, GoodEnd = 2, BadEnd = 3}
}
