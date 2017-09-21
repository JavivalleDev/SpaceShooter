using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{

    private Player _player;
    private Enemy[] _enemies;

    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            _enemies = FindObjectsOfType<Enemy>();
            foreach (var VARIABLE in _enemies)
            {
                VARIABLE.DieCheat();
            }
        }

        if(Input.GetKeyDown(KeyCode.H)) _player.HealCheat();
        if(Input.GetKeyDown(KeyCode.J)) _player.GetComponent<ShieldManager>().ShieldCheat();
    }
}
