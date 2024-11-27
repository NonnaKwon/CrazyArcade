using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    public List<GameRoom> GameRooms { set { _gameRooms = value; UpdateRoomList(); } }
    private List<GameRoom> _gameRooms;
    private UI_LobbyScene _ui;

    protected override void Awake()
    {
        base.Awake();
        SceneType = Define.Scene.LobbyScene;
    }

    protected override void Start()
    {
        base.Start();

        if (!TestMode)
            _ui = Managers.UI.ShowSceneUI<UI_LobbyScene>();
        else
        {
            _ui = FindObjectOfType<UI_LobbyScene>();
            Managers.UI.SceneUI = _ui;
        }

        // 로비에 입장했다는 패킷을 보낸다.
        C_EnterLobby enterPacket = new C_EnterLobby();
        Managers.Network.Send(enterPacket);
    }


    public void UpdateRoomList()
    {
        if (_ui == null)
        {
            Debug.Log("UpdateRoomList() : not found UI");
            return;
        }
        _ui.SetRoomList(_gameRooms);
    }

    public override void Clear()
    {

    }
}
