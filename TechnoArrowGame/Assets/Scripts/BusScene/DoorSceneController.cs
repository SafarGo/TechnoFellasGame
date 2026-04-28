using UnityEngine;

public class DoorSceneController : MonoBehaviour
{
    [SerializeField] GameObject doorHint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            doorHint.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorHint.SetActive(false);
        }
    }
}
