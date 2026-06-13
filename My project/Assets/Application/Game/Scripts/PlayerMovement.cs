using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask obstacleLayer;

    private bool isMoving = false;
    private Vector2 input;

    // Input Systemから入力を受け取るメソッド
    public void OnMove(InputAction.CallbackContext context)
    {
        // ボタンが「押された瞬間」だけ処理を通す（押しっぱなしで何度もブレるのを防ぐ）
        if (!context.performed) return;

        if (isMoving) return;

        // 値の読み取り方も context.ReadValue<Vector2>() になります
        Vector2 inputMovement = context.ReadValue<Vector2>();

        // 斜め入力を防ぐ
        if (Mathf.Abs(inputMovement.x) > Mathf.Abs(inputMovement.y))
        {
            input = new Vector2(Mathf.Sign(inputMovement.x), 0);
        }
        else if (Mathf.Abs(inputMovement.y) > Mathf.Abs(inputMovement.x))
        {
            input = new Vector2(0, Mathf.Sign(inputMovement.y));
        }
        else
        {
            input = Vector2.zero;
        }
    }

    void Update()
    {
        // Update内でのキー入力取得は不要になりました！
        // 入力があり、移動中でなければ移動を開始する
        if (!isMoving && input != Vector2.zero)
        {
            Vector3 targetPos = transform.position + new Vector3(input.x, input.y, 0);

            if (IsWalkable(targetPos))
            {
                StartCoroutine(MoveRoutine(targetPos));
            }
            else
            {
                // 壁にぶつかって移動できない場合、入力をリセットして次の入力を待つ
                input = Vector2.zero;
            }
        }
    }

    IEnumerator MoveRoutine(Vector3 targetPos)
    {
        isMoving = true;

        //while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        while ((targetPos - transform.position).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        // 1マスの移動が終わったら入力をリセット（押しっぱなしでも次のマスへの移動を検知させるため）
        input = Vector2.zero;

        CheckForEncounters();
    }

    bool IsWalkable(Vector3 targetPos)
    {
        return !Physics2D.OverlapCircle(targetPos, 0.2f, obstacleLayer);
    }

    void CheckForEncounters()
    {
        if (Random.Range(0, 100) < 10)
        {
            Debug.Log("あらくれスライム が あらわれた！");
        }
    }
}