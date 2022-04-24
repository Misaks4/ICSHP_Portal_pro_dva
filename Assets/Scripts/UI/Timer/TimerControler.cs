using System;
using TMPro;
using UnityEngine;

public class TimerControler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI smallTimer;
    [SerializeField] private TextMeshProUGUI bigTimer;

    private bool isRunning = true;
    private float time;

    public void Update()
    {
        if (!isRunning) return;
        time += Time.deltaTime;
        smallTimer.text = TimeSpan.FromSeconds(time).ToString("g");
    }

    public TimeSpan Stop()
    {
        isRunning = false;
        var elapsed = TimeSpan.FromSeconds(time);
        smallTimer.text = elapsed.ToString("g");
        bigTimer.text = $"Level dokonèen {elapsed:g}";
        return elapsed;
    }
}