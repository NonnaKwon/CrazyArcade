using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class UI_UserToken : UI_Base
{
    enum Texts
    {
        UserNickname
    }

    enum Images
    {
        PlayerImg
    }

    enum GameObjects
    {
        ReadyInfo,
        BackgroundOK
    }

    protected override void Awake()
    {
        base.Awake();
        BindTexts(typeof(Texts));
        BindImages(typeof(Images));
        BindObjects(typeof(GameObjects));
    }

    public void SetActive(bool isActive)
    {
        GetObject((int)GameObjects.ReadyInfo).SetActive(isActive);
        GetImage((int)Images.PlayerImg).gameObject.SetActive(isActive);
        GetText((int)Texts.UserNickname).text = "";
    }

    public void SetBlock(bool isBlock)
    {
        GetObject((int)GameObjects.BackgroundOK).SetActive(!isBlock);
        SetActive(!isBlock);
    }

    public void SetPlayer(Player player)
    {
        SetActive(true);
        GetObject((int)GameObjects.ReadyInfo).SetActive(player.IsReady);
        GetImage((int)Images.PlayerImg).sprite = Managers.Resource.Load<Sprite>(player.Character.ToString());
        GetText((int)Texts.UserNickname).text = player.Nickname;
    }
}
