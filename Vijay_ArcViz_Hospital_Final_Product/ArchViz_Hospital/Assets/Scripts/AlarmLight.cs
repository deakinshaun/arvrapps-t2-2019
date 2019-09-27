using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    private float fadeSpeed = 2f;
    private float highIntensity = 2f;
    private float lowIntensity = 0.5f;
    private float changeMargin = 0.2f;
    public bool alarmOn = false;
    private Light alarmlight;

    private float targetIntensity;

    void Awake()
    {
        alarmlight = GetComponent<Light>();
        alarmlight.intensity = 0f;
        targetIntensity = highIntensity;
    }
    void Update()
    {
        if (alarmOn)
        {
            alarmlight.intensity = Mathf.Lerp(alarmlight.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
            CheckTargetIntensity();
        }
        else
        {
            alarmlight.intensity = Mathf.Lerp(alarmlight.intensity, 0f, fadeSpeed * Time.deltaTime);
        }
    }

    void CheckTargetIntensity()
    {
        if(Mathf.Abs(targetIntensity - alarmlight.intensity) < changeMargin)
        {
            if (targetIntensity == highIntensity)
                targetIntensity = lowIntensity;
            else
                targetIntensity = highIntensity;
        }
    }
}
