using System;
using TMPro;
using UnityEngine;

public class TimerControler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI smallTimer;
    [SerializeField] private TextMeshProUGUI bigTimer;

    private bool isRunning = true;
    public float Time { get; set; }

    public void Update()
    {
        if (!isRunning) return;
        Time += UnityEngine.Time.deltaTime;
        smallTimer.text = TimeSpan.FromSeconds(Time).ToString("g");
    }

    public TimeSpan Stop()
    {
        isRunning = false;
        var elapsed = TimeSpan.FromSeconds(Time);
        smallTimer.text = elapsed.ToString("g");
        bigTimer.text = $"Level dokonèen {elapsed:g}";
        return elapsed;
    }
}