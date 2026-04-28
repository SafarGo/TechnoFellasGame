using UnityEngine;

public class CheckMoneyPayment : MonoBehaviour
{
    [Header("Step Manager Optional")]
    [SerializeField] private StepManager stepManager;
    [SerializeField] private int stepIndex = -1;

    [Header("Money")]
    [SerializeField] private string moneyTag = "Money";

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip moneyAcceptedClip;

    [Header("Objects")]
    [SerializeField] private GameObject moneyAcceptedObject;

    [Header("Settings")]
    [SerializeField] private bool hideMoneyAfterPayment = true;

    private bool paid = false;

    private void Start()
    {
        if (moneyAcceptedObject != null)
            moneyAcceptedObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (paid)
            return;

        if (!other.CompareTag(moneyTag))
            return;

        if (stepManager != null && stepIndex >= 0 && !stepManager.IsCurrentStep(stepIndex))
            return;

        paid = true;

        if (audioSource != null && moneyAcceptedClip != null)
            audioSource.PlayOneShot(moneyAcceptedClip);

        if (moneyAcceptedObject != null)
            moneyAcceptedObject.SetActive(true);

        if (hideMoneyAfterPayment)
            other.gameObject.SetActive(false);

        if (stepManager != null && stepIndex >= 0)
            stepManager.CompleteStep(stepIndex);
    }
}