using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CheckBasketOnBelt : MonoBehaviour
{
    [Header("Step Manager Optional")]
    [SerializeField] private StepManager stepManager;
    [SerializeField] private int stepIndex = -1;

    [Header("Basket")]
    [SerializeField] private string basketTag = "Basket";
    [SerializeField] private Transform basketSnapPoint;

    [Header("Bag")]
    [SerializeField] private ShopBagLock bagLock;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip basketPlacedClip;
    [SerializeField] private AudioClip scanProductsClip;

    [Header("Timing")]
    [SerializeField] private float delayBeforeScanning = 0.5f;
    [SerializeField] private float delayBeforeBagAppears = 1.5f;

    private bool basketPlaced = false;

    private void Start()
    {
        if (bagLock != null)
            bagLock.HideBag();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (basketPlaced)
            return;

        GameObject basket = GetBasketObject(other);

        if (basket == null)
            return;

        if (stepManager != null && stepIndex >= 0 && !stepManager.IsCurrentStep(stepIndex))
            return;

        basketPlaced = true;

        other.gameObject.SetActive(false);

        SnapBasket(basket);

        StartCoroutine(BasketRoutine());
    }

    private GameObject GetBasketObject(Collider other)
    {
        if (other.CompareTag(basketTag))
            return other.gameObject;

        if (other.transform.root.CompareTag(basketTag))
            return other.transform.root.gameObject;

        return null;
    }

    private void SnapBasket(GameObject basket)
    {
        XRGrabInteractable grabInteractable = basket.GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.enabled = false;
        }

        Rigidbody rb = basket.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        if (basketSnapPoint != null)
        {
            basket.transform.SetPositionAndRotation(
                basketSnapPoint.position,
                basketSnapPoint.rotation
            );
        }

        Collider[] colliders = basket.GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            col.isTrigger = false;
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

        if (bagLock != null)
            bagLock.ShowBagLocked();

        if (stepManager != null && stepIndex >= 0)
            stepManager.CompleteStep(stepIndex);
    }
}