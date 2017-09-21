using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSteeringBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float maxForce = 100;
    [SerializeField]
    Color debugColor = Color.white;

    public Vector3 GetForce()
    {
        Vector3 unclampedForce = GetUnclampedForce();
        Vector3 force = Vector3.ClampMagnitude(unclampedForce, maxForce);

        Debug.DrawLine(transform.position, transform.position + force, debugColor, 1);

        return force;
    }

    public abstract Vector3 GetUnclampedForce();
}
