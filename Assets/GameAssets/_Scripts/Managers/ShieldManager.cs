using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{

    public float MaxShield = 100;
    public float CurrentShield { get; private set; }

    [SerializeField] private float _recoveryTime = 5; //Tiempo que tardara en empezar a regenerarse el escudo
    [SerializeField] private float _regenRate = 1; //Cantidad de escudo regenerada por segundo

    private LifeManager _lifeManager;

    // Use this for initialization
    void Awake()
    {
        CurrentShield = MaxShield;
        _lifeManager = GetComponent<LifeManager>();
        InvokeRepeating("RegenShield", 0, _regenRate);
    }

    void RegenShield()
    {
        CurrentShield++;
        CurrentShield = Mathf.Min(CurrentShield, MaxShield);
    }

    public void DamageShield()
    {
        CurrentShield -= 10;
        if(CurrentShield <= 0)
        {
            DamageLife();
        }
        if (IsInvoking("RegenShield")) CancelInvoke("RegenShield");
        if (!IsInvoking("RegenShield")) InvokeRepeating("RegenShield", _recoveryTime, _regenRate);
    }

    public void DamageShield(float dmg)
    {
        CurrentShield -= dmg;
        float overDamage = CurrentShield;
        if (CurrentShield <= 0)
        {
            DamageLife(Mathf.Abs(overDamage));
        }
        if (IsInvoking("RegenShield")) CancelInvoke("RegenShield");
        if (!IsInvoking("RegenShield")) InvokeRepeating("RegenShield", _recoveryTime, _regenRate);
    }

    void DamageLife()
    {
        _lifeManager.RemoveLife();
    }

    void DamageLife(float dmg)
    {
        _lifeManager.RemoveLife(dmg);
    }

    public void ShieldCheat()
    {
        CurrentShield = MaxShield;
    }
}
