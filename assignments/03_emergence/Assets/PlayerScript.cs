using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Vector3 Vec;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vec = transform.localPosition;  
        Vec.y = Vec.y + Input.GetAxis("Jump") * Time.deltaTime * 30;  
        Vec.z = Vec.z + Input.GetAxis("Horizontal") * Time.deltaTime * 5;  
        Vec.x = Vec.x - Input.GetAxis("Vertical") * Time.deltaTime * 5;  
        transform.localPosition = Vec;  
    }
}
