using UnityEngine;

public class CheckPaidCard : MonoBehaviour
{
    [SerializeField] private StepManager stepManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Card")
        {
            stepManager.CompleteStep(2);
        }
    }
}
