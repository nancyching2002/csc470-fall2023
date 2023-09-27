using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject playerPrefab;
    public int gridSize;

    // Create a 2D array of CellScripts
    public CellScript[,] cells;

    // Start is called before the first frame update
    void Start()
    {
        gridSize = 100;
        // Instantiate the 2D array that we will use to store the state of the
        // Cells.
        cells = new CellScript[gridSize, gridSize];

        // Using nested for loops is a good way to create patterns like (0,0),
        // (0,1), (0,2), (0,3)... (2,0), (2,1), (2,2).... (5,0), (5,1)... etc.
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                // Create a position based on x, y
                Vector3 pos = transform.position;
                float cellWidth = 1f;
                float spacing = 0.1f;
                pos.x = pos.x + x * (cellWidth + spacing);
                pos.z = pos.z + y * (cellWidth + spacing);
                GameObject cellObj = Instantiate(cellPrefab, pos, transform.rotation);

                // (x,y) is the index in the 2D array. Store a reference to the
                // CellScript of the instantiated object because that is the
                // object that contains the information we will be intereated in
                // (the 'alive' variable.

                cells[x, y] = cellObj.GetComponent<CellScript>();
                
                //Save position of cell in itself
                cells[x, y].positionX = x;
                cells[x, y].positionY = y;

                //Set cell random state
                //20% of time will be true
                cells[x,y].lifeState = (Random.value < 0.2f);

                if (x == gridSize-1 && y == 0) {
                    GameObject playerObj = Instantiate(playerPrefab, pos + new Vector3(0,2,0), transform.rotation);
                }
            }
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
