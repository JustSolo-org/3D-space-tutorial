using System;
using System.Security.Principal;
using UnityEngine;

public class asteroid : MonoBehaviour, IDamageable
{

    [SerializeField] private FracturedAsteroid _fracturedAsteroidPrefab;
    //[SerializeField] private Detonator _explosionPrefab;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    void IDamageable.TakeDamage(int damage, Vector3 hitPosition)
    {
        FractureAsteroid(hitPosition);
    }

    private void FractureAsteroid(Vector3 hitPosition)
    {
        if (_fracturedAsteroidPrefab != null)
        {

            Instantiate(_fracturedAsteroidPrefab, _transform.position, _transform.rotation);
        }

        //if (_explosionPrefab != null)
        //{
        //    Instantiate(_explosionPrefab, hitPosition, Quaternion.identity);
        //}

        Destroy(gameObject);
    }
}


