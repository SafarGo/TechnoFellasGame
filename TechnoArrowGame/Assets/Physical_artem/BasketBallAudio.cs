using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BasketballSimpleAudio : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Sounds")]
    public AudioClip grabClip;
    public AudioClip impactClip;
    public AudioClip hoopClip;

    [Header("Impact")]
    public float minImpactSpeed = 0.8f;
    public float maxImpactSpeed = 7f;
    public float minImpactVolume = 0.15f;
    public float maxImpactVolume = 0.9f;
    public float impactCooldown = 0.08f;

    [Header("Hoop")]
    public float hoopVolume = 0.85f;
    public float hoopCooldown = 0.35f;

    [Header("Variation")]
    public float pitchRandomness = 0.05f;

    private float nextImpactTime;
    private float nextHoopTime;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;

        // Для дебага сначала делаем 2D-звук.
        // Когда всё заработает, можешь вернуть spatialBlend = 1f.
        audioSource.spatialBlend = 0f;

        audioSource.dopplerLevel = 0.1f;
        audioSource.minDistance = 0.3f;
        audioSource.maxDistance = 12f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
    }

    private void Start()
    {
        Debug.Log("BasketballSimpleAudio started on " + gameObject.name);

        if (audioSource == null)
            Debug.LogError("BasketballSimpleAudio: AudioSource не найден!");

        if (grabClip == null)
            Debug.LogWarning("BasketballSimpleAudio: Grab Clip не назначен.");

        if (impactClip == null)
            Debug.LogWarning("BasketballSimpleAudio: Impact Clip не назначен.");

        if (hoopClip == null)
            Debug.LogWarning("BasketballSimpleAudio: Hoop Clip не назначен.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        float speed = collision.relativeVelocity.magnitude;

        Debug.Log("Ball collision with " + collision.gameObject.name + ", speed: " + speed);

        if (speed < minImpactSpeed)
            return;

        if (Time.time < nextImpactTime)
            return;

        float speed01 = Mathf.InverseLerp(minImpactSpeed, maxImpactSpeed, speed);
        float volume = Mathf.Lerp(minImpactVolume, maxImpactVolume, speed01);

        PlaySound(impactClip, volume);

        nextImpactTime = Time.time + impactCooldown;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ball trigger with " + other.gameObject.name + ", tag: " + other.tag);

        if (!other.CompareTag("HoopSound"))
            return;

        if (Time.time < nextHoopTime)
            return;

        PlaySound(hoopClip, hoopVolume);

        nextHoopTime = Time.time + hoopCooldown;
    }

    public void PlayGrab()
    {
        Debug.Log("PlayGrab called");
        PlaySound(grabClip, 0.65f);
    }

    public void PlayRelease()
    {
        // Пока пусто.
    }

    [ContextMenu("TEST Grab Sound")]
    public void TestGrabSound()
    {
        PlaySound(grabClip, 1f);
    }

    [ContextMenu("TEST Impact Sound")]
    public void TestImpactSound()
    {
        PlaySound(impactClip, 1f);
    }

    [ContextMenu("TEST Hoop Sound")]
    public void TestHoopSound()
    {
        PlaySound(hoopClip, 1f);
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        if (audioSource == null)
        {
            Debug.LogError("PlaySound failed: AudioSource is null");
            return;
        }

        if (clip == null)
        {
            Debug.LogError("PlaySound failed: AudioClip is null");
            return;
        }

        audioSource.pitch = 1f + Random.Range(-pitchRandomness, pitchRandomness);
        audioSource.PlayOneShot(clip, Mathf.Clamp01(volume));

        Debug.Log("Playing sound: " + clip.name + ", volume: " + volume);
    }
}