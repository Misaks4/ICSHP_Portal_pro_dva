using UnityEngine;

public class PortalControler : MonoBehaviour
{
    [Header("Portal connection parameters")]
    [SerializeField] private GameObject secondPortal;
    [SerializeField] private Transform secondPortalPoint;

    private const float Force = 16f;

    private Portal portal;

    // Start is called before the first frame update
    private void Start()
    {
        portal = new Portal(gameObject, secondPortal, secondPortalPoint, Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        portal.OnTriggerEnter2D(collision);
    }
}