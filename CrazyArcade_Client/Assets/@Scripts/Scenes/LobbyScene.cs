using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    public List<GameRoom> GameRooms { set { _gameRooms = value; UpdateRoomList(); } }
    private List<GameRoom> _gameRooms = new List<GameRoom>();
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

        if (Managers.Game.CurrentRoom != null)
            Managers.Game.CurrentRoom = null;
    }

    public void AddRoom(GameRoom gameRoom)
    {
        _gameRooms.Add(gameRoom);
        UpdateRoomList();
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

    public GameRoom FindRoomById(int roomId)
    {
        GameRoom targetRoom = null;
        for(int i=0;i< _gameRooms.Count;i++)
        {
            if (_gameRooms[i].IsEqualId(roomId))
            {
                targetRoom = _gameRooms[i];
                break;
            }
        }

        if(targetRoom == null)
        {
            // TODO : 방을 찾을 수 없다는 경고문구 띄움.
            Debug.Log("FindRoomById() : 찾을 수 없음");
            return null;
        }

        return targetRoom;
    }

    public override void Clear()
    {

    }
}
