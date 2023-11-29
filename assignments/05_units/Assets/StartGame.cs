using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject screen;
    public void startGame()
    {
        SceneManager.LoadScene("Battle", LoadSceneMode.Single);
    }
}
