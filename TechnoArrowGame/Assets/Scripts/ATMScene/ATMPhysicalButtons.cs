using UnityEngine;

public class ATMPhysicalButtons : MonoBehaviour
{
    [SerializeField] private ATMManager atmManager;
    [SerializeField] private int digit;

    public void Press()
    {
        atmManager.PressDigit(digit);
    }
}