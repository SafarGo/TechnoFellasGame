using System.Collections;
using UnityEngine;

public class BusController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private float stopDelay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BusStation"))
        {
            speed = 0;
            anim.SetBool("IsDriving", false);
            StartCoroutine(BusStopped());
        }
    }

    IEnumerator BusStopped()
    {
        yield return new WaitForSeconds(stopDelay);
        speed = 3;
        anim.SetBool("IsDriving", true);
    }
}
