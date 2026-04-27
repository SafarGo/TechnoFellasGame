using UnityEngine;

public class ATMSubmitButton : MonoBehaviour
{
    [SerializeField] private ATMManager atmManager;

    public void Press()
    {
        Debug.Log("ATMSubmitButton: Press");

        if (atmManager != null)
            atmManager.SubmitPin();
        else
            Debug.LogWarning("ATMSubmitButton: ATMManager is null");
    }
}