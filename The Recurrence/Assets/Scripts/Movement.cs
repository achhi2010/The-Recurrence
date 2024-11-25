using System;
using System.Diagnostics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody Rb;
    AudioSource As;
    [SerializeField] float ThrustForce;
    [SerializeField] float RotationSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        As = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ThrustInput();
        RotateInput();
    }

    void ThrustInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Rb.AddRelativeForce(Vector3.up * ThrustForce * Time.deltaTime);
            if(!As.isPlaying)
            {
                As.Play();
            }
        }
        else
        {
            As.Stop();
        }
    }

    void RotateInput()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Rotation(RotationSpeed);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            Rotation(-RotationSpeed);
        }
    }

        void Rotation(float rotating)
    {
        Rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotating * Time.deltaTime);
        Rb.freezeRotation = false;
    }
}
