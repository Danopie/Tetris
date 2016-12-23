using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour {

    static int _DifficultyLevel;

    static float DifficultyDecreaseAmount;

    static Difficulty()
    {
        _DifficultyLevel = 1;
        DifficultyDecreaseAmount = 0f;
    }

	public static int DifficultyLevel
    {
        get
        {
            return _DifficultyLevel;
        }
        set
        {
            if (value == 1 || value == 2 || value == 3)
                _DifficultyLevel = value;
            else
                _DifficultyLevel = 0;
        }
    }

    public static float FallInterval
    {
        get
        {
            switch(_DifficultyLevel)    
            {
                case 1: return 1f + DifficultyDecreaseAmount;
                case 2: return 0.8f + DifficultyDecreaseAmount;
                case 3: return 0.6f + DifficultyDecreaseAmount;
                default: return 0f;
            }
        }
    }
    
    public static float IntervalLimit
    {
        get
        {
            switch (_DifficultyLevel)
            {
                case 1: return 0.1f;
                case 2: return 0.75f;
                case 3: return 0.05f;
                default: return 0f;
            }
        }
    }

    public static float IntervalDecreaseAmount
    {
        get
        {
            switch (_DifficultyLevel)
            {
                case 1: return 0.005f;
                case 2: return 0.0025f;
                case 3: return 0.0015f;
                default: return 0f;
            }
        }
    }

    public static float TimeLeft
    {
        get
        {
            switch (_DifficultyLevel)
            {
                case 1: return 180f;
                case 2: return 150f;
                case 3: return 120f;
                default: return 0f;
            }
        }
    }

    public static int ScoreToWin
    {
        get
        {
            return 10;
        }
    }

    public static void Lower()
    {
        switch (_DifficultyLevel)
        {
            case 1: DifficultyDecreaseAmount += 0.25f; break;
            case 2: DifficultyDecreaseAmount += 0.15f; break;
            case 3: DifficultyDecreaseAmount += 0.10f; break;
            default: DifficultyDecreaseAmount += 0f; break;
        }
    }

    public static void ResetVariables()
    {
        DifficultyDecreaseAmount = 0f;
    }

    public static int ScorePenalty
    {
        get
        {
            switch(_DifficultyLevel)
            {
                case 1: return 2;
                case 2: return 3;
                case 3: return 4;
                default: return 0;
            }
        }
    }
}
