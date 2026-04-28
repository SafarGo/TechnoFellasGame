using System.Collections;
using UnityEngine;

public class BusController : MonoBehaviour
{
    [SerializeField] private Animator[] wheelAnimators;
    [SerializeField] private float speed = 10;
    [SerializeField] private float stopDelay = 3;
    private bool isStopped = false;
    private bool isFirstStop = true;
    private bool isPlayerInseide = false;

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
        foreach (var wheel in wheelAnimators)
        {
            wheel.SetBool("IsDriving", false);
        }

        yield return new WaitForSeconds(stopDelay);
        if (!isPlayerInseide)
        {
            speed = 10;
            foreach (var wheel in wheelAnimators)
            {
                wheel.SetBool("IsDriving", true);
            }
            isStopped = false;
        }
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

    public void SetPlayerInside()
    {
        isPlayerInseide = true;
    }

    public void SetPlayerOutnside()
    {
        isPlayerInseide = false;
        speed = 10f;
        isStopped = false;
    }
}