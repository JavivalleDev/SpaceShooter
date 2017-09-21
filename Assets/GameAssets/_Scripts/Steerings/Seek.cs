using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : BaseSteeringBehaviour
{

    public GameObject target;    
    [SerializeField] float maxSpeed = 60; //maxima velocidad que aplicara el behaviour

    Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public override Vector3 GetUnclampedForce()
    {
        if (!target) return Vector3.zero;
        Vector3 currentVelocity = _rb.velocity;
        Vector3 desiredVelocity = (target.transform.position - transform.position).normalized * maxSpeed; //Saca la direccion restando las posiciones y multiplicandolo por la velocidad maxima
        Vector3 force = desiredVelocity - currentVelocity; //Estas tres primeras lineas resultan en la fuerza que aplico
        return force;
    }
}
