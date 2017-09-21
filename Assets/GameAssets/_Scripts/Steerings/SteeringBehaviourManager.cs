using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Va a coger todas las fuerzas y va a aplicarlas en conjunto
public class SteeringBehaviourManager : MonoBehaviour
{
    [SerializeField]
    float maxSpeed = 60;
    [SerializeField]
    float maxAngularSpeed = 45;
    [SerializeField]
    float mass = 20;

    BaseSteeringBehaviour[] _steerings;
    Rigidbody _rb;

    // Use this for initialization
    void Start()
    {
        _steerings = GetComponents<BaseSteeringBehaviour>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredVelocity = _rb.velocity;
        for(int i = 0; i< _steerings.Length; ++i)
        {
            BaseSteeringBehaviour steering = _steerings[i];
            desiredVelocity += steering.GetForce()/mass;
        }

        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxSpeed);

        //Debug.DrawLine(transform.position, transform.position + clampedForce, Color.red, 1);

        //Todos estos parametros se estan usando para que el movimiento aplicado no sea brusco

        _rb.velocity = Vector3.RotateTowards(_rb.velocity, desiredVelocity, maxAngularSpeed * Mathf.Deg2Rad * Time.fixedDeltaTime, maxSpeed);
        //_rb.velocity = Vector3.Slerp(_rb.velocity, desiredVelocity, .2f);
        //_rb.velocity = desiredVelocity;
        //transform.forward = desiredVelocity.normalized; //Para que mire siempre en la direccion en la que se mueve
        transform.forward = _rb.velocity.normalized; //Para que mire siempre en la direccion en la que se mueve
    }
}
