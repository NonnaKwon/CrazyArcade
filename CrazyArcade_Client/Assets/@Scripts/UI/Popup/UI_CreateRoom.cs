using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CreateRoom : UI_Popup
{
    enum Buttons
    {
        CreateButton,
        CloseButton
    }

    enum GameObjects
    {
        RoomNameInput,
        MaxPlayerInput
    }

    protected override void Awake()
    {
        base.Awake();
        BindButtons(typeof(Buttons));
        BindObjects(typeof(GameObjects));

        GetButton((int)Buttons.CreateButton).onClick.AddListener(ConfirmCreateRoom);
        GetButton((int)Buttons.CloseButton).onClick.AddListener(() => { ClosePopupUI(); });
        SetOptions();
    }

    protected override void Start()
    {
        base.Start();
    }

    public void ConfirmCreateRoom()
    {
        //TODO : 방을 만들었다는 패킷을 보낸다.

        //TODO : 답변이 오면 (S_CreateRoom) 방을 만들고 방에 입장한다. (씬 이동)
        //TODO : 로비에 들어가 있는 아이들한테만 방이 만들어졌다는 패킷을 쏴야한다.
    }

    private void SetOptions()
    {
        GameObject go = GetObject((int)GameObjects.MaxPlayerInput);

        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        for(int i=1;i<=Define.MAX_PLAYER;i++)
        {
            TMP_Dropdown.OptionData od = new TMP_Dropdown.OptionData($"{i}");
            options.Add(od);
        }

        go.GetComponent<TMP_Dropdown>().AddOptions(options);
    }
}
