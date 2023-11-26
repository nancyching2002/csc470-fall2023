using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager SharedInstance;
    public List<UnitScript> units = new List<UnitScript>();
    public GameObject unitPrefab;

    //Magic
    public GameObject magic;

    public List<EnemyScript> enemies = new List<EnemyScript>();
    public GameObject enemyPrefab;

    UnitScript selectedUnit;
    EnemyScript selectedEnemy;
    
    void Awake()
    {
        if (SharedInstance != null)
        {
            Debug.Log("Why is there more than one GameManager!?!?!?!");
        }
        SharedInstance = this;
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
                    // If we get in here, we hit something! And the 'hit' object
                    // contains info about what we hit.
                    if (selectedUnit != null)
                    {
                        selectedUnit.SetTarget(hit.point);
                    }
                }
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (selectedUnit != null)
                    {
                        selectedUnit.SetTargetAttack(hit.point);

                    }
                }
            }
        }
    }

    public void AttackEnemy()
    {
        selectedEnemy.TakeDamage(100f);   
    }


    public void AttackPlayer()
    {
        selectedUnit.TakeDamage(1f);   
    }

    public void SelectEnemy(EnemyScript enemy)
    {
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