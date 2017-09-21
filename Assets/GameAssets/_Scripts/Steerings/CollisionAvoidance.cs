using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : BaseSteeringBehaviour
{
    [SerializeField]
    float raycastDistance = 20;
    [SerializeField]
    float raycastRadio = 20;
    [SerializeField]
    LayerMask raycastLayerMask;

    public override Vector3 GetUnclampedForce()
    {
        RaycastHit hitInfo;

        if(Physics.SphereCast(transform.position, raycastRadio, transform.forward, out hitInfo, raycastDistance, raycastLayerMask))
        {
            Vector3 obstacleCenter = hitInfo.transform.position; //Doy por hecho que todos los obstaculos tienen su origen en el centro;
            Vector3 hitPoint = hitInfo.point;
            Vector3 force = hitPoint - obstacleCenter;

            force = Vector3.Project(force, transform.right);
            if(force.magnitude == 0)
            {
                force = UnityEngine.Random.value < 0.5f ? transform.right : -transform.right;
            }

            force = force.normalized * maxForce;
            return force;
        }

        return Vector3.zero;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
