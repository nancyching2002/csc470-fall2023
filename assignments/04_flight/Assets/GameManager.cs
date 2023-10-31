using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance;
    public TMP_Text scoreText;
    public TMP_Text transitionText;
    public TMP_Text gameDirections;
    public TMP_Text speed;
    public PlaneController PlaneController;
    public AstronautPlayer character;

    public GameObject islands;

    int score = 0;

    void Awake()
    {
        if (SharedInstance != null)
        {
            Debug.Log("There should only be one GameManager!");
        }
        SharedInstance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed.text = PlaneController.speed.ToString();

        if (score == 15) {
            gameDirections.text = "Something appear on the ground! Land the plane and check it out!" + "\n" + "\n" + "Slow the plane down (speed < 10) and approach the ground.";
            islands.SetActive(true);
        }

        if (PlaneController.notCrashed == false){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public void Landed() {
        score = 0;
        gameDirections.text = "Climb to the top of the islands. Collect powers to gain ability to double jump.";

        Instantiate(character, PlaneController.transform.position, Quaternion.identity);
        PlaneController.buttonToLand.gameObject.SetActive(false);
        PlaneController.GetComponent<PlaneController>().enabled = false;

        speed.enabled = false;
        transitionText.enabled = false;

        
        
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}
