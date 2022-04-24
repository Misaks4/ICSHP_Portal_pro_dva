using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;

    private const float PollingTime = 1f;
    private int frameCount;
    private float time;
    private void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        if (!(time >= PollingTime)) return;
        var frameRate = Mathf.RoundToInt(frameCount / time);
        fpsText.text = $"{frameRate} FPS";

        time -= PollingTime;
        frameCount = 0;
    }
}