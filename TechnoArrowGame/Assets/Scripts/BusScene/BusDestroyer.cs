using UnityEngine;

public class BusDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bus"))
        {
            Destroy(other.gameObject);
            BusSpawner.instance.Spawn();
        }
    }
}
