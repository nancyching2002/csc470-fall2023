using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        //Object that just hit 
        //GameObjects all have tags
        if (collision.gameObject.CompareTag("domino")) {
            Renderer rend = gameObject.GetComponentInChildren<Renderer>();
            rend.material.color = Color.red;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
