using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    // Declaring public variables in a MonoBehaviour script means that you can
    // assign values to these variable in the Unity editor.
    public GameObject treePrefab;

    public GameObject playerPrefab;


    // Start is called before the first frame update
    void Start()
    {
        int rowCount = 0;
        for (int i = 0; i < 10; i++)
        {
            generateTree();
            generatePlayers(rowCount);
            rowCount = rowCount + 3;
        }
    }

    void generatePlayers(int rowCount)
    {
        float x = 0;
        float y = 0;
        float z = rowCount;
        Vector3 pos = new Vector3(x, y, z);
        GameObject playerObj = Instantiate(playerPrefab, pos, Quaternion.identity);
    }

    void generateTree()
    {
        float x = Random.Range(-50, 50);
        float y = 0;
        float z = Random.Range(-50, 50);
        Vector3 pos = new Vector3(x, y, z);
        GameObject treeObj = Instantiate(treePrefab, pos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {


    }
}
