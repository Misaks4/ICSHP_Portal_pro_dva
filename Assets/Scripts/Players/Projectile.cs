using UnityEngine;

public class Projectile
{
    public delegate void FireProjectileEventHandler(Vector2 direction);

    public Projectile(GameObject projectileObject, Transform firePoint,
        FireProjectileEventHandler fireProjectile)
    {
        ProjectileObject = projectileObject;
        FirePoint = firePoint;
        FireProjectileEvent += fireProjectile;

        FireCooldown = 0.5f;
        CooldownTimer = Mathf.Infinity;
    }

    public float FireCooldown { get; }
    public float CooldownTimer { get; set; }
    public GameObject ProjectileObject { get; }
    public Transform FirePoint { get; }
    public Animator Animator { get; }
    public event FireProjectileEventHandler FireProjectileEvent;

    public void FireProjectile(Vector2 direction)
    {
        FireProjectileEvent?.Invoke(direction);
    }
}