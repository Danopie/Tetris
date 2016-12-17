using UnityEngine;
using System.Collections;
using System;

public class Group : MonoBehaviour
{

    float lastFall = 0;
    private const float timeToCharge = 0.1f;
    private float chargeTimer = 0.0f;

    // Use this for initialization
    void Start()
    {
        if (!isValidGridPos())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move Left
        if (canMoveLeft())
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
        else if (canMoveRight())
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

        else if (canRotate())
        {
            transform.Rotate(0, 0, -90);

            // See if valid
            if (isValidGridPos())
                // It's valid. Update grid.
                updateGrid();
            else
                // It's not valid. revert.
                transform.Rotate(0, 0, 90);
        }

        else if (canMoveDown())
        {
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

                // Clear filled horizontal lines
                Grid.deleteFullRows();

                // Impact effect
                ShowImpactEffect();

                // Spawn next Group
                FindObjectOfType<Spawner>().spawnNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
            if (Time.time - lastFall >= 1)
            {
                transform.position += new Vector3(0, -1, 0);

                if (isValidGridPos())
                {
                    // It's valid. Update grid.
                    updateGrid();
                }
                else
                {
                    // It's not valid. revert.
                    transform.position += new Vector3(0, 1, 0);

                    // Clear filled horizontal lines
                    Grid.deleteFullRows();

                    // Spawn next Group
                    FindObjectOfType<Spawner>().spawnNext();

                    // Disable script
                    enabled = false;
                }
            }
        }
    }

    private void ShowImpactEffect()
    {
        GameObject metalImpact = Instantiate(Resources.Load("MetalImpact")) as GameObject;
        metalImpact.transform.position = new Vector3(
                                                       transform.position.x,
                                                       transform.position.y - 1,
                                                       transform.position.z);
    }

    bool canMoveLeft()
    {
        if ((Input.GetKey(KeyCode.LeftArrow) && isDelayed()) ||        // hold
            Input.GetKeyDown(KeyCode.LeftArrow))                       // press
        {
            ResetChargeTimer();
            return true;
        }
        return false;
    }

    bool canMoveRight()
    {
        if ((Input.GetKey(KeyCode.RightArrow) && isDelayed()) ||        // hold
            Input.GetKeyDown(KeyCode.RightArrow))                       // press
        {
            ResetChargeTimer();
            return true;
        }
        return false;
    }
    bool canMoveDown()
    {
        if ( ((Input.GetKey(KeyCode.DownArrow) || Time.time - lastFall >= 1) && isDelayed()) ||           // hold
            (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1)                            // press
            )                         
        {
            ResetChargeTimer();
            return true;
        }
        return false;
    }
    bool canRotate()
    {
        if ((Input.GetKey(KeyCode.UpArrow) && isDelayed()) ||        // hold
            Input.GetKeyDown(KeyCode.UpArrow))                       // press
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
            if (Grid.grid[(int)v.x, (int)v.y] != null &&
                Grid.grid[(int)v.x, (int)v.y].parent != transform)
            {
                return false;
            }
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
}
