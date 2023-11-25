using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private State state;

    private enum State 
    {
        NoTarget,
        Target,
    }
    private void Awake()
    {
        state = State.NoTarget;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.NoTarget:
                FindPlayer();
                break;
            case State.Target:
                PlayerFound();
                break;
            
        }
//        if (Physics.Raycast(transform.position, transform.TransformDirection (Vector3.forward), out RaycastHit hitInfo, 20f))
//        {
//            Debug.Log("hit something");
//            Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) * hitInfo.distance, Color.red);
//        }
//        else
//        {
//            Debug.Log("hit nothing");
//            Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) * hitInfo.distance, Color.blue);
//        }
    }

    private void FindPlayer()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection (Vector3.forward), out RaycastHit hitInfo, 20f))
        {
            state = State.Target;
        }
    }
    private void PlayerFound()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection (Vector3.forward), out RaycastHit hitInfo, 20f))
        {
            Debug.Log(hitInfo.transform.position);
        }
    }
}
