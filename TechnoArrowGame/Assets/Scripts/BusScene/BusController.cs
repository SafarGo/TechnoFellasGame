using System.Collections;
using UnityEngine;

public class BusController : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private float stopDelay;
    private bool _isFirstTime = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BusStation"))
        {
            if (_isFirstTime)
            {
                speed = 0;
                anim.SetBool("IsDriving", false);
                StartCoroutine(BusStopped());
                _isFirstTime = false;
            }
        }
    }

    IEnumerator BusStopped()
    {
        yield return new WaitForSeconds(stopDelay);
        speed = 3;
        anim.SetBool("IsDriving", true);
    }

    public void Update()
    {
        transform.position += new Vector3(-speed * 0.01f, 0,0);
    }
}
