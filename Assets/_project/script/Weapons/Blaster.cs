using Sirenix.OdinInspector;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] [Required] private Projectile _projectilePrefab;
    [SerializeField] private Transform _muzzle;

    private float _coolDownTime;
    private int _launchForce;
    private int _damage;
    private float _duration;
    private IWeaponControls _weaponInput;
    private float _coolDown;

    private bool CanFire
    {
        get
        {
            _coolDown -= Time.deltaTime;
            return _coolDown <= 0f;
        }
    }

    private void Update()
    {
        if (_weaponInput == null) return;
        if (CanFire && _weaponInput.PrimaryFired)
        {
            FireProjectile();
        }
    }

    public void Init(IWeaponControls weaponInput, float coolDown, int launchForce, float duration, int damage)
    {
        _weaponInput = weaponInput;
        _coolDownTime = coolDown;
        _launchForce = launchForce;
        _duration = duration;
        _damage = damage;
    }

    private void FireProjectile()
    {
        _coolDown = _coolDownTime;
        Projectile projectile = Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
        projectile.gameObject.SetActive(false);
        projectile.Init(_launchForce, _damage, _duration);
        projectile.gameObject.SetActive(true);
    }
}
