using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusSpawner : MonoBehaviour
{
    public static BusSpawner instance;
    public List<GameObject> busses = new List<GameObject>();
    private bool hasBus = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(FirstSpawn());
    }

    public void Spawn()
    {
        if (!hasBus)
        {
            GameObject bus = busses[Random.Range(0, busses.Count)];
            Instantiate(bus, transform.position, bus.transform.rotation);
            hasBus = true;
        }
    }

    public void BusDestroyed()
    {
        hasBus = false;
        Spawn();
    }

    IEnumerator FirstSpawn()
    {
        yield return new WaitForSeconds(5);
        Spawn();
    }
}