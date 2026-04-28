using System.Collections;
using UnityEngine;

public class ATMDigitButton : MonoBehaviour
{
    [SerializeField] private ATMManager atmManager;
    [SerializeField] private int digit;

    public AudioSource source;
    public AudioClip clip;

    [Header("Press Animation")]
    [SerializeField] private float pressOffsetY = -0.01f;
    [SerializeField] private float pressDuration = 0.3f;

    private Vector3 startPosition;
    private Coroutine pressCoroutine;

    private void Awake()
    {
        startPosition = transform.position;
    }

    public void Press()
    {
        if (atmManager != null)
            atmManager.PressDigit(digit);

        PlayPressAnimation();

        source.PlayOneShot(clip);
    }

    private void PlayPressAnimation()
    {
        if (pressCoroutine != null)
            StopCoroutine(pressCoroutine);

        pressCoroutine = StartCoroutine(PressRoutine());
    }

    private IEnumerator PressRoutine()
    {
        transform.position = new Vector3(
            startPosition.x,
            startPosition.y + pressOffsetY,
            startPosition.z
        );

        yield return new WaitForSeconds(pressDuration);

        transform.position = startPosition;
        pressCoroutine = null;
    }
}