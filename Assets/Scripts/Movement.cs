using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    InputAction thrust;

    [SerializeField]
    private float thrustForce = 1000f;


    // Another convention is to call the variable "rb" for "rigidbody"
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        thrust.Enable();
    }

    private void FixedUpdate()
    {
        if (thrust.IsPressed())
        {

            // Add a force to the rigidbody in the upward direction
            rb.AddForce(Vector3.up * thrustForce * Time.fixedDeltaTime);


        }

    }

}
