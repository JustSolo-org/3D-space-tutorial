using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] [Range(5000f, 25000f)]
    float _launchForce = 10000f;
    [SerializeField][Range(10, 1000)] int _damage = 100;
    [SerializeField][Range(2f, 10f)] float _range = 5f;
    Rigidbody _rigidbody;
    float _duration;

    bool _outOFUEL
    {
        get
        {
            _duration = Time.deltaTime;
            return _duration <= 0f;
        }
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

     void OnEnable()
    {
        _rigidbody.AddForce(_launchForce * transform.forward);
        _duration = _range;
    }

     void Update()
    {
      if (_outOFUEL) Destroy(gameObject);  
    }

     void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Projectile collided with {collision.collider.name}");
    }

}
