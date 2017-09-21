using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    public float LoopTime;
    public float Speed;

    private bool _bLooping = false;
    private float _loopStart;

    void Start()
    {
        Speed = 180 / LoopTime;
    }

    void Update()
    {
        if (!_bLooping)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _bLooping = true;
                _loopStart = Time.time;
            }
        }

        if (Time.time > _loopStart + LoopTime)
        {
            _bLooping = false;            
        }
    }

    void FixedUpdate()
    {
        if (_bLooping)
        {
            Invoke("RotateX", 0);
            if (Time.time >= _loopStart + LoopTime / 2)
            {
                CancelInvoke("RotateX");
                Invoke("RotateZ", 0);
            }
        }
    }

    void RotateX()
    {
        transform.Rotate(Vector3.right * Speed * 2 * Time.fixedDeltaTime);
    }

    void RotateZ()
    {
        transform.Rotate(Vector3.forward * Speed * 2 * Time.fixedDeltaTime);
    }

    public bool isLooping()
    {
        return _bLooping;
    }
}