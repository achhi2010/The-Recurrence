using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OscillatoryMotion : MonoBehaviour
{
    Vector3 StartingVector;
    [SerializeField] Vector3 MovementVector;
    float MovementFactor;
    [SerializeField] float period = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartingVector = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon){return;}
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float RawSinWave = Mathf.Sin(cycles * tau);
        MovementFactor = (RawSinWave + 1f) / 2f;
        
        Vector3 offset = MovementVector *  MovementFactor;
        transform.position = StartingVector + offset;

    }
}
