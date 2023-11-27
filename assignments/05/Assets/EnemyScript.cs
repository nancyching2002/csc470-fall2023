using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject activeRing;

    public Animator animator;

    private State state;
    float moveSpeed = 2;
    public CharacterController cc;

    public HealthBar healthBar;

    public bool playerWithinAttack = false;
    public bool selected = false;

    public float health = 100;
    public float currentHealth;

    Vector3 offset = new Vector3(0,1f, 0);

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
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Vector3 offset = new Vector3(0,1f, 0);
        Debug.DrawRay(transform.position+offset, forward, Color.green);

        if (selected)
        {
            activeRing.SetActive(true);
        }
        else if (!selected)
        {
            activeRing.SetActive(false);
        }

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
        healthBar.SetHealth(currentHealth);
    }

    private void OnMouseDown()
    {
        if(GameManager.SharedInstance.selectedUnit != null)
        {
            GameManager.SharedInstance.SelectEnemy(this);
        }
    }

    private void FindPlayer()
    {
        if (Physics.Raycast(transform.position+offset, transform.TransformDirection (Vector3.forward), out RaycastHit hitInfo, 20f))
        {
            state = State.Target;
        }
    }
    private void PlayerFound()
    {
       
        if (Physics.Raycast(transform.position+offset, transform.TransformDirection (Vector3.forward), out RaycastHit hitInfo, 20f))
        {
            animator.SetBool("Moving", true);
            Vector3 playerPos = hitInfo.transform.position;
            Vector3 vectorToTarget = (playerPos - transform.position).normalized;
            

            float step = 10 * Time.deltaTime;
            Vector3 rotatedTowardsVector = Vector3.RotateTowards(transform.forward, vectorToTarget, step, 1);
            rotatedTowardsVector.y = 0;
            transform.forward = rotatedTowardsVector;

            Vector3 amountToMove = transform.forward * moveSpeed * Time.deltaTime;
            cc.Move(amountToMove);

            float distance = Vector3.Distance(transform.position, playerPos);

            if (distance < 3f)
            {
                animator.SetBool("Moving", false);
                cc.Move(new Vector3(0, 0, 0));
                playerWithinAttack = true;
                animator.SetBool("Fighting", true);
                GameManager.SharedInstance.AttackPlayer();
            }
            else 
            {
                animator.SetBool("Moving", true);
                animator.SetBool("Fighting", false);
                playerWithinAttack = false;
            }

        }
    }
}
