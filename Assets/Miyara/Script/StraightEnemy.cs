using UnityEngine;

public class StraightEnemy : MonoBehaviour
{
    [Header("�ړ�")]
    [SerializeField] private Vector3 moveDirection = Vector3.forward;    // �ړ�����
    [SerializeField] public float moveSpeed = 2.0f;                     // �ړ����x

    void Update()
    {
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�v���C���[�ɐڐG�I");
            // �����Ƀ_���[�W������C�x���g��ǉ�
        }
    }

}
