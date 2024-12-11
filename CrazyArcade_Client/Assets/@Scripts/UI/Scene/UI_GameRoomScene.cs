using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameRoomScene : UI_Scene
{
    #region enum
    enum Buttons
    {
        SelectMapButton,
        GameStartButton,
        RoomInfoChangeButton,
        ExitButton,
        DaoSelect,
        CappiSelect,
        BazziSelect,
        MaridSelect
    }

    enum Texts
    {
        RoomName,
        RoomNumber
    }

    enum Images
    {
        MapImage
    }
    #endregion

    private UI_UserToken[] _userUI;
    private int _playerCount = 0;
    
    protected override void Awake()
    {
        base.Awake();
        BindButtons(typeof(Buttons));
        BindTexts(typeof(Texts));
        BindImages(typeof(Images));

        _userUI = GetComponentsInChildren<UI_UserToken>();
        foreach (UI_UserToken token in _userUI)
            token.SetActive(false);
    }

    public void UpdateUI(GameRoom roomInfo)
    {

    }


    public void AddPlayer(Player player)
    {
        _playerCount += 1;
        UpdatePlayer(player);
    }

    public void UpdatePlayer(Player player)
    {
        _userUI[_playerCount].SetPlayer(player);
    }

    public void OnClickExit()
    {
        Managers.Game.LeaveCurrentRoom();
    }
}
