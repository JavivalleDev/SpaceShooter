﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : ProjectileBase
{
    void FixedUpdate()
    {
        Vector3 velocity = Rb.velocity;
        float raycastDistance = velocity.magnitude * Time.fixedDeltaTime; //De esta forma averiguo donde estaría el laser en el siguiente frame
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, velocity.normalized, out hitInfo, raycastDistance))
        {
            //LifeManager lifeManager = hitInfo.transform.GetComponentInParent<LifeManager>();
            //if (lifeManager) lifeManager.RemoveLife();
            ShieldManager shieldManager = hitInfo.transform.GetComponentInParent<ShieldManager>();
            if (shieldManager) shieldManager.DamageShield(Damage);
            GameObject explosionPrefab = ProjectilePool.GetObjectFromPool("ProjectileExplosion");
            if (explosionPrefab)
            {
                explosionPrefab.transform.position = transform.position;
                ParticleSystem[] ps = explosionPrefab.GetComponentsInChildren<ParticleSystem>();
                for (int i = 0; i < ps.Length; ++i) ps[i].Play();
                StartCoroutine(ReturnParticle(explosionPrefab));
            }
            Return();
        }
    }

    //Al detectar impacto por Raycast ya no lo necesito
    //void OnTriggerEnter(Collider collider)
    //{
    //    SpaceShip spaceship = collider.GetComponentInParent<SpaceShip>();
    //    if (spaceship) { spaceship.ReceiveDamage(1); }
    //    laserManager.ReturnLaser(gameObject);
    //}

    void Return()
    {
        ProjectilePool.ReturnObjectToPool("Laser", this.gameObject);
    }

    IEnumerator ReturnParticle(GameObject explosionPrefab)
    {
        yield return new WaitForSeconds(4);
        ProjectilePool.ReturnObjectToPool("ProjectileExplosion", explosionPrefab);
    }
}
