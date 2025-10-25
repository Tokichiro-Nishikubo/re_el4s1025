using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    [Header("�ړ����x�ݒ�")]
    public float moveSpeed = 5f;          // �ʏ�̈ړ����x
    public float boostMultiplier = 3f;    // Shift�ŉ�������{��

    [Header("��]���x�ݒ�")]
    public float lookSensitivity = 2f;    // �}�E�X���x

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        // �}�E�X�J�[�\�������b�N���Ĕ�\��
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // ���݂̉�]��������
        Vector3 rot = transform.localRotation.eulerAngles;
        rotationY = rot.y;
        rotationX = rot.x;
    }

    void Update()
    {
        // --- �J������] ---
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // �㉺����

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);

        // --- �J�����ړ� ---
        float moveForward = Input.GetAxis("Vertical");   // W/S
        float moveRight = Input.GetAxis("Horizontal");   // A/D
        float moveUp = 0f;

        if (Input.GetKey(KeyCode.E)) moveUp += 1f;       // �㏸
        if (Input.GetKey(KeyCode.Q)) moveUp -= 1f;       // ���~

        Vector3 move = new Vector3(moveRight, moveUp, moveForward).normalized;
        float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? boostMultiplier : 1f);

        transform.Translate(move * speed * Time.deltaTime, Space.Self);

        // --- ESC�L�[�Ń}�E�X��� ---
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
