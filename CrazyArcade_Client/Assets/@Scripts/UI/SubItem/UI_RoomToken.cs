using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static S_RoomList;

public class UI_RoomToken : UI_Base
{
    enum Texts
    {
        RoomNumber,
        RoomName,
        MaxNum,
        NowNum,
    }

    enum Images
    {
        MapImage,
    }

    enum Buttons
    {
        EnterButton
    }

    private GameRoom _roomInfo;

    protected override void Awake()
    {
        base.Awake();
        BindTexts(typeof(Texts));
        BindImages(typeof(Images));
        BindButtons(typeof(Buttons));

        GetButton((int)Buttons.EnterButton);
    }

    public void EnterRoom()
    {
        _roomInfo.TryEnterRoom();
    }

    public void SetInfo(GameRoom room)
    {
        _roomInfo = room;
        OnActive();
    }

    private void OnActive()
    {
        gameObject.SetActive(true);
    }

    public void OffActive()
    {
        gameObject.SetActive(false);
    }

}
