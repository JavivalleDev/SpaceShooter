using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WaveGenerator : MonoBehaviour
{

    [Tooltip("This number will be added to the life and shootrate of enemies, and it will be doubled on each wave")]
    [SerializeField] private float _difficulty = 1;
    [SerializeField] private int _wavesQuantity = 0;
    [SerializeField] private int _enemiesQuantityPerWave = 0;
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private float _spawnRadio;

    private Player _player;
    private int _currentWave = 0;
    private int _currentEnemies = 0;
    private GameobjectPool _enemyPool;
    private bool _bGeneratingWave = false;

    private UIManager _manager;
    private float _waitTime = 5;


    void Start()
    {
        _player = FindObjectOfType<Player>();
        _enemyPool = FindObjectOfType<GameobjectPool>();
        _manager = FindObjectOfType<UIManager>();
        StartCoroutine(GenerateWaveCoroutine());
    }

    IEnumerator GenerateWaveCoroutine()
    {
        _bGeneratingWave = true;
        _manager.WriteCom("Alerta");

        for (int i = 0; i < _enemiesQuantityPerWave; ++i)
        {
            GameObject spawnPoint = _spawnPoints[i];
            GameObject newEnemy = _enemyPool.GetObjectFromPool("Enemy");

            newEnemy.transform.position = spawnPoint.transform.position + Random.insideUnitSphere * _spawnRadio;
            newEnemy.transform.forward = spawnPoint.transform.forward;
            newEnemy.transform.SetParent(spawnPoint.transform);

            Enemy enemy = newEnemy.GetComponent<Enemy>();
            enemy.LifePoints += (_currentWave + 1) + _difficulty * (_currentWave + 1);
            enemy.target = _player.gameObject;

            LaserWeapon laser = newEnemy.GetComponent<LaserWeapon>();
            laser.Damage += _currentWave + _difficulty * _currentWave;

            MissileWeapon missile = newEnemy.GetComponent<MissileWeapon>();
            missile.Damage += (_currentWave + 1) * _difficulty * _currentWave;

            _currentEnemies++;
            yield return new WaitForSeconds(.1f);
        }
        _manager.WriteCom(string.Format("Oleada {0}" +
                          "\nEnemigos en combate: {1}", _currentWave+1, _currentEnemies));
        _currentWave++;
        _enemiesQuantityPerWave += _enemiesQuantityPerWave/(_currentWave + 1);
        //_enemiesQuantityPerWave += (int)(_spawnPoints.Length/12 + _difficulty*2);

        _bGeneratingWave = false;
    }

    public void SpaceshipDestroyed()
    {
        _currentEnemies--;
        _manager.WriteCom("Enemigos restantes: " + _currentEnemies.ToString());
        if (!_bGeneratingWave && _currentEnemies == 0)
        {
            if (_currentWave < _wavesQuantity)
            {
                StartCoroutine(GenerateWaveCoroutine());
            }

            else
            {
                InvokeRepeating("LoadVictory", 0, 1);
            }
        }
    }

    private void LoadVictory()
    {
        _manager.WriteCom("Has vencido, reiniciando");
        _waitTime--;
        if (_waitTime <= 0) SceneManager.LoadScene("Victory");
    }
}
