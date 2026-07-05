using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("売るアイテムの設定")]
    public ItemData itemToSell;

    [Header("UIの接続")]
    public TextMeshProUGUI goldText;
    public Button buyButton;

    void Start()
    {
        UpdateUI();
    }

    // 「購入ボタン」が押された時の処理
    public void OnBuyButton()
    {
        if (itemToSell == null) return;

        // GameManager（財布）にお金を払えるか確認して消費させる
        if (GameManager.instance != null)
        {
            if (GameManager.instance.SpendGold(itemToSell.price))
            {
                //  購入成功！
                Debug.Log(itemToSell.itemName + " を購入しました！");

                // ここで「プレイヤーのインベントリにアイテムを追加する」処理を今後足せます

                UpdateUI();
            }
            else
            {
                //  ゴールド不足
                Debug.Log("お金が足りなくて買えなかった！");
            }
        }
    }

    // 「店を出るボタン」が押された時の処理
    public void OnExitButton()
    {
        Debug.Log("マップに戻ります。");
        SceneManager.LoadScene("Game Scene"); // 元のマップに戻す
    }

    // 所持金表示を最新にする
    void UpdateUI()
    {
        if (GameManager.instance != null)
        {
            goldText.text = "所持金: " + GameManager.instance.currentGold + " G";
        }
        else
        {
            goldText.text = "所持金: ---- G (GameManager未配置)";
        }
    }
}