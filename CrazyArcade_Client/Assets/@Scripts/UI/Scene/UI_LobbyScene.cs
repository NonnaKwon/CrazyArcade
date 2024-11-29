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
    private List<GameRoom> _gameRooms;
    private const int MAX_TOKEN = 6; // 한 페이지에 방 수

    protected override void Awake()
    {
        base.Awake();
        BindButtons(typeof(Buttons));
        BindObjects(typeof(GameObjects));

        GetButton((int)Buttons.CreateRoomButton).onClick.AddListener(OnCreateRoom);
        GetButton((int)Buttons.QuitButton).onClick.AddListener(OnClickQuit);
        GetButton((int)Buttons.QuickPlayButton).onClick.AddListener(OnClickQuickPlay);
    }

    protected override void Start()
    {
        base.Start();

        _uiList = GetObject((int)GameObjects.RoomList).GetComponentsInChildren<UI_RoomToken>();
        for (int i = 0; i < _uiList.Length; i++)
            _uiList[i].OffActive();
    }

    public void SetRoomList(List<GameRoom> list, int page = 0)
    {
        _gameRooms = list;
        for (int i= 0; i < list.Count; i++)
        {
            if (i >= MAX_TOKEN)
                break;

            _uiList[i].SetInfo(list[i]);
        }

        //TODO : 페이징 기능
    }
    public void OnClickQuickPlay()
    {
        if (_gameRooms.Count == 0)
            return;

        int tryCount = 0;
        while(true)
        {
            int rand = Random.Range(0, _gameRooms.Count);
            if (_gameRooms[rand].TryEnterRoom())
                break;

            tryCount++;
            if (tryCount >= 100)
            {
                Debug.LogError("OnClickQuickPlay() : 들어갈 수 있는 방이 없음.");
                break;
            }
        }
    }

    public void OnCreateRoom()
    {
        Managers.UI.ShowPopupUI<UI_CreateRoom>();
    }
    
    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}
