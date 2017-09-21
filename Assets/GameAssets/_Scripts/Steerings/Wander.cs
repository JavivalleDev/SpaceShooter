using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : BaseSteeringBehaviour
{
    public float circleDistance; //Un punto a una distancia del objeto donde se generara un circulo
    public float circleRadio; //El radio del circulo que se va a crear
    public float angleIncrementRads; //El punto que se mueve en un arco dado

    float currentAngleRads;

    public override Vector3 GetUnclampedForce()
    {
        Vector3 circleCenter = transform.forward * circleDistance;
        Vector3 circle = new Vector3(Mathf.Sin(currentAngleRads), 0, Mathf.Cos(currentAngleRads)) * circleRadio;

        currentAngleRads += Random.Range(-.5f, .5f) * angleIncrementRads * Time.deltaTime; //Movemos el punto hacia la izquierda o la derecha

        Vector3 force = circle + circleCenter;
        return force;
    }
}
