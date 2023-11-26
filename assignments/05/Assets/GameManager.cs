using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager SharedInstance;
    public List<UnitScript> units = new List<UnitScript>();
    public GameObject unitPrefab;

    public GameObject healthPrefab;

    //Particles
    public GameObject particles;


    public List<EnemyScript> enemies = new List<EnemyScript>();
    public GameObject enemyPrefab;

    public UnitScript selectedUnit;
    EnemyScript selectedEnemy;
    EnemyScript currentEnemy;

    public float attackCooldown = 1f;
    private float attackNext = 0;

    //Special attack times
    public float lightiningCooldown = 5f;
    private float lightiningNextFireTime = 0;

    public float fireCooldown = 10f;
    private float fireNextFireTime = 0;

    public GameObject magicCircle1;
    public GameObject magicCircle2;
    
    void Awake()
    {
        if (SharedInstance != null)
        {
            Debug.Log("Why is there more than one GameManager!?!?!?!");
        }
        SharedInstance = this;
    }

    void Start()
    {
        particles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999999))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    if (selectedUnit != null)
                    {
                        particles.transform.position = hit.point;
                        particles.SetActive(true);
                        selectedUnit.SetTarget(hit.point);
                        DeselectEnemy();
                    }
                }
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (selectedUnit != null)
                    {
                        selectedUnit.SetTargetAttack(hit.point);
                        particles.SetActive(false);
                        if (selectedUnit.canAttack == true && Time.time > attackNext)
                        {
                            AttackEnemy();
                            attackNext = Time.time + attackCooldown;
                        }

                        
                    }
                }
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Health"))
                {
                    selectedUnit.healthTarget = hit.point;
                    if (selectedUnit != null && selectedUnit.canHeal == true)
                    {
                        if (selectedUnit.currentHealth + 50f >= 100f)
                        {
                            selectedUnit.currentHealth += (100f-selectedUnit.currentHealth);
                        }
                        else{
                            selectedUnit.currentHealth += 50f;
                        }
                        selectedUnit.healthBar.SetHealth(selectedUnit.currentHealth);
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }


        //Special Attacks - lightining and fire
        if (Time.time > lightiningNextFireTime && Input.GetKeyDown(KeyCode.Alpha1) && selectedUnit != null)
        {
            GameObject magic1 = Instantiate(magicCircle1, selectedEnemy.transform.position, selectedEnemy.transform.rotation);
            selectedEnemy.TakeDamage(50f);   
            lightiningNextFireTime = Time.time + lightiningCooldown;
            Destroy(magic1, 1f);
        }
        if (Time.time > fireNextFireTime && Input.GetKeyDown(KeyCode.Alpha2) && selectedUnit != null)
        {
            GameObject magic1 = Instantiate(magicCircle2, selectedEnemy.transform.position, selectedEnemy.transform.rotation);
            selectedEnemy.TakeDamage(100f);   
            fireNextFireTime = Time.time + fireCooldown;
            Destroy(magic1, 1f);
        }
    }

    public void AttackEnemy()
    {
        selectedEnemy.TakeDamage(25f);   
    }


    public void AttackPlayer()
    {
        selectedUnit.TakeDamage(5f);   
    }

    public void DeselectEnemy()
    {
        // Deselect enemies 
        foreach (EnemyScript e in enemies) {
            e.selected = false;
        }
    }

    public void SelectEnemy(EnemyScript enemy)
    {
        foreach (EnemyScript e in enemies) {
            e.selected = false;
        }
        selectedEnemy = enemy;
        selectedEnemy.selected = true;
    }

    public void SelectUnit(UnitScript unit)
    {
        selectedUnit = unit;
        selectedUnit.selected = true;
        selectedUnit.SetUnitColor();
    }
}