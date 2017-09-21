using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
