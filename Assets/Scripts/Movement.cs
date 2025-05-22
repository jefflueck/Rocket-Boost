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
    private AudioClip mainEngine;

    // Another convention is to call the variable "rb" for "rigidbody"
    private Rigidbody rb;
    AudioSource audioSource;

    [SerializeField]
    ParticleSystem thrustParticles;

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

            // Add a force to the rigidbody in the upward direction
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
                thrustParticles.Play();
            }
        }
        else
        {
            audioSource.Stop();
            thrustParticles.Stop();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
        }
        else
        {
            // No rotation input, do nothing
        }

    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false; // Unfreeze rotation so physics can take over
    }
}
