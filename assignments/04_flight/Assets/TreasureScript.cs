using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreasureScript : MonoBehaviour
{
    public TMP_Text win;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				//You win!
				Debug.Log("WON");
            	win.enabled = true;
			}
		}
}
