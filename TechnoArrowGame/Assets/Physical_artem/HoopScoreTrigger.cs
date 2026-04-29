using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HoopScoreTrigger : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text scoreText;

    [Header("Score")]
    public int pointsPerScore = 2;
    public int score = 0;

    [Header("Ball check")]
    public bool requireBallTag = false;
    public string ballTag = "Basketball";

    [Header("Anti-fake settings")]
    public float minDownwardVelocity = 0.4f;
    public float cooldownPerBall = 0.7f;

    [Header("Magnet")]
    public float magnetStrength = 5f;

    [Header("Debug")]
    public bool debugLogs = true;

    private readonly Dictionary<Rigidbody, float> nextScoreTimeByBall = new();

    private void Awake()
    {
        Collider triggerCollider = GetComponent<Collider>();
        triggerCollider.isTrigger = true;
        UpdateScoreText();
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody ballRigidbody = other.attachedRigidbody;
        if (ballRigidbody == null)
            ballRigidbody = other.GetComponentInParent<Rigidbody>();
        if (ballRigidbody == null)
            return;

        if (ballRigidbody.linearVelocity.y > -minDownwardVelocity)
            return;

        Vector3 directionToCenter = transform.position - ballRigidbody.position;
        ballRigidbody.AddForce(directionToCenter * magnetStrength, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody ballRigidbody = other.attachedRigidbody;
        if (ballRigidbody == null)
            ballRigidbody = other.GetComponentInParent<Rigidbody>();
        if (ballRigidbody == null)
            return;

        if (requireBallTag)
        {
            bool hasBallTag =
                other.CompareTag(ballTag) ||
                ballRigidbody.CompareTag(ballTag);
            if (!hasBallTag)
                return;
        }

        if (ballRigidbody.linearVelocity.y > -minDownwardVelocity)
        {
            if (debugLogs)
                Debug.Log("HoopScoreTrigger: не засчитано, м€ч не летел вниз.");
            return;
        }

        if (nextScoreTimeByBall.TryGetValue(ballRigidbody, out float nextAllowedTime))
        {
            if (Time.time < nextAllowedTime)
                return;
        }

        AddScore(pointsPerScore);
        nextScoreTimeByBall[ballRigidbody] = Time.time + cooldownPerBall;

        if (debugLogs)
            Debug.Log("Score! Total: " + score);
    }

    private void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "ќчки: " + score;
    }

    [ContextMenu("Reset Score")]
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
}