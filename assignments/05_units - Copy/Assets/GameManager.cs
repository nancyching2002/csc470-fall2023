using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject UIManager;
    public static GameManager SharedInstance;
    public GameObject unitPrefab;

    // Key
    public GameObject keyPrefab;

    // Health potion
    public GameObject healthPrefab;

    //Particles
    public GameObject particles;

    //NPCs
    public NPCScript npc;

    public List<EnemyScript> enemies = new List<EnemyScript>();
    public GameObject enemyPrefab;

    public UnitScript selectedUnit;
    EnemyScript selectedEnemy;
    EnemyScript currentEnemy;

    private float attackCooldown = 1f;
    private float attackNext = 0;

    //Special attack times
    public float lightiningCooldown = 5f;
    private float lightiningNextFireTime = 0;
    public  float lightiningCooldownTimer = 0f;

    public float fireCooldown = 10f;
    private float fireNextFireTime = 0;
    public  float fireCooldownTimer = 0f;

    public GameObject magicCircle1;
    public GameObject magicCircle2;

    public bool playerDead = false;
    
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

        if (selectedUnit.dead)
        {
            playerDead = true;
        }
        if (lightiningCooldownTimer > 1)
        {
            lightiningCooldownTimer -= Time.deltaTime;
        }

        if (fireCooldownTimer > 1)
        {
            fireCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999999) && (selectedUnit != null))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    particles.transform.position = hit.point;
                    particles.SetActive(true);
                    selectedUnit.SetTarget(hit.point);
                    DeselectEnemy();
                }
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {

                        selectedUnit.SetTargetAttack(hit.point);
                        particles.SetActive(false);
                        if (selectedUnit.canAttack == true && Time.time > attackNext)
                        {
                            selectedUnit.animator.SetTrigger("Fighting");
                            AttackEnemy();
                            attackNext = Time.time + attackCooldown;
                        }
                    
                }
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("PortalEnter"))
                {
                    if (Vector3.Distance(selectedUnit.transform.position, hit.point) < 5f)
                    {
                        SceneManager.LoadScene("Battle", LoadSceneMode.Single);
                    } 
                    
                }
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("NPC"))
                {
       
                    if (Vector3.Distance(selectedUnit.transform.position, hit.point) < 10f)
                    {
                        Debug.Log("Hit");
                        if (selectedUnit.hasKey)
                        {
                            SceneManager.LoadScene("Won", LoadSceneMode.Single);
                        }
                        else
                        {
                            npc.talkTo();
                        }
                    } 
                }
                else
                    {
                        npc.closeBox();
                    }

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Key"))
                {
                    if (Vector3.Distance(selectedUnit.transform.position, hit.point) < 4f)
                    {
                        selectedUnit.hasKey = true;
                        keyPrefab.SetActive(false);
                    }
                }

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Health"))
                {
                    selectedUnit.healthTarget = hit.point;
                    if (selectedUnit.canHeal == true)
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            specialOne();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            specialTwo();
        }
    }

    public void restart()
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }

    public void specialOne()
    {
        if (Time.time > lightiningNextFireTime && selectedUnit != null && selectedEnemy)
        {
            GameObject magic1 = Instantiate(magicCircle1, selectedEnemy.transform.position, selectedEnemy.transform.rotation);
            selectedEnemy.TakeDamage(50f);  
            lightiningNextFireTime = Time.time + lightiningCooldown;
            lightiningCooldownTimer += 5;
            Destroy(magic1, 1f);
        }
    }
    public void specialTwo()
    {
        if (Time.time > fireNextFireTime && selectedUnit != null && selectedEnemy)
        {
            GameObject magic1 = Instantiate(magicCircle2, selectedEnemy.transform.position, selectedEnemy.transform.rotation);
            selectedEnemy.TakeDamage(100f);   
            fireNextFireTime = Time.time + fireCooldown;
            fireCooldownTimer += 10f;
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
}