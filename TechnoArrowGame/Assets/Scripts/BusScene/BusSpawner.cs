using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusSpawner : MonoBehaviour
{
    public static BusSpawner instance;
    public bool IsCanSpawn;
    public List<GameObject> busses = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        StartCoroutine(FirstSpawn());
    }

    public void Spawn()
    {
        if(IsCanSpawn)
        {
            Instantiate(busses[Random.Range(0, 2)], transform.position, transform.rotation);
        }
    }

    IEnumerator FirstSpawn()
    {
        yield return new WaitForSeconds(5);
        Spawn();
    }
}
