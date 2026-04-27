using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStep : MonoBehaviour
{
    public void Scene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
