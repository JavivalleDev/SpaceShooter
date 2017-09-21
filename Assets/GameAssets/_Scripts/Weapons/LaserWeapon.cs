using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : WeaponBase
{
    public float MaxOverHeat = 20;
    [HideInInspector]public float CurrentOverHeat = 0;

    [SerializeField] private ParticleSystem[] _particles;

    private bool _bOverHeat = false;


    //Vector3[] originalForwards;

    protected override void Awake()
    {
        base.Awake();
        ProjectilePool = FindObjectOfType<GameobjectPool>();
        StartCoroutine(reduceOverHeat());
        //originalForwards = new Vector3[ShootPoints.Length];
        //for(int i = 0; i < ShootPoints.Length; ++i)
        //{
        //    originalForwards[i] = ShootPoints[i].transform.forward;
        //}
    }

    protected override void Update()
    {
        base.Update();
        _bOverHeat = CurrentOverHeat >= MaxOverHeat - 1;
    }

    protected override void Shoot()
    {
        if (_bOverHeat) return;
        for (int i = 0; i < ShootPoints.Length; ++i)
        {
            if(AudioS)AudioS.Play();
            GameObject shootPoint = ShootPoints[i];
            GameObject newLaser = ProjectilePool.GetObjectFromPool("Laser");

            newLaser.transform.position = shootPoint.transform.position;
            newLaser.transform.forward = shootPoint.transform.forward;
            newLaser.GetComponent<ProjectileBase>().Damage = Damage;

            if (CompareTag("Enemy")) Target = FindObjectOfType<Player>().gameObject;

            if (Target) newLaser.transform.LookAt(Target.transform);
            newLaser.GetComponent<Rigidbody>().velocity = newLaser.transform.forward * Speed;

            if (_particles[i]) _particles[i].Play();
            if (CompareTag("Player")) { newLaser.GetComponent<LineRenderer>().startColor = Color.blue;newLaser.GetComponent<LineRenderer>().endColor = Color.blue;}
            if (CurrentOverHeat < MaxOverHeat) CurrentOverHeat++;
            CurrentOverHeat = Mathf.Min(CurrentOverHeat, MaxOverHeat);
        }
    }

    IEnumerator reduceOverHeat()
    {
        if(CurrentOverHeat > 0) CurrentOverHeat--;
        CurrentOverHeat = Mathf.Max(0, CurrentOverHeat);
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(reduceOverHeat());
    }


}
