using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject drop;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy() 
    {
        GameObject key = Instantiate(drop, boss.transform.position, drop.transform.rotation);
        GameManager.SharedInstance.keyPrefab = key;
    }
}
