using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomScene : BaseScene
{
    private UI_GameRoomScene _ui;
    private GameRoom _roomInfo;


    protected override void Awake()
    {
        base.Awake();
        SceneType = Define.Scene.GameRoomScene;
    }

    protected override void Start()
    {
        base.Start();

        _ui = Managers.UI.ShowSceneUI<UI_GameRoomScene>();
        _roomInfo = Managers.Game.CurrentRoom;
    }

    private void SetInfo()
    {
        
    }

    public override void Clear()
    {
        
    }
}
