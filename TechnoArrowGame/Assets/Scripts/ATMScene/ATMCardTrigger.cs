using UnityEngine;

public class ATMCardTrigger : MonoBehaviour
{
    [SerializeField] private ATMManager atmManager;
    public AudioSource source;
    public AudioClip clip;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ATMCardTrigger: something entered: " + other.name);

        if (!other.CompareTag("Card"))
            return;

        Debug.Log("ATMCardTrigger: card detected");

        if (atmManager != null)
            atmManager.OnCardPresented();
        else
            Debug.LogWarning("ATMCardTrigger: ATMManager is null");

        source.PlayOneShot(clip);
    }
}