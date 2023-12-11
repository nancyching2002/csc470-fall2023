using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float speed = 0.5f;
    public GameObject player;


    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(player.transform.position.x, player.transform.position.y+15, player.transform.position.z-5);
        transform.position = offset;
    }

    // Update is called once per frame
    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal") * speed;
        float zAxisValue = Input.GetAxis("Vertical") * speed;

        transform.position = new Vector3(transform.position.x + xAxisValue, transform.position.y, transform.position.z + zAxisValue);
    }
}
