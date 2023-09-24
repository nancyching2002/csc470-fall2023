using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject dominoPrefab;
    public GameObject firstDomino;

    public float spacing = 0.8f;
    public int numOfDominos = 100;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numOfDominos; i++)
        {
            Vector3 pos = transform.position;
            pos = pos + transform.forward * i * spacing;
            GameObject domino = Instantiate(dominoPrefab, pos, transform.rotation);

            if (i == 0)
            {
                firstDomino = domino;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Get rigid body on domino to launch it
            Rigidbody rb = firstDomino.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(firstDomino.transform.forward * 10);
            }
        }
    }
}
