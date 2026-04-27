using System.Collections;
using UnityEngine;

public class CheckPaidCard : MonoBehaviour
{
    [SerializeField] private StepManager stepManager;

    [SerializeField] public AudioClip success_paid;
    [SerializeField] public AudioClip wrong_paid;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCompleted)
            return;

        if (isProcessing)
            return;

        if (!other.CompareTag("Card"))
            return;

        if (stepManager != null && !stepManager.IsCurrentStep(stepIndex))
            return;

        StartCoroutine(TryPay());
    }

    private IEnumerator TryPay()
    {
        isProcessing = true;

        bool success = Random.value <= successChance;


        if (success)
        {
            yield return new WaitForSeconds(failShowTime);

            this.gameObject.GetComponent<Renderer>().enabled = false;

            isCompleted = true;
            source.PlayOneShot(success_paid);
            successObject.SetActive(true);

            yield return new WaitForSeconds(failShowTime);

            this.gameObject.GetComponent<Renderer>().enabled = true;

            stepManager.CompleteStep(stepIndex);
        }
        else
        {
            yield return new WaitForSeconds(failShowTime);
                
            this.gameObject.GetComponent<Renderer>().enabled = false;
        
            source.PlayOneShot(wrong_paid);
            failObject.SetActive(true);

            yield return new WaitForSeconds(failShowTime);

            failObject.SetActive(false);
            this.gameObject.GetComponent<Renderer>().enabled = true;
        }

        isProcessing = false;
    }
}