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
        GetText((int)Texts.RoomNumber).text = $"{room.Id:000}";
        GetText((int)Texts.RoomName).text = $"{room.RoomName}";
        GetText((int)Texts.MaxNum).text = $"{room.MaxPlayer}";
        GetText((int)Texts.NowNum).text = $"{room.PlayerCount}";

        //TODO : 맵 스프라이트 설정해야함 : Title 완성되면.
        //GetImage((int)Images.MapImage).sprite = Managers.Resource.Load<Sprite>(room.Map.ToString());
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
