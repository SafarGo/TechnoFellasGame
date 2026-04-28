using UnityEngine;

public class ATMSubmitButton : MonoBehaviour
{
    [SerializeField] private ATMManager atmManager;
    public AudioSource source;
    public AudioClip clip;
    public void Press()
    {
        Debug.Log("ATMSubmitButton: Press");

        if (atmManager != null)
            atmManager.SubmitPin();
        else
            Debug.LogWarning("ATMSubmitButton: ATMManager is null");
        source.PlayOneShot(clip);
    }
}