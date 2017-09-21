using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoAim : MonoBehaviour
{

    [SerializeField] private GameObject _raycastPoint;
    [SerializeField] private float _radius = 5;
    [SerializeField] private float _maxDistance = 400;
    [SerializeField] private LayerMask _enemyLayerMask;

    private GameObject Target;

    void Update()
    {
        RaycastHit[] spaceshipsHit = Physics.SphereCastAll(_raycastPoint.transform.position, _radius * 30, transform.forward, _maxDistance, _enemyLayerMask);
        RaycastHit[] orderedHits = spaceshipsHit.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();

        if (orderedHits.Length == 0)
        {
            Target = null;
            return;
        }

        for (int i = 0; i < orderedHits.Length; ++i) //Co probar que tenemos vision directa de alguno de ellos
        {
            RaycastHit hit;
            Vector3 direction = (orderedHits[i].transform.position - transform.position).normalized;
            if (Physics.Raycast(transform.position, direction, out hit, _maxDistance))
            {
                if (hit.transform.gameObject.Equals(orderedHits[i].transform.gameObject))
                {
                    Vector3 hitDirection = orderedHits[i].transform.position - transform.position;
                    float angle = Vector3.Angle(hitDirection, transform.forward);
                    if (angle <= 45)
                    {
                        Target = orderedHits[i].transform.gameObject;
                        break;
                    }
                    else Target = null;
                }
            }
        }
    }

    public GameObject GetTarget()
    {
        return Target;
    }
}
