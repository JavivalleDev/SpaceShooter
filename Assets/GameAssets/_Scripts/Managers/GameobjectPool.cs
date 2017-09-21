using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectPrefabs;
    private List<GameObject>[] _objects;
    public int InitialSize = 30;

    void Awake()
    {
        _objects = new List<GameObject>[_objectPrefabs.Length];
        for (int i = 0; i < _objectPrefabs.Length; ++i)
        {
            _objects[i] = new List<GameObject>();
        }
        AddObjects();
    }

    public GameObject GetObjectFromPool(string name)
    {
        for (int i = 0; i < _objectPrefabs.Length; ++i)
        {

            GameObject prefab = _objectPrefabs[i];
            if (prefab.name.Equals(name))
            {
                if (_objects[i].Count == 0)
                {
                    AddObjectsOf(i);
                }

                GameObject newObject = _objects[i][0];
                _objects[i].RemoveAt(0);

                newObject.SetActive(true);
                return newObject;
            }
        }

        return null;

    }

    public void ReturnObjectToPool(string name, GameObject newObject)
    {
        newObject.SetActive(false);
        for (int i = 0; i < _objectPrefabs.Length; ++i)
        {
            if (_objectPrefabs[i].name.Equals(name))
            {
                _objects[i].Add(newObject);
                return;
            }
        }
    }

    void AddObjects()
    {
        for (int i = 0; i < _objectPrefabs.Length; ++i)
        {
            for (int j = 0; j < InitialSize; ++j)
            {
                GameObject newObject = Instantiate(_objectPrefabs[i]);
                newObject.transform.SetParent(transform);
                newObject.SetActive(false);
                _objects[i].Add(newObject);
            }
        }
    }

    void AddObjectsOf(int index)
    {
        for (int j = 0; j < InitialSize; ++j)
        {
            GameObject newObject = Instantiate(_objectPrefabs[index]);
            newObject.transform.SetParent(transform);
            newObject.SetActive(false);
            _objects[index].Add(newObject);
        }
    }

    void AddObjects(string name)
    {
        for (int i = 0; i < _objectPrefabs.Length; ++i)
        {
            if (!_objectPrefabs[i].name.Equals(name)) continue;

            GameObject newObject = Instantiate(_objectPrefabs[i]);
            newObject.transform.SetParent(transform);
            newObject.SetActive(false);
            _objects[i].Add(newObject);

        }
    }

}

