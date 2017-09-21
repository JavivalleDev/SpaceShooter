using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LifeManager
{
    [SerializeField] private float _raycastDistance;
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private LayerMask playerLayerMask;

    private GameobjectPool _pool;
    

    [SerializeField]
    public GameObject target
    {
        get
        {
            return _seek.target;
        }
        set
        {
            _seek.target = value;
        }
    }

    GameobjectPool _enemyPool;
    Seek _seek;
    WaveGenerator _waveGen;

    protected override void Awake()
    {
        base.Awake();
        _seek = GetComponent<Seek>();
        _enemyPool = FindObjectOfType<GameobjectPool>();
        _waveGen = FindObjectOfType<WaveGenerator>();
        _pool = FindObjectOfType<GameobjectPool>();
    }

    void FixedUpdate()
    {
        bool playerOnSight = false;
        RaycastHit hitInfo;

        if (Physics.SphereCast(transform.position, 120, transform.forward, out hitInfo, _raycastDistance, playerLayerMask))
        {
            print("hola");
            Vector3 direction = (hitInfo.transform.position - transform.position).normalized;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, _raycastDistance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Vector3 hitDirection = hit.transform.position - transform.position;
                    float angle = Vector3.Angle(hitDirection, transform.forward);
                    if (angle <= 90)
                    {
                        playerOnSight = true;
                    }
                }
            }
        }

        if (!playerOnSight)
        {
            _laserWeapon.StopShooting();
        }
        else
        {
            _laserWeapon.StartShooting();
            if(Random.value > 0.95f) _missileWeapon.SingleShoot();
        }
    }

    protected override void Die()
    {
        GameObject explosionPrefab = _pool.GetObjectFromPool("ShipExplosion");
        if (explosionPrefab)
        {
            explosionPrefab.transform.position = transform.position;
            ParticleSystem[] ps = explosionPrefab.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < ps.Length; ++i) ps[i].Play();
            StartCoroutine(ReturnParticle(explosionPrefab));

        }
        _enemyPool.ReturnObjectToPool("Enemy", this.gameObject);
        _waveGen.SpaceshipDestroyed();
    }

    IEnumerator ReturnParticle(GameObject explosionPrefab)
    {
        yield return new WaitForSeconds(4);
        _pool.ReturnObjectToPool("ShipExplosion", explosionPrefab);
    }
}



//void FixedUpdate()
//{
//    bool playerOnSight = false;
//    RaycastHit hitInfo;
//    if (Physics.Raycast(_raycastPoint.position, this.transform.forward, out hitInfo, _raycastDistance))
//    {
//        if (hitInfo.transform.root.CompareTag("Player"))
//        {
//            playerOnSight = true;
//        }
//    }

//    if (!playerOnSight)
//    {
//        _laserWeapon.StopShooting();
//    }
//    else
//    {
//        _laserWeapon.StartShooting();
//        if (Random.value > 0.85f) _missileWeapon.SingleShoot();
//    }
//}
