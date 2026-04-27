using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public void SwitchScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}
