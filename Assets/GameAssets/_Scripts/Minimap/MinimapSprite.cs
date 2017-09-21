using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSprite : MonoBehaviour
{
    private LifeManager _parentSpaceship;

    void Start()
    {
        _parentSpaceship = GetComponentInParent<LifeManager>();
    }

    void Update()
    {
        transform.forward = Vector3.up;
        if(_parentSpaceship) transform.Rotate(Vector3.forward, _parentSpaceship.transform.eulerAngles.y + 180);
    }
}
