using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    public List<GameRoom> GameRooms { set { _gameRooms = value; UpdateRoomListUI(); } }
    private List<GameRoom> _gameRooms;

    protected override void Awake()
    {
        base.Awake();
        SceneType = Define.EScene.LobbyScene;
    }

    protected override void Start()
    {
        base.Start();
        if(!TestMode)
            Managers.UI.ShowSceneUI<UI_LobbyScene>();

        // �κ� �����ߴٴ� ��Ŷ�� ������.
        C_EnterLobby enterPacket = new C_EnterLobby();
        Managers.Network.Send(enterPacket);
    }

    public void UpdateRoomListUI()
    {

    }

    public override void Clear()
    {

    }
}
