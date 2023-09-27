using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public bool lifeState;
    GameOfLife gol;
    Vector3 Vec;
    public int positionX = -1;
    public int positionY = -1;
    public Color aliveColor;
    public Color deadColor;
    public Color AliveBeforeColor;
    public bool aliveBefore;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        // Reference to cells array
        GameObject golObj = GameObject.Find("GameOfLifeObj");
        gol = golObj.GetComponent<GameOfLife>();

        //Cell visuals is the child of the object
        rend = gameObject.GetComponentInChildren<Renderer>();
        aliveBefore = lifeState;
        updateColor();

    }

    void moveIfAlive()
    {
        float speed = 1.0f;
        if (lifeState == true)
        {
            transform.localScale += new Vector3(0, speed * Time.deltaTime, 0);
        }
    }

    void Lifecycle()
    {
        if (lifeState)
        {
            if (CountLiveNeighbors() == 2 || CountLiveNeighbors() == 3)
            {
                lifeState = true;
            }
            else if (CountLiveNeighbors() <= 4 || CountLiveNeighbors() == 1 || CountLiveNeighbors() == 0)
            {
                lifeState = false;
            }
        }
        else
        {
            if (CountLiveNeighbors() == 3)
            {
                lifeState = true;
            }
        }
        updateColor();
        moveIfAlive();
    }

    // Update is called once per frame
    void Update()
    {
        Lifecycle();

    }
    void updateColor()
    {
        if (lifeState)
        {
            rend.material.color = aliveColor;
        }
        else if (aliveBefore) {
            rend.material.color = AliveBeforeColor;
        }
        else
        {
            rend.material.color = deadColor;
        }
    }
    private void OnMouseDown()
    {
        lifeState = !lifeState;
        updateColor();
    }

    int CountLiveNeighbors()
    {
        //Does not count itself - minus 1
        int alive = -1;

        //Corner edge case
        if (positionY == 0 && positionX == 0)
        {
            for (int xIndex = (positionX); xIndex <= positionX + 1; xIndex++)
            {
                for (int yIndex = (positionY); yIndex <= positionY + 1; yIndex++)
                {
                    if (gol.cells[xIndex, yIndex].lifeState)
                    {
                        alive++;
                    }
                }
            }
        }
        else if (positionY == 0 && positionX == gol.gridSize - 1)
        {
            for (int xIndex = (positionX - 1); xIndex <= positionX; xIndex++)
            {
                for (int yIndex = (positionY); yIndex <= positionY + 1; yIndex++)
                {
                    if (gol.cells[xIndex, yIndex].lifeState)
                    {
                        alive++;
                    }
                }
            }
        }
        else if (positionY == gol.gridSize - 1 && positionX == 0)
        {
            for (int xIndex = (positionX); xIndex <= positionX + 1; xIndex++)
            {
                for (int yIndex = (positionY - 1); yIndex <= positionY; yIndex++)
                {
                    if (gol.cells[xIndex, yIndex].lifeState)
                    {
                        alive++;
                    }
                }
            }
        }
        else if (positionY == gol.gridSize - 1 && positionX == gol.gridSize - 1)
        {
            for (int xIndex = (positionX - 1); xIndex <= positionX; xIndex++)
            {
                for (int yIndex = (positionY - 1); yIndex <= positionY; yIndex++)
                {
                    if (gol.cells[xIndex, yIndex].lifeState)
                    {
                        alive++;
                    }
                }
            }
        }


        //Border of array

        else if (positionY == 0)
        {
            for (int xIndex = (positionX - 1); xIndex <= positionX + 1; xIndex++)
            {
                for (int yIndex = (positionY); yIndex <= positionY + 1; yIndex++)
                {
                    if (gol.cells[xIndex, yIndex].lifeState)
                    {
                        alive++;
                    }
                }
            }
        }

        else if (positionY == gol.gridSize - 1)
        {
            for (int xIndex = (positionX - 1); xIndex <= positionX + 1; xIndex++)
            {
                for (int yIndex = (positionY - 1); yIndex <= positionY; yIndex++)
                {
                    if (gol.cells[xIndex, yIndex].lifeState)
                    {
                        alive++;
                    }
                }
            }
        }
        else if (positionX == gol.gridSize - 1)
        {
            for (int xIndex = (positionX - 1); xIndex <= positionX; xIndex++)
            {
                for (int yIndex = (positionY - 1); yIndex <= positionY + 1; yIndex++)
                {
                    if (gol.cells[xIndex, yIndex].lifeState)
                    {
                        alive++;
                    }
                }
            }
        }
        else if (positionX == 0)
        {
            for (int xIndex = (positionX); xIndex <= positionX + 1; xIndex++)
            {
                for (int yIndex = (positionY - 1); yIndex <= positionY + 1; yIndex++)
                {
                    if (gol.cells[xIndex, yIndex].lifeState)
                    {
                        alive++;
                    }
                }
            }
        }
        else
        {
            for (int xIndex = (positionX - 1); xIndex <= positionX + 1; xIndex++)
            {
                for (int yIndex = (positionY - 1); yIndex <= positionY + 1; yIndex++)
                {
                    if (gol.cells[xIndex, yIndex].lifeState)
                    {
                        alive++;
                    }
                }
            }
        }
        return alive;
    }

}

