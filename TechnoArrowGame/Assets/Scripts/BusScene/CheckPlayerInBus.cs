using UnityEngine;

public class CheckPlayerInBus : MonoBehaviour
{
    [SerializeField] private StepManager stepManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stepManager.CompleteCurrentStep();
        }
    }
}
    