using UnityEngine;
using System.Collections;

public class ScoreRow
{
    public readonly string label;
    public readonly int value;
    public readonly bool isGuaranteed;

    public ScoreRow (string label, int value, bool isGuaranteed)
    {
        this.label = label;
        this.value = value;
        this.isGuaranteed = isGuaranteed;
    }
}
