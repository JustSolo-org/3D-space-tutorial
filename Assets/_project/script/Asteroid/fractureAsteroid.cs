using UnityEngine;

public class FracturedAsteroid : MonoBehaviour
{
    [SerializeField] [Range(1f, 60f)] private float _duration = 10f;
    [SerializeField] private float _explosionForce = 400f;
    [SerializeField] private float _explosionRadius = 30f;

    private bool _scattered = false;

    public void Scatter(Vector3 hitPosition)
    {
        _scattered = true;
        Vector3 origin = hitPosition != Vector3.zero ? hitPosition : transform.position;
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.useGravity = false;
            rb.AddExplosionForce(_explosionForce, origin, _explosionRadius, 0f, ForceMode.Impulse);
        }
    }

    private void Start()
    {
        if (!_scattered)
        {
            Scatter(transform.position);
        }
    }

    private void OnEnable()
    {
        Destroy(gameObject, _duration);
    }
}
