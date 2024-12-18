﻿using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
	public bool TestMode = true;

    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    protected virtual void Awake()
	{
		#if UNITY_EDITOR
		TestMode = true;
#else
		TestMode = true;
#endif

        if (TestMode)
            SetTestMode();

    }

	protected virtual void Start()
    {
        Managers.Scene.CurrentScene = this;
    }

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
