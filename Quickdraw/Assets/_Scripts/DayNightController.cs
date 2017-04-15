using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    public Light sun;
    public float secondsInFullDay = 120f;
    [Range(0, 1)]
    public float currentTimeOfDay = 0;
    [HideInInspector]
    public float timeMultiplier = 1f;

    float sunInitialIntensity;

    void Start()
    {
        sunInitialIntensity = sun.intensity;
    }

    void Update()
    {
        if (gameManager.GameState == "Movement")
        {
            UpdateSun();

            currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

            if (currentTimeOfDay >= 1)
            {
                currentTimeOfDay = 0;
            }
        }
        else
        {
            UpdateSun();
        }
    }

    void UpdateSun()
    {
        float intensityMultiplier = 1;
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 90f), 170, 0);

        if(gameManager.GameState != "Movement")
        {
            currentTimeOfDay = 1;
        }

        intensityMultiplier = 1;

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}