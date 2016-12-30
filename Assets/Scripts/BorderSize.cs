using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderSize : MonoBehaviour {

    static private Vector2[] listSize = new Vector2[] { new Vector2(10f, 25f) , new Vector2(15f, 30f), new Vector2(20f, 35f) };
    static public int sizeOption = 0;

    static private void initBorderLeft()
    {
        GameObject borderLeft = GameObject.Find("Border Left");

        borderLeft.transform.localScale = new Vector3(5, getSize.y / 0.625f, 1);

        borderLeft.transform.position += new Vector3(0, (getSize.y - 25) / 2, 0); 
    }

    static private void initBorderBottom()
    {
        GameObject borderBot = GameObject.Find("Border Bottom");

        borderBot.transform.localScale = new Vector3(5, (getSize.x + 0.5f) / 0.625f, 1);

        borderBot.transform.position += new Vector3(((getSize.x - 10.5f) + 0.5f) / 2f, 0, 0);
    }

    static private void initBorderRight()
    {
        GameObject borderLeft = GameObject.Find("Border Right");

        borderLeft.transform.localScale = new Vector3(5, getSize.y / 0.625f, 1);

        borderLeft.transform.position += new Vector3(((getSize.x - 10.5f) + 0.5f), (getSize.y - 25) / 2, 0);
    }

    static public void initBorder()
    {
        initBorderLeft();
        initBorderBottom();
        initBorderRight();
    }

    static public Vector2 getSize
    {
        get
        {
            return listSize[sizeOption];
        }
    }
}
