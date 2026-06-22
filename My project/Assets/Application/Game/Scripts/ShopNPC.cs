using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    // このお店で売りたいアイテムをインスペクターから登録する
    public List<ItemData> shopInventory = new List<ItemData>();

    // プレイヤーが話しかけてきたときに実行する関数
    public void TalkToShopKeeper()
    {
        // 後述するショップマネージャーを呼び出し、売り物リストを渡して画面を開く
        ShopManager.instance.OpenShop(shopInventory);
    }
}