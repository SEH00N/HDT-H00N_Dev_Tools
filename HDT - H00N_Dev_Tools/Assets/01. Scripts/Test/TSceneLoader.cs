using UnityEngine;
using H00N.Manager;

public class TSceneLoader : MonoBehaviour
{
    public void LoadScene()
    {
        SceneLoader.Instance.LoadSceneAsync("SceneLoader", () => {
            Debug.Log("Scene : SceneLoader Loaded!!");
        });
    }

    public void AddScene()
    {
        SceneLoader.Instance.AddSceneAsync("Scene2", () => {
            Debug.Log("Scene : Scene2 Added!!");
        });
    }

    public void UnloadScene()
    {
        SceneLoader.Instance.UnloadSceneAsync("SceneLoader", () => {
            Debug.Log("Scene : SceneLoader Unloaded!!");
        });
    }
}
