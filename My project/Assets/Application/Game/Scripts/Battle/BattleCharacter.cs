using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    // インスペクターから ScriptableObject (PlayerDataなど) をハメ込む枠
    [SerializeField] private CharacterData characterData;

    // バトル中に変動する現在のステータス
    public string characterName { get; private set; }
    public int maxHp { get; private set; }
    public int currentHp { get; private set; }
    public int attackPower { get; private set; }

    void Awake()
    {
        // ゲーム開始時に ScriptableObject のマスターデータを自身の変数にコピーする
        if (characterData != null)
        {
            characterName = characterData.characterName;
            maxHp = characterData.maxHp;
            attackPower = characterData.attackPower;
            currentHp = maxHp; // 最初はHP満タン
        }
        else
        {
            Debug.LogError(gameObject.name + " に CharacterData がセットされていません！");
        }
    }

    // ダメージを受ける関数（死亡したらtrueを返す）
    public bool TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp < 0) currentHp = 0;

        Debug.Log(characterName + "は " + damage + " のダメージを受けた！ 残りHP: " + currentHp);

        return currentHp <= 0;
    }
}