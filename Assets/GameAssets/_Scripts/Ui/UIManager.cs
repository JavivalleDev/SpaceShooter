using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Player _player;
    private GameObject _target;

    [SerializeField] private Image _playerLifeBar;
    [SerializeField] private Image _playerShieldBar;
    [SerializeField] private Image _playerSpeedBar;
    [SerializeField] private Image _targetLifeBar;
    [SerializeField] private Image _targetShieldBar;
    [SerializeField] private Image _laserImage;
    [SerializeField] private Image[] _missileImage;
    [SerializeField] private Text _comPanelText;

    [SerializeField] private AudioSource _audio;


    private AutoAim _playerAutoAim;
    private LaserWeapon _playerLaser;
    private MissileWeapon _playerMissile;
    private ShieldManager _playerShieldManager;

    private float _textWait = 5, _textStart;
    private bool _bWaiting = true, _bWriting = false;

    // Use this for initialization
    void Awake()
    {
        _player = FindObjectOfType<Player>();
        _playerAutoAim = _player.GetComponent<AutoAim>();
        _playerLaser = _player.GetComponent<LaserWeapon>();
        _playerMissile = _player.GetComponent<MissileWeapon>();
        _playerShieldManager = _player.GetComponent<ShieldManager>();

        InvokeRepeating("WaitingCom", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        _playerLifeBar.fillAmount = _player.LifePoints / _player.GetMaxLife();
        _playerShieldBar.fillAmount = _playerShieldManager.CurrentShield / _playerShieldManager.MaxShield;
        _playerSpeedBar.fillAmount = _player.Speed / _player.MaxSpeed;

        _laserImage.fillAmount = _playerLaser.CurrentOverHeat / _playerLaser.MaxOverHeat;

        switch (_playerMissile.CurrentAmmo)
        {
            case 0:
                for (int i = 0; i < _missileImage.Length; ++i)
                {
                    _missileImage[i].enabled = false;
                }
                break;

            case 1:
                for (int i = 0; i < _missileImage.Length; ++i)
                {
                    _missileImage[i].enabled = false;
                }
                _missileImage[0].enabled = true;
                break;

            case 2:
                for (int i = 0; i < _missileImage.Length - 1; ++i)
                {
                    _missileImage[i].enabled = true;
                }
                _missileImage[2].enabled = false;
                break;

            case 3:
                for (int i = 0; i < _missileImage.Length; ++i)
                {
                    _missileImage[i].enabled = true;
                }
                break;

        }

        _target = _playerAutoAim.GetTarget();
        if (_target)
        {
            LifeManager targetLife = _target.GetComponent<LifeManager>();
            if (targetLife) _targetLifeBar.fillAmount = targetLife.LifePoints / targetLife.GetMaxLife();

            ShieldManager targetShield = _target.GetComponent<ShieldManager>();
            if (targetShield) _targetShieldBar.fillAmount = targetShield.CurrentShield / targetShield.MaxShield;
        }
        else
        {
            _targetLifeBar.fillAmount = 0;
            _targetShieldBar.fillAmount = 0;
        }

        if (!_bWaiting && Time.time > _textStart + _textWait)
        {
            InvokeRepeating("WaitingCom", 0, 1);
        }

    }

    public void WriteCom(string text)
    {
        CancelInvoke("WaitingCom");
        //_comPanelText.text = text;
        if (_bWriting) return;
        StartCoroutine(WriteTextCoroutine(text));
        _bWaiting = false;
        _textStart = Time.time;
    }

    void WaitingCom()
    {
        _bWaiting = true;

        _comPanelText.text = _comPanelText.text.Equals("_") ? "" : "_";
    }

    IEnumerator WriteTextCoroutine(string text)
    {
        _bWriting = true;
        _comPanelText.text = "";
        for (int i = 0; i < text.Length; ++i)
        {
            _comPanelText.text += text[i];
            if(_audio) _audio.PlayOneShot(_audio.clip);
            yield return new WaitForSeconds(0.03f);
        }
        _bWriting = false;
    }
}
