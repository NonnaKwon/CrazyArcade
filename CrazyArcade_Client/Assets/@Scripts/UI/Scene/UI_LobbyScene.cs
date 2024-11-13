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


    protected override void Awake()
    {
        base.Awake();
        BindButtons(typeof(Buttons));
        BindObjects(typeof(GameObjects));

        _uiList = GetObject((int)GameObjects.RoomList).GetComponentsInChildren<UI_RoomToken>();
        GetButton((int)Buttons.CreateRoomButton).onClick.AddListener(OnCreateRoom);
    }

    protected override void Start()
    {
        base.Start();
    }

    private void SetRoomList()
    {

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
