using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour {

    private static int _DifficultyLevel;
	public static int DifficultyLevel
    {
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
                case 1: return 1.0f;
                case 2: return 0.8f;
                case 3: return 0.1f;
                default: return 1.0f;
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
                default: return 0.1f;
            }
        }
    }

    public static float IntervalDecreaseAmount
    {
        get
        {
            switch (_DifficultyLevel)
            {
                case 1: return 0.00001f;
                case 2: return 0.00025f;
                case 3: return 0.0005f;
                default: return 0.00001f;
            }
        }
    }
       
}
