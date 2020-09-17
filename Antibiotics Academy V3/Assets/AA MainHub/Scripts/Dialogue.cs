using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name; // name of npc

    [TextArea(3, 10)]
    public string[] sentences; // text to appear in the dialogue box
}