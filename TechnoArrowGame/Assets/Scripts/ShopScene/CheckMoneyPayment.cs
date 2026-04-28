using UnityEngine;

public class CheckMoneyPayment : MonoBehaviour
{
    [Header("Step Manager Optional")]
    [SerializeField] private StepManager stepManager;
    [SerializeField] private int stepIndex = -1;

    [Header("Money")]
    [SerializeField] private string moneyTag = "Money";

    [Header("Bag")]
    [SerializeField] private ShopBagLock bagLock;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip moneyAcceptedClip;

    [Header("Settings")]
    [SerializeField] private bool hideMoneyAfterPayment = true;

    private bool paid = false;

    private void OnTriggerEnter(Collider other)
    {
        if (paid)
            return;

        GameObject money = GetMoneyObject(other);

        if (money == null)
            return;

        if (stepManager != null && stepIndex >= 0 && !stepManager.IsCurrentStep(stepIndex))
            return;

        paid = true;

        if (audioSource != null && moneyAcceptedClip != null)
            audioSource.PlayOneShot(moneyAcceptedClip);

        if (hideMoneyAfterPayment)
            money.SetActive(false);

        if (bagLock != null)
            bagLock.UnlockBag();

        Debug.Log("CheckMoneyPayment: money accepted");

        if (stepManager != null && stepIndex >= 0)
            stepManager.CompleteStep(stepIndex);
    }

    private GameObject GetMoneyObject(Collider other)
    {
        if (other.CompareTag(moneyTag))
            return other.gameObject;

        if (other.transform.root.CompareTag(moneyTag))
            return other.transform.root.gameObject;

        return null;
    }
}