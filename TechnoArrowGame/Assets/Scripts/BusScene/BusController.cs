using System.Collections;
using UnityEngine;

public class BusController : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private float speed = 10;
    [SerializeField] private float stopDelay = 3;
    private bool isStopped = false;
    private bool isFirstStop = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BusStation") && isFirstStop)
        {
            StartCoroutine(StopAtStation());
            isFirstStop = false;
        }
    }

    IEnumerator StopAtStation()
    {
        isStopped = true;
        speed = 0;
        anim.SetBool("IsDriving", false);

        yield return new WaitForSeconds(stopDelay);

        speed = 10;
        anim.SetBool("IsDriving", true);
        isStopped = false;
    }

    void Update()
    {
        if (!isStopped)
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
    }

    private void OnDestroy()
    {
        BusSpawner.instance.BusDestroyed();
    }
}