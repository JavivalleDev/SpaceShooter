using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LifeManager : MonoBehaviour
{
    public float LifePoints;
    private float _maxLife = 3;

    protected LaserWeapon _laserWeapon;
    protected MissileWeapon _missileWeapon;
    protected Rigidbody _rigidbody;

    protected virtual void Awake()
    {
        LifePoints = _maxLife;
        _laserWeapon = GetComponent<LaserWeapon>();
        _missileWeapon = GetComponent<MissileWeapon>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    #region AddLife
    void AddLife()
    {
        LifePoints++;
        LifePoints = Mathf.Max(LifePoints, _maxLife);
    }

    void AddLife(float life)
    {
        LifePoints += life;
        LifePoints = Mathf.Max(LifePoints, _maxLife);
    }
    #endregion

    #region RemoveLife
    public void RemoveLife()
    {
        LifePoints--;
        CheckLife();
    }

    public void RemoveLife(float life)
    {
        LifePoints -= life;
        CheckLife();
    }
    #endregion

    #region Die
    void CheckLife()
    {
        if (LifePoints <= 0)
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Equals("Planet")) return;

        Die();
    }

    protected abstract void Die();
    #endregion

    public float GetMaxLife()
    {
        return _maxLife;
    }

    public void DieCheat()
    {
        Die();
    }

    public void HealCheat()
    {
        LifePoints = _maxLife;
    }
}
