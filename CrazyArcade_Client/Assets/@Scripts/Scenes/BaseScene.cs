using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
	public bool TestMode = true;

    public Define.EScene SceneType { get; protected set; } = Define.EScene.Unknown;

    protected virtual void Awake()
	{
		#if UNITY_EDITOR
		TestMode = true;
#else
		TestMode = false;
#endif
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";

        if (TestMode)
            SetTestMode();
    }

	protected virtual void Start() { }
	
	protected virtual void Update() { }

	public abstract void Clear();
	private void SetTestMode()
	{
        if (Managers.Resource.IsPreload)
            return;

        Managers.Resource.LoadAllAsync<GameObject>("Preload", (a, b, c) =>
        {
            Debug.Log("Load Complete");
        });

        Managers.Network.Init();
    }
}
