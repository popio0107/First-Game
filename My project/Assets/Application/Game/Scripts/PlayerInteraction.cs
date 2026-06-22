using UnityEngine;
using UnityEngine.InputSystem; // 🔴 これが最上部にあるか確認してください

public class PlayerInteraction : MonoBehaviour
{
    private Vector2 lookDirection = Vector2.down; // プレイヤーの向いている方向
    [SerializeField] private float rayDistance = 1.2f; // 調べる距離（1マス分強）
    [SerializeField] private LayerMask npcLayer;      // NPCのレイヤー

    //// 🔴 1. 移動入力を受け取る関数（既存のものを参考に、向きだけ更新する例）
    //public void OnMove(InputValue value)
    //{
    //    Vector2 moveInput = value.Get<Vector2>();

    //    // 入力があった時だけ、ドラクエ風の十字方向（上下左右）に向きを固定する
    //    if (moveInput.x != 0 || moveInput.y != 0)
    //    {
    //        if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
    //        {
    //            lookDirection = new Vector2(moveInput.x, 0).normalized;
    //        }
    //        else
    //        {
    //            lookDirection = new Vector2(0, moveInput.y).normalized;
    //        }
    //    }
    //}

    // 🔴 2. 今回追加した「Interact」アクションに対応する関数
    public void OnInteract(InputAction.CallbackContext context)

    {
        // ボタンが「押された瞬間」だけ処理を実行する
        if (context.performed)
        {
            Debug.Log("決定ボタンが押されました！正面を調べます。");
            TryInteractWithNPC();
        }
    }

    // 正面のNPCを調べるロジック
    void TryInteractWithNPC()
    {
        Vector2 origin = transform.position; // プレイヤーの中心位置

        // 正面に向かって見えないレーザーを飛ばす
        RaycastHit2D hit = Physics2D.Raycast(origin, lookDirection, rayDistance, npcLayer);

        // シーンビューに確認用の赤い線を表示（ゲーム画面には映りません）
        Debug.DrawRay(origin, lookDirection * rayDistance, Color.red, 0.5f);

        if (hit.collider != null)
        {
            // 当たった相手にShopNPCスクリプトがついていたら話しかける
            ShopNPC shopNPC = hit.collider.GetComponent<ShopNPC>();
            if (shopNPC != null)
            {
                shopNPC.TalkToShopKeeper();
            }
        }
    }
}