using UnityEngine;

public class LevelCameraControler : MonoBehaviour
{
    [SerializeField] private Transform[] players;

    private const float Speed = 2;

    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        var tPositionX = 0f;
        var tPositionY = 0f;
        foreach (var player in players)
        {
            tPositionX += player.position.x;
            tPositionY += player.position.y;
        }

        tPositionX /= players.Length;
        tPositionY /= players.Length;

        transform.position = Vector3.SmoothDamp(transform.position,
            new Vector3(tPositionX, tPositionY + 4f, transform.position.z), ref velocity, Speed);
    }
}