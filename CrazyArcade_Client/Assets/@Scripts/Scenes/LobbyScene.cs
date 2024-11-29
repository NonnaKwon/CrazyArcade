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

        // �κ� �����ߴٴ� ��Ŷ�� ������.
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
            // TODO : ���� ã�� �� ���ٴ� ����� ���.
            Debug.Log("FindRoomById() : ã�� �� ����");
            return null;
        }

        return targetRoom;
    }

    public override void Clear()
    {

    }
}
