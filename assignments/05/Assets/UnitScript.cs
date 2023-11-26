using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    //Hover/Select color
    public Renderer bodyRenderer;
    public CharacterController cc;

    //Magic
    public GameObject magic;


    public Color selectedColor;
    public Color hoverColor;
    Color defaultColor;

    float moveSpeed = 5;
    public float health = 100;
    public float currentHealth;

    public HealthBar healthBar;
    float damage = 10f;

    bool hover = false;
    public bool selected = false;

    Vector3 target;
    Vector3 enemyTarget;
    bool hasTarget = false;
    bool hasEnemyTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = bodyRenderer.material.color;
        GameManager.SharedInstance.units.Add(this);

        currentHealth = health;
        healthBar.SetMaxHealth(health);
    }

    private void OnDestroy()
    {
        GameManager.SharedInstance.units.Remove(this);
    }


    // Update is called once per frame
    void Update()
    {
        //If enemy has been clicked, check if the player is close then attack
        if (hasEnemyTarget)
        {
            if (Vector3.Distance(transform.position, enemyTarget) < 3f)
            {
                Debug.Log("Attacking");
                GameManager.SharedInstance.AttackEnemy();
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
            if (Vector3.Distance(transform.position, target) < 1f)
            {
                hasTarget = false;
            }
        }

    }

    //public void useMagic()
    //{
    //    Vector3 offset = new Vector3(0,0,1f);
    //    //Generic Magic
    //    Instantiate(magic, transform.position + offset, transform.rotation);
    //}

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
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
            bodyRenderer.material.color = selectedColor;
        }
        else if (hover)
        {
            bodyRenderer.material.color = hoverColor;
        }
        else
        {
            bodyRenderer.material.color = defaultColor;
        }
    }
}