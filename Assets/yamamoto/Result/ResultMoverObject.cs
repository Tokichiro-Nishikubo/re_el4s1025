using UnityEngine;
using System.Collections;

public class ResultMoverObject : MonoBehaviour, IResultObject
{
    [SerializeField] private Vector3 pos;   // 移動先（相対位置）
    [SerializeField] private float duration = 1.0f; // 移動時間（秒）
    private Vector3 targetpos;
    private Coroutine moveCoroutine;

    public void Start()
    {
        targetpos = transform.position+pos;
    }
    public void ResultSet()
    {
        // 現在位置 + pos にイージングで移動
        MoveTo(targetpos);
    }

    public void ResultEnd()
    {
        // 現在位置 - pos にイージングで移動
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

            // イージング（EaseOutCubic）
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
        // 現在位置
        Vector3 current = transform.position;
        // 目標位置（ResultSet 時）
        Vector3 target = current + pos;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(current, target);     // 現在位置→目標位置の線

        // 方向矢印を少し描く（オプション）
        Vector3 dir = (target - current).normalized * 0.3f;
        Gizmos.DrawRay(target - dir, dir);
    }
#endif
}

