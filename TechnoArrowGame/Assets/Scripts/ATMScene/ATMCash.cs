using UnityEngine;

public class ATMCash : MonoBehaviour
{
    [SerializeField] private ATMManager atmManager;

    public void OnGrabbed()
    {
        Debug.Log("ATMCash: grabbed");

        if (atmManager != null)
            atmManager.OnCashGrabbed();
        else
            Debug.LogWarning("ATMCash: ATMManager is null");
    }
}