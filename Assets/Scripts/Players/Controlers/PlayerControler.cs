using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    [Header("Fire parameters")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireProjectile;
    [SerializeField] private Renderer otherPlayer;

    [Header("Movement parameters")]
    [SerializeField] private KeyBind keyBind;

    private Renderer playerRenderer;
    private const float MoveVelocity = 8f;
    private const float JumpVelocity = 22f;
    private const float CoyoteTime = 0.25f;
    private const float VerticalTick = 0.5f;
    private const float Paralyzed = 0.15f;
    private const float FallLimit = 1f;

    private Player player;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        player = new Player(gameObject, keyBind
            , new Movement(GetComponent<CapsuleCollider2D>(), GetComponent<Rigidbody2D>(), GetComponent<Animator>(), LayerMask.GetMask("Platform"), MoveVelocity, JumpVelocity,
                CoyoteTime, VerticalTick, Paralyzed, FallLimit)
            , new Projectile(fireProjectile, firePoint, FireProjectile));
    }

    private void Update()
    {
        player.Update();
    }

    private void FireProjectile(Vector2 direction)
    {
        playerRenderer.sortingOrder = 1;
        otherPlayer.sortingOrder = 0;
        fireProjectile.GetComponent<PortalProjectileControler>().FireProjectile(direction);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}