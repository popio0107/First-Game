using UnityEngine;

public class EnemySymbol : MonoBehaviour
{
    // インスペクターから、この敵の固有の名前やID（"Goblin_A" など）を付けておく
    [SerializeField] private string enemyID;

    void Start()
    {
        // 固有IDが未設定なら、オブジェクト名＋座標などで自動割り当て
        if (string.IsNullOrEmpty(enemyID))
        {
            enemyID = gameObject.name + "_" + transform.position.ToString();
        }

        // 後述：バトルから戻ってきたとき、自分がすでに倒された敵なら消滅させる
        if (EncounterManager.instance != null && EncounterManager.instance.isReturningFromBattle)
        {
            if (EncounterManager.instance.engagedEnemyID == enemyID)
            {
                Destroy(gameObject); // 自分は倒されたので消える
            }
        }
    }

    // プレイヤーと接触した時の判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーの位置を保存しつつ、バトル開始
            EncounterManager.instance.StartBattle(enemyID, collision.transform.position);
        }
    }
}