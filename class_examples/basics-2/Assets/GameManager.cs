using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform launcherTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            Vector3 force = launcherTransform.forward;
            int addedForce = 0;
            while(Input.GetKeyDown(KeyCode.Space)) {
                addedForce = addedForce * 10;
            }
            
        }
        GameObject ball = Instantiate(ballPrefab, launcherTransform.position, launcherTransform.rotation);
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
            
        ballRb.AddForce((force * addedForce) * 1000);
    }
}
