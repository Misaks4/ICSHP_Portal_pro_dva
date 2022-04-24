using UnityEngine;

public class PortalProjectileControler : MonoBehaviour
{
    [Header("Portal parameters")] [SerializeField]
    private GameObject portal;

    [SerializeField] private LayerMask portalHitLayerMask;

    private const float Speed = 20;
    private const float Starvation = 1f;
    private const float Range = 0.01f;
    private const float PortalOffSet = -0.03f;
    private readonly Vector2 bounds = new Vector2(0.2f, 0.2f);

    private PortalProjectile portalProjectile;

    private void Start()
    {
        portalProjectile =
            new PortalProjectile(gameObject, portal, portalHitLayerMask, bounds, Range, PortalOffSet, Speed,
                Starvation);
    }

    private void Update()
    {
        portalProjectile.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        portalProjectile.OnTriggerEnter2D(collision);
    }

    public void FireProjectile(Vector2 direction)
    {
        portalProjectile.FireProjectile(direction);
    }
}