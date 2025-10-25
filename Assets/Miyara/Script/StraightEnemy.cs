using UnityEngine;

public class StraightEnemy : MonoBehaviour
{
    [Header("移動")]
    [SerializeField] private Vector3 moveDirection = Vector3.forward;    // 移動方向
    [SerializeField] public float moveSpeed = 2.0f;                     // 移動速度

    void Update()
    {
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("プレイヤーに接触！");
            // ここにダメージ処理やイベントを追加
        }
    }

}
