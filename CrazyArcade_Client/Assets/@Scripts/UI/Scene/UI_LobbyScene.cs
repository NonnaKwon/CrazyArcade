using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_LobbyScene : UI_Scene
{
    enum Buttons
    {
        CreateRoomButton,
        QuitButton,
        QuickPlayButton
    }

    enum GameObjects
    {
        RoomList
    }

    private UI_RoomToken[] _uiList;
    private const int MAX_TOKEN = 6; // 한 페이지에 방 수

    protected override void Awake()
    {
        base.Awake();
        BindButtons(typeof(Buttons));
        BindObjects(typeof(GameObjects));

        _uiList = GetObject((int)GameObjects.RoomList).GetComponentsInChildren<UI_RoomToken>();
        for (int i = 0; i < _uiList.Length; i++)
            _uiList[i].OffActive();

        GetButton((int)Buttons.CreateRoomButton).onClick.AddListener(OnCreateRoom);
    }

    protected override void Start()
    {
        base.Start();
    }

    public void SetRoomList(List<GameRoom> list, int page = 0)
    {
        for (int i= 0; i < list.Count; i++)
        {
            if (i >= MAX_TOKEN)
                break;

            Debug.Log($"{_uiList.Length} / {list.Count} / {i}");
            _uiList[i].SetInfo(list[i]);
        }

        //TODO : 페이징 기능
    }

    public void OnCreateRoom()
    {
        Managers.UI.ShowPopupUI<UI_CreateRoom>();
    }

    public void OnClickQuit()
    {
        //Managers.Game.
    }
}
