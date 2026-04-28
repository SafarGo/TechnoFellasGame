using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasketballTrailController : MonoBehaviour
{
    [Header("References")]
    public TrailRenderer trail;

    [Header("When to show")]
    public float minSpeedToShow = 4.0f;
    public bool hideWhenGrabbed = true;

    [Header("Trail shape")]
    public float normalTime = 0.25f;
    public float fastTime = 0.45f;
    public float speedForMaxTrail = 12f;

    private Rigidbody rb;
    private bool isGrabbed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (trail != null)
        {
            trail.emitting = false;
            trail.Clear();
        }
    }

    private void Update()
    {
        if (trail == null)
            return;

        float speed = rb.linearVelocity.magnitude;

        bool shouldShow =
            speed >= minSpeedToShow &&
            (!hideWhenGrabbed || !isGrabbed);

        trail.emitting = shouldShow;

        if (!shouldShow)
        {
            trail.Clear();
            return;
        }

        float speed01 = Mathf.InverseLerp(minSpeedToShow, speedForMaxTrail, speed);
        trail.time = Mathf.Lerp(normalTime, fastTime, speed01);
    }

    public void SetGrabbed(bool grabbed)
    {
        isGrabbed = grabbed;

        if (grabbed && trail != null)
        {
            trail.emitting = false;
            trail.Clear();
        }
    }
}