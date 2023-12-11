using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public GameObject dialogueBox;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.SharedInstance.npc = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void talkTo()
    {
        animator.SetTrigger("hello");
        dialogueBox.SetActive(true);
    }
    public void closeBox()
    {
        dialogueBox.SetActive(false);
    }
}
