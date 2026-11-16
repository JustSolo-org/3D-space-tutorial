using Sirenix.OdinInspector;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageable
{
    [SerializeField] private FracturedAsteroid _fracturedAsteroidPrefab;
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)] [SerializeField] private Detonator _explosionPrefab;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        FractureAsteroid(hitPosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if collision is with the player shield or ship
        Shield shield = collision.collider.GetComponentInParent<Shield>();
        if (shield == null)
        {
            shield = collision.collider.GetComponent<Shield>();
        }

        if (shield == null)
        {
            ShipControl ship = collision.collider.GetComponentInParent<ShipControl>();
            if (ship != null)
            {
                shield = ship.GetComponentInChildren<Shield>();
            }
        }

        Vector3 hitPoint = collision.contacts.Length > 0 ? collision.GetContact(0).point : transform.position;
        float relativeSpeed = collision.relativeVelocity.magnitude;
        // Collision damage scales with impact speed (250 minimum up to 1500)
        int damage = Mathf.Clamp((int)(relativeSpeed * 50f), 250, 1500);

        if (shield != null)
        {
            shield.TakeDamage(damage, hitPoint);
            FractureAsteroid(hitPoint);
            return;
        }

        // Check if collision is with a ship hull (Player or Enemy layer)
        int layer = collision.gameObject.layer;
        int colLayer = collision.collider.gameObject.layer;
        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        if (layer == playerLayer || colLayer == playerLayer || layer == enemyLayer || colLayer == enemyLayer)
        {
            IDamageable damageable = collision.collider.GetComponentInParent<IDamageable>();
            if (damageable != null && (MonoBehaviour)damageable != this)
            {
                damageable.TakeDamage(damage, hitPoint);
            }
            FractureAsteroid(hitPoint);
        }
    }

    private void FractureAsteroid(Vector3 hitPosition)
    {
        if (_fracturedAsteroidPrefab != null)
        {
            FracturedAsteroid fractured = Instantiate(_fracturedAsteroidPrefab, _transform.position, _transform.rotation);
            fractured.transform.localScale = _transform.localScale;
            fractured.Scatter(hitPosition);
        }

        if (_explosionPrefab != null)
        {
            Detonator explosion = Instantiate(_explosionPrefab, _transform.position, Quaternion.identity);
            if (explosion != null)
            {
                explosion.size *= _transform.localScale.x;
            }
        }

        Destroy(gameObject);
    }
}
