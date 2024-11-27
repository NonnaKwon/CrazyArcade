using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    enum GameObjects
    {
        EnterButton
    }

    private GameRoom _roomInfo;

    protected override void Awake()
    {
        base.Awake();
        BindTexts(typeof(Texts));
        BindImages(typeof(Images));
        BindObjects(typeof(GameObjects));
        OffActive();
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

    private void OffActive()
    {
        gameObject.SetActive(false);
    }

}
