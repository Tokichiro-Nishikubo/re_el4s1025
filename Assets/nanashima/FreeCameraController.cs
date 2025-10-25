using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    [Header("移動速度設定")]
    public float moveSpeed = 5f;          // 通常の移動速度
    public float boostMultiplier = 3f;    // Shiftで加速する倍率

    [Header("回転速度設定")]
    public float lookSensitivity = 2f;    // マウス感度

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        // マウスカーソルをロックして非表示
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 現在の回転を初期化
        Vector3 rot = transform.localRotation.eulerAngles;
        rotationY = rot.y;
        rotationX = rot.x;
    }

    void Update()
    {
        // --- カメラ回転 ---
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // 上下制限

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);

        // --- カメラ移動 ---
        float moveForward = Input.GetAxis("Vertical");   // W/S
        float moveRight = Input.GetAxis("Horizontal");   // A/D
        float moveUp = 0f;

        if (Input.GetKey(KeyCode.E)) moveUp += 1f;       // 上昇
        if (Input.GetKey(KeyCode.Q)) moveUp -= 1f;       // 下降

        Vector3 move = new Vector3(moveRight, moveUp, moveForward).normalized;
        float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? boostMultiplier : 1f);

        transform.Translate(move * speed * Time.deltaTime, Space.Self);

        // --- ESCキーでマウス解放 ---
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
