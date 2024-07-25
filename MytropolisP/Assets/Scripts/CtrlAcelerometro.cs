using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlAcelerometro : MonoBehaviour
{
    public float AccelerometerUpdateInterval = 1.0f / 100.0f;
    public float LowPassKernelWidthInSeconds = 0.001f;
    public Vector3 lowPassValue = Vector3.zero;


Vector3 lowpass(){
    float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; // tweakable
    lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);
    Debug.Log(lowPassValue);
    return lowPassValue;
    }
}
