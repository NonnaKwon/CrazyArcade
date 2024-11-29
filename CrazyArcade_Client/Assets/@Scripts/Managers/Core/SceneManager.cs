using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using static Define;

public class SceneManager
{
    public BaseScene CurrentScene { get; set; }
    public Define.Scene NextSceneType;

    public void LoadScene(Define.Scene type, Transform parents = null)
    {
        NextSceneType = type;
        Managers.Clear();
        Addressables.LoadSceneAsync(GetSceneName(type), LoadSceneMode.Single, true);
    }

    public string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
