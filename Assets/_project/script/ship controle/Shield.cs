using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageable
{
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] [Range(0.25f, 1f)] private float _fadeOutTime = 0.5f;
    [SerializeField] private float _minIntensity = -10f, _maxIntensity = 0f;
    [SerializeField] private int _maxHealth = 5000;
    [SerializeField] private GameObject _explosionPrefab;

    private Renderer _renderer;
    private Color _baseColor;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private int _health;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _health = _maxHealth;
        _baseColor = _renderer.material.color;
    }

    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        _health -= damage;
        Debug.Log($"Shield took {damage} damage! Remaining health: {Mathf.Max(0, _health)}/{_maxHealth}");
        if (_health <= 0)
        {
            DestroyShields();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(FlashAndFadeShields());
    }

    private void DestroyShields()
    {
        StopAllCoroutines();
        if (_explosionPrefab != null)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private IEnumerator FlashAndFadeShields()
    {
        Color shieldColor;
        Color emissionColor = _renderer.material.GetColor(EmissionColor);
        float currentTime = 0f;
        while (currentTime < _fadeOutTime)
        {
            shieldColor = Color.Lerp(_flashColor, _baseColor, currentTime / _fadeOutTime);
            float intensity = Mathf.Lerp(_maxIntensity, _minIntensity, currentTime / _fadeOutTime);
            ChangeShieldColor(shieldColor, emissionColor * Mathf.Pow(2, intensity));
            currentTime += Time.deltaTime;
            yield return null;
        }

        ChangeShieldColor(_baseColor, emissionColor * Mathf.Pow(2, _minIntensity));
    }

    private void ChangeShieldColor(Color shieldColor, Color emissionColor)
    {
        _renderer.material.color = shieldColor;
        _renderer.material.SetColor(EmissionColor, emissionColor);
    }
}
