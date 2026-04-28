using System.Collections;
using UnityEngine;

public class CheckBasketOnBelt : MonoBehaviour
{
    [Header("Step Manager Optional")]
    [SerializeField] private StepManager stepManager;
    [SerializeField] private int stepIndex = -1;

    [Header("Basket")]
    [SerializeField] private string basketTag = "Basket";
    [SerializeField] private Transform basketSnapPoint;

    [Header("Objects")]
    [SerializeField] private GameObject bagObject;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip basketPlacedClip;
    [SerializeField] private AudioClip scanProductsClip;

    [Header("Timing")]
    [SerializeField] private float delayBeforeScanning = 0.5f;
    [SerializeField] private float delayBeforeBagAppears = 1.5f;

    [Header("Settings")]
    [SerializeField] private bool freezeBasketAfterPlaced = true;

    private bool basketPlaced = false;

    private void Start()
    {
        if (bagObject != null)
            bagObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (basketPlaced)
            return;

        if (!other.CompareTag(basketTag))
            return;

        if (stepManager != null && stepIndex >= 0 && !stepManager.IsCurrentStep(stepIndex))
            return;

        basketPlaced = true;

        GameObject basket = other.gameObject;

        SnapBasket(basket);

        StartCoroutine(BasketRoutine());
    }

    private void SnapBasket(GameObject basket)
    {
        if (basketSnapPoint != null)
        {
            basket.transform.position = basketSnapPoint.position;
            basket.transform.rotation = basketSnapPoint.rotation;
        }

        Rigidbody rb = basket.GetComponent<Rigidbody>();

        if (rb != null && freezeBasketAfterPlaced)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    private IEnumerator BasketRoutine()
    {
        if (audioSource != null && basketPlacedClip != null)
            audioSource.PlayOneShot(basketPlacedClip);

        yield return new WaitForSeconds(delayBeforeScanning);

        if (audioSource != null && scanProductsClip != null)
            audioSource.PlayOneShot(scanProductsClip);

        yield return new WaitForSeconds(delayBeforeBagAppears);

        if (bagObject != null)
            bagObject.SetActive(true);

        if (stepManager != null && stepIndex >= 0)
            stepManager.CompleteStep(stepIndex);
    }
}