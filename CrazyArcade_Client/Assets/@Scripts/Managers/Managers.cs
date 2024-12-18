
using UnityEngine;

public class Managers : MonoBehaviour
{
	public static bool Initialized { get; set; }

	private static Managers s_instance; // 유일성이 보장된다
    public static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

	#region Contents

	private GameManager _game = new GameManager();
	private EventManager _event = new EventManager();
    public static GameManager Game { get { return Instance?._game; } }
    public static EventManager Event { get { return Instance?._event; } }
    #endregion

    #region Core

    private DataManager _data = new DataManager();
    private PoolManager _pool = new PoolManager();
    private ResourceManager _resource = new ResourceManager();
    private SceneManager _scene = new SceneManager();
    private SoundManager _sound = new SoundManager();
    private UIManager _ui = new UIManager();
    private NetworkManager _network = new NetworkManager();

    public static DataManager Data { get { return Instance?._data; } }
    public static PoolManager Pool { get { return Instance?._pool; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static SceneManager Scene { get { return Instance?._scene; } }
    public static SoundManager Sound { get { return Instance?._sound; } }
    public static UIManager UI { get { return Instance?._ui; } }
    public static NetworkManager Network { get { return Instance?._network; } }

    #endregion

    #region Language
    private static Define.Language _language = Define.Language.Korean;
    public static Define.Language Language
    {
        get { return _language; }
        set
        {
            _language = value;
        }
    }

    public static string GetText(string textId)
    {
        switch (_language)
        {
            case Define.Language.Korean:
                break;
            case Define.Language.English:
                break;
            case Define.Language.French:
                break;
            case Define.Language.SimplifiedChinese:
                break;
            case Define.Language.TraditionalChinese:
                break;
            case Define.Language.Japanese:
                break;
        }

        return textId;
    }
    
    public static string GetErrorMsg(Define.ErrorMessage msg)
    {
        switch (msg)
        {
            case Define.ErrorMessage.Level:
                return "TODO 감히 사용 할 수 없습니다.";
            case Define.ErrorMessage.Class:
                return "TODO 현재 클래스에서 사용할 수 없습니다.";
            case Define.ErrorMessage.InventoryFull:
                return "TODO 가방이 가득 찼습니다..";
            case Define.ErrorMessage.Etc:
                return "사용할 수 없습니다.";
        }

        return "";
    }

    #endregion

    public static void Init()
    {
        if (s_instance == null && Initialized == false)
        {
            Initialized = true;

			GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
            s_instance._sound.Init();
        }		
	}
    private void Update()
    {
        _network.Update();
    }

    public static void Clear()
    {
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }

    private void OnApplicationQuit()
    {
        _network.DisConnect();
    }
}
