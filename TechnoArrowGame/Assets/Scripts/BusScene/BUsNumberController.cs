using UnityEngine;

public class BUsNumberController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().BusNumber = gameObject.tag;
            Debug.Log($"Enter the collision, {other.GetComponent<PlayerController>().BusNumber} number");
        }
    }
}
