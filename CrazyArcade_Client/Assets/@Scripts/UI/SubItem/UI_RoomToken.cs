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

    protected override void Awake()
    {
        base.Awake();
        BindTexts(typeof(Texts));
        BindImages(typeof(Images));
        BindObjects(typeof(GameObjects));
        gameObject.SetActive(false);
    }

    public void OnActive()
    {

    }

    public void OffActive()
    {

    }

}
