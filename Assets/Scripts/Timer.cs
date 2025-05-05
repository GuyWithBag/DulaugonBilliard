using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float startingTimeInSeconds = 3600f; // Editable in Inspector (e.g., 3600 = 1 hour)
    [SerializeField] private Text timerText; // Reference to UI Text component

    private float currentTime;
    public bool isRunning = false;

    void Start()
    {
        currentTime = startingTimeInSeconds;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                isRunning = false;
            }
            UpdateTimerDisplay();
        }
    }

    // Converts time in seconds to "00:00:00" format
    private void UpdateTimerDisplay()
    {
        int hours = Mathf.FloorToInt(currentTime / 3600);
        int minutes = Mathf.FloorToInt((currentTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    // Call these methods to control the timer
    public void StartTimer()
    {
        isRunning = true;
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        currentTime = startingTimeInSeconds;
        isRunning = false;
        UpdateTimerDisplay();
    }
}