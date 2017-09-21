using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : BaseSteeringBehaviour
{
    [SerializeField]
    float separationRadio;

    public override Vector3 GetUnclampedForce()
    {
        Vector3 force = Vector3.zero;

        GameObject[] spaceships = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < spaceships.Length; ++i)
        {
            Transform spaceship = spaceships[i].transform;
            float distance = Vector3.Distance(transform.position, spaceship.transform.position);

            if(distance < separationRadio)
            {
                //Vector3 currentVelocity = _rb.velocity;
                //Vector3 desiredVelocity = -(spaceship.transform.position - transform.position);
                //Vector3 partialForce = desiredVelocity - currentVelocity;
                //force += partialForce;

                Vector3 desiredVelocity = -(spaceship.position - transform.position);
                force += desiredVelocity.normalized * maxForce;
            }
        }

        return force;
    }
}
