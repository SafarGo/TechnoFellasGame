using UnityEngine;

public class ATMCardTrigger : MonoBehaviour
{
    [SerializeField] private ATMManager atmManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Card"))
            return;

        atmManager.OnCardPresented();
    }
}