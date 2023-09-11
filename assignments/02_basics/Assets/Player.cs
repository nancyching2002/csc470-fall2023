using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 Vec;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vec = transform.localPosition;  
        Vec.y = Vec.y + Input.GetAxis("Jump") * Time.deltaTime * 20;  
        Vec.x = Vec.x + Input.GetAxis("Horizontal") * Time.deltaTime * 20;  
        Vec.z = Vec.z + Input.GetAxis("Vertical") * Time.deltaTime * 20;  
        transform.localPosition = Vec;  
    }
}
