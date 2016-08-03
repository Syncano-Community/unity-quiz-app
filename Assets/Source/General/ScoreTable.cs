using UnityEngine;
using System.Collections;

public class ScoreTable
{
    public static readonly ScoreRow[] rows = {
        /* 01 */ new ScoreRow("100", 100, false),
        /* 02 */ new ScoreRow("200", 200, false),
        /* 03 */ new ScoreRow("300", 300, false),
        /* 04 */ new ScoreRow("500", 500, false),
        /* 05 */ new ScoreRow("1 000", 1000, true),
        /* 06 */ new ScoreRow("2 000", 2000, false),
        /* 07 */ new ScoreRow("4 000", 4000, false),
        /* 08 */ new ScoreRow("8 000", 8000, false),
        /* 09 */ new ScoreRow("16 000", 16000, false),
        /* 10 */ new ScoreRow("32 000", 32000, true),
        /* 11 */ new ScoreRow("64 000", 64000, false),
        /* 12 */ new ScoreRow("125 000", 125000, false),
        /* 13 */ new ScoreRow("250 000", 250000, false),
        /* 14 */ new ScoreRow("500 000", 500000, false),
        /* 15 */ new ScoreRow("1 MILION", 1000000, true),
    };

    public static ScoreRow GetRow(int index)
    {
        return rows[index];
    }

    public static int GetRowCount()
    {
        return rows.Length;
    }

    public static ScoreRow GetLastGuaranteed(int index)
    {
        for (int i = index; i >= 0; i--)
        {
            if (rows[index].isGuaranteed)
                return rows[index];
        }

        return null;
    }
}
