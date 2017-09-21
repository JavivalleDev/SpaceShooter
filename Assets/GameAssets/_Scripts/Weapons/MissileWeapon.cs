using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileWeapon : WeaponBase {

    public int MaxAmmo = 3;
    public int CurrentAmmo;
    
    private float _shootStart = 0;
    private bool _bShootAble = false;

    protected override void Awake()
    {
        base.Awake();
        ProjectilePool = FindObjectOfType<GameobjectPool>();
        CurrentAmmo = MaxAmmo;
        StartCoroutine(reloadAmmo());
    }

    protected override void Update()
    {
        base.Update();
        if (Time.time > _shootStart + ShootRate) _bShootAble = true;
    }

    protected override void Shoot()
    {
        if (CurrentAmmo == 0 || !_bShootAble) return;
        if (AudioS) AudioS.Play();
        for (int i = 0; i < ShootPoints.Length; ++i)
        {
            _shootStart = Time.time;
            GameObject shootPoint = ShootPoints[i];
            GameObject newMissile = ProjectilePool.GetObjectFromPool("Missile");

            newMissile.transform.position = shootPoint.transform.position;
            newMissile.transform.forward = shootPoint.transform.forward;
            newMissile.GetComponent<ProjectileBase>().Damage = Damage;

            if (CompareTag("Enemy")) Target = FindObjectOfType<Player>().gameObject;

            if (Target)
            {
                newMissile.GetComponent<Missile>().SetTarget(Target);
                newMissile.transform.LookAt(Target.transform);
            }
            
            newMissile.GetComponent<Rigidbody>().velocity = shootPoint.transform.forward * Speed;
            if(CurrentAmmo > 0) CurrentAmmo--;
            CurrentAmmo = Mathf.Max(0, CurrentAmmo);

            _bShootAble = false;
        }
    }

    IEnumerator reloadAmmo()
    {
        if(CurrentAmmo < 3) CurrentAmmo++;
        CurrentAmmo = Mathf.Min(CurrentAmmo, MaxAmmo);
        yield return new WaitForSeconds(3);
        StartCoroutine(reloadAmmo());
    }
}
