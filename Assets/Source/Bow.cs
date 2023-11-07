using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private ProjectileTrajectory _projectileTrajectory;
    [SerializeField] private Projectile _projectilePrefab;

    private float _speed;

    private Vector2 Velocity => transform.right * _speed;

    public void Pull(float speed)
    {
        _speed = speed;

        if (speed > 0)
            _projectileTrajectory.Show();
        else
            _projectileTrajectory.Hide();

        _projectileTrajectory.Draw(transform.position, Velocity);
    }

    public void Shoot()
    {
        Projectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        _projectileTrajectory.Hide();
        projectile.Shoot(Velocity);
    }
}
