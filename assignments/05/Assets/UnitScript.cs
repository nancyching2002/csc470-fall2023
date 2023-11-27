using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitScript : MonoBehaviour
{
    //Hover/Select color
    public GameObject activeRing;
    public GameObject hoverRing;

    public CharacterController cc;

    public Animator animator;

    public Color selectedColor;
    public Color hoverColor;

    float moveSpeed = 5;
    public float health = 100;
    public float currentHealth;

    public HealthBar healthBar;

    bool hover = false;
    public bool selected = false;

    Vector3 target;
    Vector3 enemyTarget;
    bool hasTarget = false;
    bool hasEnemyTarget = false;

    public bool canAttack = false;
    public Vector3 healthTarget;
    public bool canHeal = false;

    public bool dead = false;



    // Start is called before the first frame update
    void Start()
    {

        GameManager.SharedInstance.units.Add(this);

        currentHealth = health;
        healthBar.SetMaxHealth(health);
    }

    private void OnDestroy()
    {
        GameManager.SharedInstance.units.Remove(this);
    }

    //Get to the end 
        void OnControllerColliderHit(ControllerColliderHit collide)
        {
            if (collide.gameObject.CompareTag("Trigger"))
            {
                SceneManager.LoadScene("Won", LoadSceneMode.Single);
            }
        }

    void Update()
    {
        //Death
        if (currentHealth < 1)
        {
            animator.SetBool("Dead", true);
            dead = true;
        }

        if (!dead) 
        {
            // Close to health poition
            if (Vector3.Distance(transform.position, healthTarget) < 5f)
            {
                canHeal = true;
            }
            else
            {
                canHeal = false;
            }

            //If enemy has been clicked, check if the player is close then attack
            if (hasEnemyTarget)
            {
                if (Vector3.Distance(transform.position, enemyTarget) < 5f)
                {
                    canAttack = true;
                }
                else
                {
                    canAttack = false;
                }
            }

            //Move
            if (hasTarget)
            {
                Vector3 vectorToTarget = (target - transform.position).normalized;

                float step = 5 * Time.deltaTime;
                Vector3 rotatedTowardsVector = Vector3.RotateTowards(transform.forward, vectorToTarget, step, 1);
                rotatedTowardsVector.y = 0;
                transform.forward = rotatedTowardsVector;

                cc.Move(vectorToTarget * moveSpeed * Time.deltaTime);
                animator.SetBool("Moving", true);
                if (Vector3.Distance(transform.position, target) < 1f)
                {
                    hasTarget = false;
                    animator.SetBool("Moving", false);
                }
            }
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage*Time.deltaTime*1;
        healthBar.SetHealth(currentHealth);
    }

    public void SetTarget(Vector3 t)
    {
        target = t;
        hasTarget = true;
    }

    public void SetTargetAttack(Vector3 t)
    {
        enemyTarget = t;
        hasTarget = true;
        hasEnemyTarget = true;
    }


    private void OnMouseDown()
    {
        GameManager.SharedInstance.SelectUnit(this);
        SetUnitColor(); 
    }

    private void OnMouseEnter()
    {
        hover = true;
        SetUnitColor();
    }

    private void OnMouseExit()
    {
        hover = false;
        SetUnitColor();
    }

    public void SetUnitColor()
    {
        if (selected)
        {
            hoverRing.SetActive(false);
            activeRing.SetActive(true);
        }
        else if (hover)
        {
            hoverRing.SetActive(true);
            activeRing.SetActive(false);
        }
        else
        {
            hoverRing.SetActive(false);
            activeRing.SetActive(false);
        }
    }
}