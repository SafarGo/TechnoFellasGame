using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnLoaded;
    }

    void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnpoint = GameObject.Find("SpawnPoint");
        transform.position = spawnpoint.transform.position;
    }
}
