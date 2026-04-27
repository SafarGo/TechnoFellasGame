using UnityEngine;

public class ATMClearButton : MonoBehaviour
{
    [SerializeField] private ATMManager atmManager;

    public void Press()
    {
        Debug.Log("ATMClearButton: Press");

        if (atmManager != null)
            atmManager.ClearPin();
        else
            Debug.LogWarning("ATMClearButton: ATMManager is null");
    }
}