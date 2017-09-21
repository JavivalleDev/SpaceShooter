using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected GameObject[] ShootPoints;
    [SerializeField] protected float Speed = 400;

    public float ShootRate = 0.2f;
    public float Damage = 1;

    protected AutoAim Autoaim;
    protected GameObject Target;
    protected UIManager Manager;
    protected GameobjectPool ProjectilePool;

    [SerializeField] protected AudioSource AudioS;
    

    protected virtual void Awake()
    {
        Autoaim = GetComponent<AutoAim>();
        Manager = FindObjectOfType<UIManager>();
    }

    protected virtual void Update()
    {
        if (gameObject.tag.Equals("Player"))
        {
            Target = Autoaim.GetTarget();
        }
    }

    public void StartShooting()
    {
        if (!IsInvoking("Shoot"))
        {
            InvokeRepeating("Shoot", 0, ShootRate);
        }
    }

    public void StopShooting()
    {
        if (IsInvoking("Shoot"))
        {
            CancelInvoke("Shoot");
        }
    }

    public void SingleShoot()
    {
        Invoke("Shoot", 0);
    }

    public GameObject GetTarget()
    {
        return Target;
    }

    protected abstract void Shoot();
}