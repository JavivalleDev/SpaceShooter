using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIcon : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private AudioSource _audio;
    
    private Player _player;
    private bool _bIsPlayed;

    private SpriteRenderer _sprite;

    void Awake()
    {
        _sprite = sprite.GetComponent<SpriteRenderer>();
        _player = FindObjectOfType<Player>();
    }

    void Update()
    {
        sprite.transform.Rotate(Vector3.forward);
        GameObject playerTarget = _player.GetComponent<WeaponBase>().GetTarget();
        if (playerTarget)
        {
            _sprite.enabled = playerTarget.Equals(gameObject);
            if (_bIsPlayed || !_sprite.enabled) return;
            _audio.PlayOneShot(_audio.clip);
            _bIsPlayed = true;
            return;
        }

        _sprite.enabled = false;
        _bIsPlayed = false;

    }
}
