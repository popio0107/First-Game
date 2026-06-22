using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    [Header("UI Windows")]
    public GameObject shopUIPanel;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI dialogText;
    public Transform itemListParent; // Contentオブジェクト

    [Header("Prefabs")]
    public GameObject itemTextPrefab; // 商品名を表示するためのTextプレハブ
    public RectTransform cursor;      // 「?」のカーソル画像

    private List<ItemData> currentShopItems;
    private int selectedIndex = 0;
    private PlayerInventory playerInventory;
    private bool isShopOpen = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // シーン内からプレイヤーのインベントリを探しておく
        playerInventory = Object.FindFirstObjectByType<PlayerInventory>();
        shopUIPanel.SetActive(false); // 最初は隠しておく
    }

    // 道具屋を開く
    public void OpenShop(List<ItemData> itemsToSell)
    {
        currentShopItems = itemsToSell;
        isShopOpen = true;
        shopUIPanel.SetActive(true);
        selectedIndex = 0;

        dialogText.text = "なにか おもとめですか？";
        UpdateGoldUI();
        GenerateItemList();
        UpdateCursorPosition();
    }

    // 所持金表示の更新
    void UpdateGoldUI()
    {
        goldText.text = playerInventory.gold.ToString() + " Ｇ";
    }

    // 売り物リストをUIに生成
    void GenerateItemList()
    {
        // 古いリスト表示を削除
        foreach (Transform child in itemListParent) { Destroy(child.gameObject); }

        // 商品を並べる
        foreach (var item in currentShopItems)
        {
            GameObject obj = Instantiate(itemTextPrefab, itemListParent);
            // 例：「やくそう              8G」のような文字列を作る
            obj.GetComponent<TextMeshProUGUI>().text = string.Format("{0,-10} {1,4}G", item.itemName, item.price);
        }
    }

    private void Update()
    {
        // 🔴 古い Input.GetKeyDown を使っている中身をすべて消すか、
        // 以下のように1行だけにして処理をスキップさせます。
        if (!isShopOpen) return;

        // ※ ここにあった Input.GetKeyDown(KeyCode.Space) などの処理は
        // 一旦すべて消去するか、コメントアウトしてください。
    }

    // カーソルの位置を移動させる
    void UpdateCursorPosition()
    {
        if (itemListParent.childCount == 0) return;

        // 選択中のテキスト項目の位置に矢印を合わせる
        Transform selectedItem = itemListParent.GetChild(selectedIndex);
        Vector3 newPos = cursor.position;
        newPos.y = selectedItem.position.y; // Y軸を合わせる
        cursor.position = newPos;
    }

    // 購入ロジック
    void BuyItem(ItemData item)
    {
        // 1. お金が足りるかチェックして支払う
        if (playerInventory.SpendGold(item.price))
        {
            // 2. アイテムをカバンに追加
            playerInventory.AddItem(item);
            dialogText.text = item.itemName + " を かいました。\nおおきに！";
            UpdateGoldUI();
        }
        else
        {
            // お金が足りない場合
            dialogText.text = "ゴールドが たりないようですぜ。";
        }
    }

    void CloseShop()
    {
        isShopOpen = false;
        shopUIPanel.SetActive(false);
    }
}