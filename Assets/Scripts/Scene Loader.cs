using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string targetScene = "Ingame";

    void OnMouseDown()
    {
        SceneManager.LoadScene(targetScene);
    }
}