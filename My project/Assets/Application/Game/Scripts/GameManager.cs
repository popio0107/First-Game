using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // プレイヤーの所持金
    public int currentGold = 200; // 最初は200G持っていることにする

    void Awake()
    {
        // シーンを切り替えてもこのオブジェクト（財布データ）を破壊しない
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // お金を増やす（戦闘で勝利した時などに呼ぶ）
    public void AddGold(int amount)
    {
        currentGold += amount;
        Debug.Log(amount + "G 獲得！ 所持金: " + currentGold + "G");
    }

    // お金を消費する（購入に成功したらtrueを返す）
    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            Debug.Log(amount + "G 消費。 残り所持金: " + currentGold + "G");
            return true;
        }
        else
        {
            Debug.Log("ゴールドが足りません！");
            return false;
        }
    }
}