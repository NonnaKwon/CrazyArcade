using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using static Define;

public class SceneManager
{
    public BaseScene CurrentScene
    {
        get { return _currentScene == null ? GameObject.FindObjectOfType<BaseScene>() : _currentScene; }
        set { _currentScene = value; }
    }
    public Define.Scene NextSceneType;

    private BaseScene _currentScene;
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
        _currentScene.Clear();
        _currentScene = null;
    }
}
