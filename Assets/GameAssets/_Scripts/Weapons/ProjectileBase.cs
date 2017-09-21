using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour {

    public float LifeTime = 5;
    public float Damage;

    protected GameobjectPool ProjectilePool;
    protected Rigidbody Rb;

    protected virtual void Awake()
    {
        ProjectilePool = FindObjectOfType<GameobjectPool>();
        Rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        Invoke("Return", LifeTime);
    }

    void OnDisable()
    {
        CancelInvoke("Return");
    }
}
