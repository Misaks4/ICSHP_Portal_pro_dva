using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("Fire parameters")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireProjectile;

    [Header("Movement parameters")]
    [SerializeField] private KeyBind keyBind;

    private const float MoveVelocity = 8f;
    private const float JumpVelocity = 14f;
    private const float CoyoteTimeIni = 0.25f;
    private const float VerticalTickIni = 2f;

    private Player player;

    // Start is called before the first frame update
    private void Start()
    {
        player = new Player(gameObject, keyBind
            , new Movement(GetComponent<CapsuleCollider2D>(), LayerMask.GetMask("Platform"), MoveVelocity, JumpVelocity,
                CoyoteTimeIni, VerticalTickIni)
            , new Projectile(fireProjectile, firePoint, FireProjectile));
    }

    // Update is called once per frame
    private void Update()
    {
        player.Update();
    }

    private void FireProjectile(Vector2 direction)
    {
        fireProjectile.GetComponent<PortalProjectileControler>().FireProjectile(direction);
    }
}