using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class BattleManager : MonoBehaviour
{
    // ターンの状態を管理する
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
    public BattleState state;

    [Header("キャラクターの設定")]
    public BattleCharacter player;
    public BattleCharacter enemy;

    [Header("UIの接続")]
    public TextMeshProUGUI playerHpText;
    public TextMeshProUGUI enemyHpText;
    public Button attackButton;
    public Button fleeButton;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    // 1. 戦闘開始の準備
    IEnumerator SetupBattle()
    {
        //  修正：player.characterName = "プレイヤー"; などの上書き処理を削除しました。
        // （ScriptableObject の設定名がそのまま自動で使われます！）

        UpdateUI();

        yield return new WaitForSeconds(1f); // 1秒待ってから

        // プレイヤーのターンへ
        PlayerTurn();
    }

    // 2. プレイヤーのターン開始
    void PlayerTurn()
    {
        state = BattleState.PLAYERTURN;

        //  修正：Debug.Action になっていた部分を Debug.Log に直しました！
        Debug.Log("プレイヤーのターン！ コマンドを選んでください。");

        // ボタンを押せるようにする
        attackButton.interactable = true;
        fleeButton.interactable = true;
    }

    // 「たたかう」ボタンが押された時の処理
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        // 連打防止のためにボタンを無効化
        attackButton.interactable = false;
        fleeButton.interactable = false;

        StartCoroutine(PlayerAttackRoutine());
    }

    IEnumerator PlayerAttackRoutine()
    {
        // 敵にダメージを与える
        bool isDead = enemy.TakeDamage(player.attackPower);
        UpdateUI();

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            // 敵が生きていれば敵のターンへ
            StartCoroutine(EnemyTurnRoutine());
        }
    }

    // 3. 敵のターン処理
    IEnumerator EnemyTurnRoutine()
    {
        state = BattleState.ENEMYTURN;
        Debug.Log("敵のターン！");

        yield return new WaitForSeconds(1f);

        // プレイヤーにダメージを与える
        bool isDead = player.TakeDamage(enemy.attackPower);
        UpdateUI();

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            // プレイヤーが生きていればプレイヤーのターンに戻る
            PlayerTurn();
        }
    }

    // 4. 勝敗が決まった時の処理
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            Debug.Log("戦闘に勝利した！");
            // マップに戻る（以前実装した処理）
            SceneManager.LoadScene("Game Scene");
        }
        else if (state == BattleState.LOST)
        {
            Debug.Log("敗北しました...ゲームオーバー画面へ");
            // 必要に応じてタイトルに戻るなど
        }
    }

    // 「逃げる」ボタンが押された時の処理
    public void OnFleeButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        Debug.Log("うまく逃げ切れた！");
        // 戦闘をなかったことにしてマップに戻る
        SceneManager.LoadScene("Game Scene");
    }

    // HP表示を最新にする関数
    void UpdateUI()
    {
        playerHpText.text = player.characterName + " HP: " + player.currentHp + " / " + player.maxHp;
        enemyHpText.text = enemy.characterName + " HP: " + enemy.currentHp + " / " + enemy.maxHp;
    }
}