using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : SingletonMonoBehaviour<GameSystem> {

    // プレイヤー名
    public string PlayerName { set; get; }

    // プレイヤーのオブジェクト
    public GameObject PlayerObject {set; get;}

    // プレイヤーにとってのターゲット
    public GameObject PlayerTarget { set; get; }

    // エネミーにとってのターゲット
    public GameObject EnemyTarget { set; get; }

    public GameObject MainAim { set; get; }

    public Vector3 defaultMainAimPos;

    private Dictionary<string, int> charactorNumber = new Dictionary<string, int>();

    // プレイヤーGameObjectのプレファブ
    public GameObject[] PlayerPrefabs;
    
    protected override void Awake()
    {
        // 継承前のAwakeを実行(Singletonでのチェック関数のみ実行)
        base.Awake();
        // オブジェクトの走査
        PlayerTarget = null;

        // シーン読み込み時に実行
        SceneManager.sceneLoaded += OnSceneLoaded;
        // このオブジェクトはシーン移動でオブジェクト破棄しない
        DontDestroyOnLoad(gameObject);

        // ディクショナリ追加
        charactorNumber.Add("SDunitychan", 0);
        charactorNumber.Add("SDmisakichan", 1);

    }

    private void Update()
    {
        TargetSearch();
    }

    private void TargetSearch()
    {
        // ロックオンボタンが押されたとき
        if (Input.GetButtonDown("Lock"))
        {
            // ターゲットがいる(ロックオン中)なら、ロックを外す
            if (PlayerTarget != null)
            {
                PlayerTarget = null;
                MainAim.transform.rotation = MainAim.transform.parent.transform.rotation;
                MainAim.transform.position = MainAim.transform.parent.transform.position + MainAim.transform.parent.transform.TransformDirection(defaultMainAimPos);

            }
            // ターゲットが不在(ロックオン中でない)なら、ロックオン対象のターゲットを探しターゲットにする
            else
            {
                GameObject[] PlayerTargets = GameObject.FindGameObjectsWithTag("Enemy");
                float closestDistance = Mathf.Infinity;
                foreach (GameObject closest in PlayerTargets)
                {
                    float distance = (closest.transform.position - this.transform.position).sqrMagnitude;
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        PlayerTarget = closest;
                    }
                }
                PlayerTarget = PlayerTarget.transform.Find("lockTarget").gameObject;

            }
        }
    }

    // シーン読み込み時に実行
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // MainSceneが読み込まれたとき
        if (scene.name == "MainScene")
            MainSceneLoaded();
    }

    private void MainSceneLoaded()
    {
        // 戦闘用オブジェクトの読み込み
        MainAim = GameObject.FindGameObjectWithTag("MainAim").gameObject;
        defaultMainAimPos = MainAim.transform.localPosition;

        // Playerオブジェクトの生成
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            GameObject playerInstance = Instantiate(PlayerPrefabs[charactorNumber[PlayerName]], PlayerPrefabs[charactorNumber[PlayerName]].transform.position, PlayerPrefabs[charactorNumber[PlayerName]].transform.rotation);
            playerInstance.transform.parent = GameObject.Find("World").transform;
            PlayerObject = playerInstance;
        }
    }
}
