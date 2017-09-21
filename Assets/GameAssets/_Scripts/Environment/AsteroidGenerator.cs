using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    
    [SerializeField] private GameObject[] _asteroids;
    [SerializeField] private GameObject[] _asteroidsNCR;
    [SerializeField] private int _asteroidCount = 100;
    [SerializeField] private Transform _beltParent;
    [SerializeField] private Transform _externalBeltParent;

    public float MinRadiusWeight = 2800;
    public float MaxRadiusWeight = 3600;
    public float RadiusHeight = 100;
    public float CloserBeltSustraction = 20;
    public float CloserBeltHeight = 100;

    void Start()
    {

        SpawnCentralBelt();
        //Segundo for para un anillo superior más cercano al planeta
        SpawnSuperiorBelt();
        //Tercer for para un anillo inferior más cercano al planeta
        SpawnInferiorBelt();

        transform.Rotate(Vector3.right, 17); //Guiño a Saturno

        //Cuarto anillo exterior
        SpawnExteriorBelt();

    }

    void SpawnCentralBelt()
    {
        for (int asteroid = 0; asteroid < _asteroidCount; ++asteroid)
        {
            Vector3 centrePos = transform.position;
            GameObject newAsteroid = Instantiate(_asteroids[Random.Range(0, _asteroids.Length)]);
            // "circlePoint" progreso entre el circulo de 0-1. Multiplico por 1 para asegurar una fraccion como resultado.
            float circlePoint = (float)(asteroid * 1.0) / _asteroidCount;
            // Calculo el angulo para el siguiente paso (en radianes)
            float angle = circlePoint * Mathf.PI * 2;
            // Calculo la X y la Z usando Seno y Coseno
            float x = Mathf.Sin(angle) * Random.Range(MinRadiusWeight, MaxRadiusWeight);
            float z = Mathf.Cos(angle) * Random.Range(MinRadiusWeight, MaxRadiusWeight);
            //Le asigno un vector con los datos obtenidos, y se lo sumo a la posicion central
            Vector3 pos = new Vector3(x, Random.Range(-RadiusHeight, RadiusHeight), z) + centrePos;
            newAsteroid.transform.position = pos;
            newAsteroid.transform.rotation = Random.rotation;
            newAsteroid.transform.parent = _beltParent;
        }
    }

    void SpawnSuperiorBelt()
    {
        for (int asteroid = 0; asteroid < _asteroidCount; ++asteroid)
        {
            Vector3 centrePos = transform.position;
            GameObject newAsteroid = Instantiate(_asteroids[Random.Range(0, _asteroids.Length)]);
            // "circlePoint" progreso entre el circulo de 0-1. Multiplico por 1 para asegurar una fraccion como resultado.
            float circlePoint = (float)(asteroid * 1.0) / _asteroidCount;
            // Calculo el angulo para el siguiente paso (en radianes)
            float angle = circlePoint * Mathf.PI * 2;
            // Calculo la X y la Z usando Seno y Coseno
            float x = Mathf.Sin(angle) * Random.Range(MinRadiusWeight - CloserBeltSustraction, MaxRadiusWeight - CloserBeltSustraction);
            float z = Mathf.Cos(angle) * Random.Range(MinRadiusWeight - CloserBeltSustraction, MaxRadiusWeight - CloserBeltSustraction);
            //Le asigno un vector con los datos obtenidos, y se lo sumo a la posicion central
            Vector3 pos = new Vector3(x, Random.Range(RadiusHeight, RadiusHeight + CloserBeltHeight), z) + centrePos;
            newAsteroid.transform.position = pos;
            newAsteroid.transform.rotation = Random.rotation;
            newAsteroid.transform.parent = _beltParent;
        }
    }

    void SpawnInferiorBelt()
    {
        for (int asteroid = 0; asteroid < _asteroidCount; ++asteroid)
        {
            Vector3 centrePos = transform.position;
            GameObject newAsteroid = Instantiate(_asteroids[Random.Range(0, _asteroids.Length)]);
            // "circlePoint" progreso entre el circulo de 0-1. Multiplico por 1 para asegurar una fraccion como resultado.
            float circlePoint = (float)(asteroid * 1.0) / _asteroidCount;
            // Calculo el angulo para el siguiente paso (en radianes)
            float angle = circlePoint * Mathf.PI * 2;
            // Calculo la X y la Z usando Seno y Coseno
            float x = Mathf.Sin(angle) * Random.Range(MinRadiusWeight - CloserBeltSustraction, MaxRadiusWeight - CloserBeltSustraction);
            float z = Mathf.Cos(angle) * Random.Range(MinRadiusWeight - CloserBeltSustraction, MaxRadiusWeight - CloserBeltSustraction);
            //Le asigno un vector con los datos obtenidos, y se lo sumo a la posicion central
            Vector3 pos = new Vector3(x, Random.Range(-RadiusHeight - CloserBeltHeight, -RadiusHeight), z) + centrePos;
            newAsteroid.transform.position = pos;
            newAsteroid.transform.rotation = Random.rotation;
            newAsteroid.transform.parent = _beltParent;
        }
    }

    void SpawnExteriorBelt()
    {
        for (int asteroid = 0; asteroid < _asteroidCount * 4; ++asteroid)
        {
            Vector3 centrePos = transform.position;
            GameObject newAsteroid = Instantiate(_asteroidsNCR[Random.Range(0, _asteroidsNCR.Length)]);
            // "circlePoint" progreso entre el circulo de 0-1. Multiplico por 1 para asegurar una fraccion como resultado.
            float circlePoint = (float)(asteroid * 1.0) / _asteroidCount;
            // Calculo el angulo para el siguiente paso (en radianes)
            float angle = circlePoint * Mathf.PI * 2;
            // Calculo la X y la Z usando Seno y Coseno
            float x = Mathf.Sin(angle) * Random.Range(MinRadiusWeight * 3, MaxRadiusWeight * 3);
            float z = Mathf.Cos(angle) * Random.Range(MinRadiusWeight * 3, MaxRadiusWeight * 3);
            //Le asigno un vector con los datos obtenidos, y se lo sumo a la posicion central
            Vector3 pos = new Vector3(x, Random.Range(-RadiusHeight * 2, RadiusHeight * 2), z) + centrePos;
            newAsteroid.transform.position = pos;
            newAsteroid.transform.rotation = Random.rotation;
            newAsteroid.transform.parent = _externalBeltParent;
        }
    }

}
