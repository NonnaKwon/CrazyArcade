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
        TMP_Dropdown dropdown = GetObject((int)GameObjects.MaxPlayerInput).GetComponent<TMP_Dropdown>();
        string roomName = GetObject((int)GameObjects.RoomNameInput).GetComponent<TMP_InputField>().text;
        int maxPlayer = int.Parse(dropdown.options[dropdown.value].text);

        //���� ������ٴ� ��Ŷ�� ������.
        C_CreateRoom packet = new C_CreateRoom();
        packet.roomName = roomName;
        packet.maxPlayer = maxPlayer;

        Managers.Network.Send(packet);

        //TODO : �� �������,, �̶�� �˾��� ���.

        //TODO : �������� ���� ��������ٴ� ��ȣ�� ����, �濡 ����.
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
