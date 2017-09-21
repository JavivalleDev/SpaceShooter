using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundParent : MonoBehaviour
{
    [Range(-5f, 5f)]
    [SerializeField] private float angle = 1;
    [Range(-1f, 1f)]
    [SerializeField] private float RotationX = 0, RotationY = 0, RotationZ = 0;

    void Update()
    {
        transform.RotateAround(transform.parent.transform.position, new Vector3(RotationX, RotationY, RotationZ), angle * Time.deltaTime);
    }
}
