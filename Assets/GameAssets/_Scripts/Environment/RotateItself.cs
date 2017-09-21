using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItself : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up);
    }
}
