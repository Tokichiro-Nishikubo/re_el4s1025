using UnityEngine;

public class StraightEnemy : MonoBehaviour
{
    [Header("移動")]
    [SerializeField] private Vector3 moveDirection = Vector3.forward; // 移動方向
    [SerializeField] public float moveSpeed = 2.0f;                   // 移動速度

    [Header("回転")]
    [SerializeField] private bool enableRotation = false;            // 回転するかどうか
    [SerializeField] private float rotationSpeed = 90f;              // 回転速度（度/秒）

    void Update()
    {
        if (!GameManager.Instance.IsGame) return;

        // 移動処理
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;

        // 回転処理（有効な場合のみ）
        if (enableRotation)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("プレイヤーに接触！");

            GameManager.Instance.PlayerController.GameOverStart();
        }
    }
}
