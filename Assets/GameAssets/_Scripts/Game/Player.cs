using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LaserWeapon))]
[RequireComponent(typeof(Rigidbody))]
public class Player : LifeManager
{

    [SerializeField] private float _angularSpeed = 90;
    [SerializeField] private float _force = 0;
    [SerializeField] private float _maxForce = 50;

    [SerializeField] private AudioSource _engineAudioSource;

    public float MinSpeed = 0, MaxSpeed = 100;

    private Loop _loop;

    [HideInInspector]
    public float Speed;

    private bool _bAlive = true;

    private UIManager _manager;
    private float _waitTime = 5;

    protected override void Awake()
    {
        base.Awake();
        _manager = FindObjectOfType<UIManager>();
        _loop = GetComponent<Loop>();
    }

    void Update()
    {
        if (_bAlive)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _laserWeapon.StartShooting();
            }

            if (Input.GetButtonUp("Fire1"))
            {
                _laserWeapon.StopShooting();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                _missileWeapon.SingleShoot();
            }
            
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float acceleration = Input.GetAxis("Acceleration");

            _force = acceleration * _maxForce;

            if (!_loop.isLooping())
            {
                this.transform.Rotate(Vector3.up * horizontal * Time.deltaTime * _angularSpeed, Space.World);
                this.transform.Rotate(Vector3.right * vertical * Time.deltaTime * _angularSpeed, Space.Self);
            }

            Speed = _rigidbody.velocity.magnitude;

            if (_engineAudioSource) _engineAudioSource.volume = (Speed / MaxSpeed) / 2;
        }
    }

    void FixedUpdate()
    {
        if (_bAlive)
        {
            _rigidbody.AddForce(this.transform.forward * _force * Time.fixedDeltaTime, ForceMode.VelocityChange);
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, MaxSpeed);
            if (_rigidbody.velocity.magnitude < MinSpeed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * MinSpeed;
            }
            _rigidbody.velocity = this.transform.forward * _rigidbody.velocity.magnitude;
        }

    }

    protected override void Die()
    {
        _rigidbody.useGravity = true;
        _bAlive = false;
        InvokeRepeating("LoadGameOver", 0, 1);
    }

    private void LoadGameOver()
    {
        _manager.WriteCom("Sistemas inhabilitados, reiniciando");
        _waitTime--;
        if(_waitTime <= 0) SceneManager.LoadScene("GameOver");
    }
}
