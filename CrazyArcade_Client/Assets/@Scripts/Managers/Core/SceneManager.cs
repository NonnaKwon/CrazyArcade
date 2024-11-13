using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using static Define;

public class SceneManager
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }
    public EScene NextSceneType;

    public void LoadScene(EScene type, Transform parents = null)
    {
        NextSceneType = type;
        Managers.Clear();
        Addressables.LoadSceneAsync(GetSceneName(type), LoadSceneMode.Single, true);
    }

    public string GetSceneName(EScene type)
    {
        string name = System.Enum.GetName(typeof(EScene), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
