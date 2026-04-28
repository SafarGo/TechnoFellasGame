using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ShopBagLock : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private Rigidbody rb;

    private void Awake()
    {
        if (grabInteractable == null)
            grabInteractable = GetComponent<XRGrabInteractable>();

        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    public void HideBag()
    {
        gameObject.SetActive(false);
    }

    public void ShowBagLocked()
    {
        gameObject.SetActive(true);

        if (grabInteractable != null)
            grabInteractable.enabled = false;

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void UnlockBag()
    {
        gameObject.SetActive(true);

        if (grabInteractable != null)
            grabInteractable.enabled = true;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        Debug.Log("ShopBagLock: bag unlocked");
    }
}