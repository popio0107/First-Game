using UnityEngine;
using System.Collections;

public class EnemyWalk : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;     // 移動速度
    [SerializeField] private float walkTime = 1f;      // 1回に歩く時間
    [SerializeField] private float waitTime = 2f;      // 次に歩き出すまでの待ち時間

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(WanderRoutine());
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // 完全に停止させる (※Unity古いバージョンの場合は velocity)
        }
    }

    // ランダムに歩き回るコルーチン
    IEnumerator WanderRoutine()
    {
        while (true)
        {
            // 1. 立ち止まって待つ
            isMoving = false;
            yield return new WaitForSeconds(Random.Range(waitTime * 0.5f, waitTime * 1.5f));

            // 2. ランダムな方向（上下左右＋斜め）を決める
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

            // 3. 一定時間歩く
            isMoving = true;
            yield return new WaitForSeconds(walkTime);
        }
    }
}