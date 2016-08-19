using UnityEngine;
using System.Collections;

/// <summary>
/// Represents table of scores. It's used to find player reward and to create dynamic score table in UI.
/// </summary>
public class ScoreTable
{
    /// <summary>
    /// The zero reward.
    /// </summary>
    public static readonly ScoreRow zero = new ScoreRow("$0", 0, true);

    /// <summary>
    /// The reward rows.
    /// </summary>
    public static readonly ScoreRow[] rows = {
        /* 01 */ new ScoreRow("$100", 100, false),
        /* 02 */ new ScoreRow("$200", 200, false),
        /* 03 */ new ScoreRow("$300", 300, false),
        /* 04 */ new ScoreRow("$500", 500, false),
        /* 05 */ new ScoreRow("$1,000", 1000, true),
        /* 06 */ new ScoreRow("$2,000", 2000, false),
        /* 07 */ new ScoreRow("$4,000", 4000, false),
        /* 08 */ new ScoreRow("$8,000", 8000, false),
        /* 09 */ new ScoreRow("$16,000", 16000, false),
        /* 10 */ new ScoreRow("$32,000", 32000, true),
        /* 11 */ new ScoreRow("$64,000", 64000, false),
        /* 12 */ new ScoreRow("$125,000", 125000, false),
        /* 13 */ new ScoreRow("$250,000", 250000, false),
        /* 14 */ new ScoreRow("$500,000", 500000, false),
        /* 15 */ new ScoreRow("$1 MILLION", 1000000, true),
    };

    /// <summary>
    /// Gets the reward row.
    /// </summary>
    public static ScoreRow GetRow(int index)
    {
        return rows[index];
    }

    /// <summary>
    /// Gets the reward row count.
    /// </summary>
    public static int GetRowCount()
    {
        return rows.Length;
    }

    /// <summary>
    /// Gets the score for last correct answer index;
    /// </summary>
    public static ScoreRow GetScoreForIndex(int index, bool giveUp)
    {
        if (giveUp)
        {
            if (index >= 0)
                return rows[index];
            
            return zero;
        }

        for (int i = index; i >= 0; i--)
        {
            if (rows[i].isGuaranteed)
                return rows[i];
        }

        return zero;
    }
}
