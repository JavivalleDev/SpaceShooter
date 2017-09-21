using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{

    private Transform _player;

    void Awake()
    {
        _player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void LateUpdate() //En general este update se usa para las camaras, para que se ejecuten primero todos los movimientos y las fisicas de los updates, y despues ajustar la camara
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x = _player.position.x;
        currentPosition.z = _player.position.z;
        transform.position = currentPosition;

        Vector3 playerForward = _player.forward;
        transform.LookAt(transform.position - Vector3.up);
        transform.Rotate(new Vector3(0, 0, -_player.transform.eulerAngles.y));
    }
}
