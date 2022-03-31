using System;
using UnityEngine;

public class DataTranslator
{
    private static string LV1SCORE_TAG = "[LV1SCORE]";
    private static string LV2SCORE_TAG = "[LV2SCORE]";
    private static string LV3SCORE_TAG = "[LV3SCORE]";
    private static string LEVEL_TAG = "[LEVEL]";


    public static int DataToScore(string data)
    {
        return (int.Parse(DataToValue(data, LV1SCORE_TAG)) + int.Parse(DataToValue(data, LV2SCORE_TAG)) + int.Parse(DataToValue(data, LV3SCORE_TAG)));
    }


    public static int DataToLv1Score(string data)
    {
        return int.Parse(DataToValue(data, LV1SCORE_TAG));
    }

    public static int DataToLv2Score(string data)
    {
        return int.Parse(DataToValue(data, LV2SCORE_TAG));
    }


    public static int DataToLv3Score(string data)
    {
        return int.Parse(DataToValue(data, LV3SCORE_TAG));
    }

    public static int DataToLevel(string data)
    {
        return int.Parse(DataToValue(data, LEVEL_TAG));
    }


    public static string DataToValue(string data, string tag)
    {
        string[] parts = data.Split('/');
        foreach (string part in parts)
        {
            if (part.StartsWith(tag))
            {
                return part.Substring(tag.Length);
                
            }
        }
        Debug.LogError(tag + "not found in " + data);
        return "";
    }

    public static string ValuesToData(int score1, int score2, int score3, int level)
    {
        return LV1SCORE_TAG + score1 + '/' + LV2SCORE_TAG + score2 + '/' + LV3SCORE_TAG + score3 + '/' + LEVEL_TAG + level;
    }

}
