using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    private bool _isHit;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isHit == false) 
            transform.up = _rigidBody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isHit == false)
        {
            _rigidBody.isKinematic = true;
            _rigidBody.velocity = Vector2.zero;
            _isHit = true;
        }
    }

    public void Shoot(Vector2 velocity)
    {
        _rigidBody.velocity = velocity;
    }
}
