using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Field : MonoBehaviour
{
    [SerializeField][Range(100, 1000)] private int _asteroidCount = 500;
    [SerializeField][Range(100f, 500f)] private float _fieldRadius = 300f;
    [SerializeField] [Range(0.5f, 5f)] private float _MaxAsteroidScale = 5f;
    [SerializeField] private List<GameObject> _asteroidPrefabs;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        SpawnAsteroid();
    }

    private void SpawnAsteroid()
    {
        for (int i = 0; i < _asteroidCount; i++)
        {
          
            GameObject asteroid = Instantiate(_asteroidPrefabs[Random.Range(0, _asteroidPrefabs.Count)], 
                _transform.position, Quaternion.identity);
            asteroid.transform.localScale = Vector3.one * Random.Range(0.5f, _MaxAsteroidScale);
            asteroid.transform.position += Random.insideUnitSphere * _fieldRadius;
            asteroid.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere *Random.Range(0.1f, 10.0f), ForceMode.Impulse);

        }
    }
}
