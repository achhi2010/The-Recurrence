using System;
using System.Diagnostics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody Rb;
    AudioSource As;
    [SerializeField] float ThrustForce;
    [SerializeField] float RotationSpeed;
    [SerializeField] AudioClip ThrustClip;
    [SerializeField] ParticleSystem JetParticles;
    [SerializeField] ParticleSystem LeftThrusterParticles;
    [SerializeField] ParticleSystem RightThrusterParticles;
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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StopThrusting()
    {
        As.Stop();
        JetParticles.Stop();
    }

    void StartThrusting()
    {
        Rb.AddRelativeForce(Vector3.up * ThrustForce * Time.deltaTime);
        if (!As.isPlaying)
        {
            As.PlayOneShot(ThrustClip);
        }
        if (!JetParticles.isPlaying) { JetParticles.Play(); }
    }

    void RotateInput()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            RotatingLeft();
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            RotatingRight();
        }
        else
        {
            NotRotating();
        }
    }

    void NotRotating()
    {
        LeftThrusterParticles.Stop();
        RightThrusterParticles.Stop();
    }

    void RotatingRight()
    {
        if (!RightThrusterParticles.isPlaying) { RightThrusterParticles.Play(); }
        Rotation(-RotationSpeed);
    }

    void RotatingLeft()
    {
        if (!LeftThrusterParticles.isPlaying) { LeftThrusterParticles.Play(); }
        Rotation(RotationSpeed);
    }

    void Rotation(float rotating)
    {
        Rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotating * Time.deltaTime);
        Rb.freezeRotation = false;
    }
}
