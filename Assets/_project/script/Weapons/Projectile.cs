using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private Detonator _hitEffect;

    private float _launchForce = 10000f;
    private int _damage = 100;
    private float _range = 2f;
    private Rigidbody _rigidbody;
    private float _duration;
    private static int _playerLayer = -1;
    private static int _playerShieldLayer = -1;

    private bool OutOfFuel
    {
        get
        {
            _duration -= Time.deltaTime;
            return _duration <= 0f;
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_playerLayer == -1)
        {
            _playerLayer = LayerMask.NameToLayer("Player");
            _playerShieldLayer = LayerMask.NameToLayer("PlayerShield");
        }
    }

    public void Init(int launchForce, int damage, float range)
    {
        _launchForce = launchForce;
        _damage = damage;
        _range = range;
    }

    private void OnEnable()
    {
        if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(_launchForce * transform.forward);
        _duration = _range;
    }

    private void OnDisable()
    {
        if (_rigidbody != null)
        {
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }

    private void Update()
    {
        if (OutOfFuel) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        int layer = collision.gameObject.layer;
        // Ignore own ship and own shield
        if (layer == _playerLayer || layer == _playerShieldLayer)
        {
            if (collision.contacts.Length > 0)
            {
                Physics.IgnoreCollision(collision.collider, collision.contacts[0].thisCollider, true);
            }
            return;
        }

        IDamageable damageable = collision.collider.GetComponentInParent<IDamageable>();
        if (damageable == null)
        {
            damageable = collision.collider.GetComponent<IDamageable>();
        }
        if (damageable != null)
        {
            Vector3 hitPosition = collision.contacts.Length > 0 ? collision.GetContact(0).point : transform.position;
            damageable.TakeDamage(_damage, hitPosition);
        }

        if (_hitEffect != null)
        {
            Instantiate(_hitEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
