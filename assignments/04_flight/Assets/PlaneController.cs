using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float speed = 10f;
    float xRotationSpeed = 50f;
    float zRotationSpeed = 50f;
    float yRotationSpeed = 50f;
    bool stoppingSpeed = false;
    public bool notCrashed = true;
    float gravityPosition;

    public GameObject buttonToLand;
    Vector3 oldCamPos;

    public Camera cameraObject;
    void Start()
    {
        GameManager.SharedInstance.UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical"); //If nothing is pressed, returns 0

        float xRotation = vAxis * xRotationSpeed * Time.deltaTime;
        float zRotation = hAxis * zRotationSpeed * Time.deltaTime;
        float yRotation = hAxis * yRotationSpeed * Time.deltaTime;
        transform.Rotate(xRotation, yRotation , -zRotation , Space.Self);
        
        //BOOST
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed += 20;
        }
        //SLOW DOWN
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed -= 10 * Time.deltaTime;
            Debug.Log(speed);
        }

        //SPEED IS NEVER < 0s
        speed = Mathf.Max(0, speed);

        //TERRAIN
        float terrainY = Terrain.activeTerrain.SampleHeight(transform.position);
        if (transform.position.y < terrainY)
        {
            transform.position = new Vector3(transform.position.x, terrainY, transform.position.z);
            speed -= 100 * Time.deltaTime;
        }

        //MOVEMENT
        gameObject.transform.position += (speed * gameObject.transform.forward * Time.deltaTime);
        speed = Mathf.Max(0, (speed - 5f * Time.deltaTime));

        // GRAVITY
        gravityPosition = transform.position.y;
        gravityPosition -= 5f * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, gravityPosition, transform.position.z);

        //CAN LAND
        if ((speed <= 10f) && transform.position.y <= terrainY) {
            stoppingSpeed = true;
            Debug.Log("Can land");

        }
        if (stoppingSpeed == true && notCrashed) {
            buttonToLand.gameObject.SetActive(true);
        }

        //TOO FAST
        if ((speed >= 10f) && transform.position.y <= terrainY) {
            notCrashed = false;
            Debug.Log("Crashed!");
        }

        // CAMERA
        Vector3 newCamPos = transform.position + -transform.forward * 30 + Vector3.up * 10;
        if (oldCamPos == null)
        {
            oldCamPos = newCamPos;
        }
        cameraObject.transform.position = (newCamPos + oldCamPos) / 2f;
        cameraObject.transform.LookAt(transform);
        oldCamPos = newCamPos;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GameManager.SharedInstance.UpdateScore(1);
        }
    }
}
