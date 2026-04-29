using System.Collections;
using UnityEngine;

public class CheckPaidCard : MonoBehaviour
{
    [SerializeField] private StepManager stepManager;

    [SerializeField] public AudioClip success_paid;
    [SerializeField] public AudioClip success_paid_1;
    [SerializeField] public AudioClip wrong_paid;
    [SerializeField] public AudioClip wrong_paid_1;
    [SerializeField] public AudioClip wrong_paid_2;
    [SerializeField] public AudioClip wrong_bus;
    [SerializeField] public AudioSource source;

    [Header("Step")]
    [SerializeField] private int stepIndex = 2;

    [Header("Chance")]
    [Range(0f, 1f)]
    [SerializeField] private float successChance = 0.7f;

    [Header("Feedback")]
    [SerializeField] private GameObject successObject;
    [SerializeField] private GameObject failObject;
    [SerializeField] private float failShowTime = 1f;

    private bool isCompleted = false;
    private bool isProcessing = false;

    private void Start()
    {
        if (successObject != null)
            successObject.SetActive(false);

        if (failObject != null)
            failObject.SetActive(false);

        if (stepManager == null)
            stepManager = GameObject.Find("StepManager").GetComponent<StepManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCompleted)
            return;

        if (isProcessing)
            return;

        if (!other.CompareTag("Card"))
            return;

        StartCoroutine(TryPay());
    }

    private IEnumerator TryPay()
    {
        isProcessing = true;

        bool success = Random.value <= successChance;

        if (success)
        {
            if (CompareTag("67"))
            {
                isCompleted = true;

                source.PlayOneShot(success_paid);
                source.PlayOneShot(success_paid_1);

                successObject.SetActive(true);

                yield return new WaitForSeconds(failShowTime);

                successObject.SetActive(false);

                stepManager.CompleteStep(stepIndex);
            }
            else
            {
                isCompleted = true;

                source.PlayOneShot(success_paid);
                source.PlayOneShot(wrong_bus);

                successObject.SetActive(true);

                yield return new WaitForSeconds(failShowTime);

                successObject.SetActive(false);
            }
        }
        else
        {
            source.PlayOneShot(wrong_paid);

            AudioClip randomWrongClip = Random.value < 0.5f ? wrong_paid_1 : wrong_paid_2;
            source.PlayOneShot(randomWrongClip);

            failObject.SetActive(true);

            yield return new WaitForSeconds(failShowTime);

            failObject.SetActive(false);
        }

        isProcessing = false;
    }
}