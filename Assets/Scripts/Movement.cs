using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField]
    InputAction thrust;
    [SerializeField]
    InputAction rotation;

    [SerializeField]
    private float thrustStrength = 1000f;

    [SerializeField]
    private float rotationStrength = 100f;

    [SerializeField]
    private AudioClip mainEngineSFX;


    // Another convention is to call the variable "rb" for "rigidbody"
    private Rigidbody rb;
    AudioSource audioSource;

    [SerializeField]
    ParticleSystem mainEngineParticles;
    [SerializeField]
    ParticleSystem leftThrusterParticles;
    [SerializeField]
    ParticleSystem rightThrusterParticles;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            RotateLeft();
        }
        else if (rotationInput > 0)
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }

    }

    private void StopRotation()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationStrength);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationStrength);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false; // Unfreeze rotation so physics can take over
    }
}
