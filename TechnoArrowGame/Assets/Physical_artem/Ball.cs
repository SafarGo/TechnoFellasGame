using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasketballVelocityLimiter : MonoBehaviour
{
    [Header("Realistic VR basketball limits")]
    public float maxLinearSpeed = 16f;
    public float maxAngularSpeed = 80f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngularSpeed;
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxLinearSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxLinearSpeed;

        if (rb.angularVelocity.magnitude > maxAngularSpeed)
            rb.angularVelocity = rb.angularVelocity.normalized * maxAngularSpeed;
    }
}