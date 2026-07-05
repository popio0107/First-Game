using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager instance;

    // 現在のマップでのプレイヤーの位置や、接触した敵のIDを記憶しておく変数
    public Vector3 lastPlayerPosition;
    public string engagedEnemyID;
    public bool isReturningFromBattle = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // シーンが切り替わってもこのオブジェクトを消さない
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // バトルシーンへ移行する関数
    public void StartBattle(string enemyID, Vector3 playerPos)
    {
        engagedEnemyID = enemyID;
        lastPlayerPosition = playerPos;
        isReturningFromBattle = true;

        Debug.Log("戦闘開始！ 敵ID: " + enemyID);

        // 「BattleScene」という名前の戦闘シーンに切り替える
        SceneManager.LoadScene("BattleScene");
    }
}