using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TitleScene : UI_Scene
{
    enum Buttons
    {
        LoginButton
    }

    protected override void Awake()
    {
        base.Awake();
        BindButtons(typeof(Buttons));

    }

    public void Login()
    {
        Managers.Scene.LoadScene(Define.EScene.TitleScene);
    }
}
