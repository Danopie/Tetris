using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Group : MonoBehaviour
{
    private float fallInterval = Difficulty.FallInterval; 
    private float lastFall = 0;
    private const float timeToCharge = 0.1f;
    private float chargeTimer = 0.0f;

    public delegate void GameOver();
    public static event GameOver OnGameOver;

    private bool isRuning = false;
    // Use this for initialization
    void Start()
    {
    }

    public void Run()
    {
        isRuning = true;
        if (!isValidGridPos())
        {
            OnGameOver();
            Destroy(gameObject);
        }
    }
    //Reset the fall interval when the scene reloads
    public void ResetFallInterval()
    {
        fallInterval = Difficulty.FallInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRuning)
            return;
        // Move Left
        if (playerMoveLeft())
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // See if valid
            if (isValidGridPos())
                // Its valid. Update grid.
                updateGrid();
            else
                // Its not valid. revert.
                transform.position += new Vector3(1, 0, 0);
        }

        // Move Right
        else if (playerMoveRight())
        {
            // Modify position
            transform.position += new Vector3(1, 0, 0);

            // See if valid
            if (isValidGridPos())
                // It's valid. Update grid.
                updateGrid();
            else
                // It's not valid. revert.
                transform.position += new Vector3(-1, 0, 0);
        }

        else if (playerRotate())
        {
            transform.Rotate(0, 0, -90);
            // See if valid
            if (!isValidGridPos())
                if (!moveToValidGridPos())   // I dont know where to go
                    transform.Rotate(0, 0, 90);

            // update grid
            updateGrid();

        }

        else if (playerMoveDown())
        {
            if(fallInterval >= Difficulty.IntervalLimit)
                fallInterval -= Difficulty.IntervalDecreaseAmount;
            // Modify position
            transform.position += new Vector3(0, -1, 0);
            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid();
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Impact effect
                ShowImpactEffect();

                // Clear filled horizontal lines
                Grid.deleteFullRows();

                // Spawn next Group
                FindObjectOfType<Spawner>().spawnNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }
    }

    private bool moveToValidGridPos()
    {
        Vector3 backup = transform.position;

        // try left
        transform.position += new Vector3(-1, 0, 0);
        if (isValidGridPos())
            return true;

        // left again
        transform.position += new Vector3(-1, 0, 0);
        if (isValidGridPos())
            return true;

        // try right
        transform.position += new Vector3(4, 0, 0);
        if (isValidGridPos())
            return true;
        // right again
        transform.position += new Vector3(1, 0, 0);
        if (isValidGridPos())
            return true;

        // if cant find valid position
        transform.position = backup;
        return false;
    }

    private void ShowImpactEffect()
     {
         GameObject metalImpact = Instantiate(Resources.Load("MetalImpact")) as GameObject;
         metalImpact.transform.position = new Vector3(
                                                        transform.position.x,
                                                        transform.position.y - 1,
                                                        transform.position.z);
     }
    bool playerMoveLeft()
    {
        if ((Input.GetKey(KeyCode.LeftArrow) && isDelayed()) ||        // hold
            Input.GetKeyDown(KeyCode.LeftArrow))                       // press
        {
            ResetChargeTimer();
            return true;
        }
        return false;
    }
    bool playerMoveRight()
    {
        if ((Input.GetKey(KeyCode.RightArrow) && isDelayed()) ||        // hold
            Input.GetKeyDown(KeyCode.RightArrow))                       // press
        {
            ResetChargeTimer();
            return true;
        }
        return false;
    }
    bool playerMoveDown()
    {
        if (((Input.GetKey(KeyCode.DownArrow) || Time.time - lastFall >= fallInterval) && isDelayed()) ||           // hold
            (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= fallInterval)                            // press
            )                                               
        {
            ResetChargeTimer();
            return true;
        }
        return false;
    }
    bool playerRotate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && this.name !="Block O(Clone)")      //Press and not block O              
        {
            ResetChargeTimer();
            return true;
        }
        return false;
    }

    void ResetChargeTimer()
    {
        chargeTimer = 0f;
    }

    bool isDelayed()
    {
        chargeTimer += Time.deltaTime;
        if (chargeTimer >= timeToCharge)
        {
            ResetChargeTimer();
            return true;
        }
        return false;
    }

    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);

            // Not inside Border?
            if (!Grid.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Grid.grid[(int)v.x, (int)v.y] != null)
                if(Grid.grid[(int)v.x, (int)v.y].parent != transform)
                    return false;
        }
        return true;
    }

    public static bool isValidGridPos(Transform transform)
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);

            // Not inside Border?
            if (!Grid.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Grid.grid[(int)v.x, (int)v.y] != null)
                if (Grid.grid[(int)v.x, (int)v.y].parent != transform)
                    return false;
        }
        return true;
    }

    void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Grid.h; ++y)
            for (int x = 0; x < Grid.w; ++x)
                if (Grid.grid[x, y] != null)
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }

    static public void updateGrid(Transform transform)
    {
        // Remove old children from grid
        for (int y = 0; y < Grid.h; ++y)
            for (int x = 0; x < Grid.w; ++x)
                if (Grid.grid[x, y] != null)
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
