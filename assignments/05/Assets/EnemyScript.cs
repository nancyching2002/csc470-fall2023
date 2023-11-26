using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private State state;
    float moveSpeed = 2;
    public CharacterController cc;

    public bool playerWithinAttack = false;
    public bool selected = false;

    public float health = 100;
    public float currentHealth;

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
        GameManager.SharedInstance.enemies.Add(this);
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            GameManager.SharedInstance.enemies.Remove(this);
            Destroy(gameObject);
        }
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
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void OnMouseDown()
    {
        GameManager.SharedInstance.SelectEnemy(this);
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
            Vector3 playerPos = hitInfo.transform.position;
            Vector3 vectorToTarget = (playerPos - transform.position).normalized;
            

            float step = 10 * Time.deltaTime;
            Vector3 rotatedTowardsVector = Vector3.RotateTowards(transform.forward, vectorToTarget, step, 1);
            rotatedTowardsVector.y = 0;
            transform.forward = rotatedTowardsVector;

            Vector3 amountToMove = transform.forward * moveSpeed * Time.deltaTime;
            cc.Move(amountToMove);

            float distance = Vector3.Distance(transform.position, playerPos);

            if (distance < 2f)
            {
                cc.Move(new Vector3(0, 0, 0));
                playerWithinAttack = true;
                GameManager.SharedInstance.AttackPlayer();
            }
            else 
            {
                playerWithinAttack = false;
            }

        }
    }
}
