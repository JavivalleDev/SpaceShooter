using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    private float _speed = 0, _waitTime = 5;
    [SerializeField] private Text _texto;

    void Start()
    {
        InvokeRepeating("Accelerate", 0, 0.001f);
        InvokeRepeating("WriteText", 0, 1);
    }

    void Update()
    {
        transform.Translate(-transform.forward * _speed * Time.deltaTime);
        if(_waitTime <= 0) SceneManager.LoadScene("MainMenu");
    }

    void Accelerate()
    {
        _speed++;
    }

    void WriteText()
    {
        _texto.text = "Returning to Main Menu in " + _waitTime;
        _waitTime--;
    }


}
