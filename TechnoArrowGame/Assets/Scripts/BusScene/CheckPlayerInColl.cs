using UnityEngine;

public class CheckPlayerInColl : MonoBehaviour
{
    [SerializeField] private StepManager stepManager;
    public int step;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stepManager.CompleteStep(step);
            Destroy(this.gameObject);
        }   
    }
}
    