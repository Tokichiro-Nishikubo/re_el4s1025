using UnityEngine;
using System.Collections;

public class ResultMoverObject : MonoBehaviour, IResultObject
{
    [SerializeField] private Vector3 pos;   // �ړ���i���Έʒu�j
    [SerializeField] private float duration = 1.0f; // �ړ����ԁi�b�j
    private Vector3 targetpos;
    private Coroutine moveCoroutine;

    public void Start()
    {
        targetpos = transform.position+pos;
    }
    public void ResultSet()
    {
        // ���݈ʒu + pos �ɃC�[�W���O�ňړ�
        MoveTo(targetpos);
    }

    public void ResultEnd()
    {
        // ���݈ʒu - pos �ɃC�[�W���O�ňړ�
        MoveTo(targetpos - pos);
    }

    private void MoveTo(Vector3 targetPos)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveCoroutine(targetPos));
    }

    private IEnumerator MoveCoroutine(Vector3 targetPos)
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // �C�[�W���O�iEaseOutCubic�j
            t = 1f - Mathf.Pow(1f - t, 3f);

            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        transform.position = targetPos;
        moveCoroutine = null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // ���݈ʒu
        Vector3 current = transform.position;
        // �ڕW�ʒu�iResultSet ���j
        Vector3 target = current + pos;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(current, target);     // ���݈ʒu���ڕW�ʒu�̐�

        // �������������`���i�I�v�V�����j
        Vector3 dir = (target - current).normalized * 0.3f;
        Gizmos.DrawRay(target - dir, dir);
    }
#endif
}

