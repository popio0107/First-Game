using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int gold = 100; // 初期所持金
    public List<ItemData> items = new List<ItemData>(); // 所持アイテムリスト

    // お金を支払う処理
    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            return true; // 支払い成功
        }
        return false; // お金が足りない
    }

    // アイテムを追加する処理
    public void AddItem(ItemData item)
    {
        items.Add(item);
        Debug.Log(item.itemName + "を手に入れた！");
    }
}