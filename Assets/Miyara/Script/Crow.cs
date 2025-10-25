using UnityEngine;

public class Crow : MonoBehaviour
{
    [System.Serializable]
    public struct LaneSpawnSetting
    {
        public int laneIndex;
        public bool mode; // true → 他レーンに2体スポーン、false → 指定レーンに1体
    }


    [Header("移動設定")]
    [SerializeField] public Vector3 moveDirection = Vector3.forward;
    [SerializeField] public float minSpeed = 1.0f;
    [SerializeField] public float maxSpeed = 5.0f;

    [Header("レーン設定")]
    [SerializeField] public Transform[] spawnLanes;
    [SerializeField] public LaneSpawnSetting[] laneSettings;

    [Header("エネミー設定")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("プレイヤーとの距離判定")]
    [SerializeField] PlayerController player;
    [SerializeField] float range = 5.0f;

    [Header("スポーン制御")]
    [SerializeField] private bool useRandomLane = true;
    [SerializeField] private bool useRandomMode = true;

    private int spawnCount = 0; // Spawn関数の呼び出し回数



    /// <summary>
    /// カラスのスポーン
    /// </summary>
    public void SpawnCrow(float offsetDistance)
    {
        if (spawnLanes.Length < 3)
        {
            Debug.LogWarning("spawnLanes が3つ未満です");
            return;
        }

        int laneIndex;
        bool mode = false;

        if (useRandomLane)
        {
            laneIndex = Random.Range(0, spawnLanes.Length);
        }
        else
        {
            if (laneSettings.Length == 0)
            {
                Debug.LogWarning("laneSettings が未設定です");
                return;
            }

            int index = spawnCount % laneSettings.Length;
            laneIndex = laneSettings[index].laneIndex;
            mode = useRandomMode ? Random.value < 0.5f : laneSettings[index].mode;
        }

        if (useRandomLane && useRandomMode)
        {
            mode = Random.value < 0.5f;
        }

        // プレイヤーとの距離チェック
        float distance = Vector3.Distance(player.transform.position, spawnLanes[laneIndex].position);
        if (distance < range)
        {
            Debug.Log($"レーン{laneIndex}はプレイヤーに近すぎるためスキップ");
            return;
        }

        if (mode)
        {
            SpawnCrowOtherLanes(offsetDistance, laneIndex);
        }
        else
        {
            SpawnCrowOneLane(offsetDistance, laneIndex);
        }

        spawnCount++;
    }

    /// <summary>
    /// 指定レーンに敵をスポーン
    /// </summary>
    private void SpawnCrowOneLane(float offsetDistance, int lane)
{
    if (lane < 0 || lane >= spawnLanes.Length)
    {
        Debug.LogWarning($"laneIndex の値 {lane} が spawnLanes の範囲外です");
        return;
    }

    Vector3 basePos = spawnLanes[lane].position;
    Vector3 spawnPos = basePos + moveDirection.normalized * offsetDistance;

    GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

    // 個別速度を設定（例：ランダム）
    float speed = Random.Range(minSpeed, maxSpeed);
    var mover = enemy.GetComponent<StraightEnemy>();
    if (mover != null)
    {
        mover.moveSpeed = speed;
    }
}

    /// <summary>
    /// 指定レーン以外の2レーンに敵をスポーン
    /// </summary>
    private void SpawnCrowOtherLanes(float offsetDistance, int excludedLane)
    {
        if (spawnLanes.Length < 3)
        {
            Debug.LogWarning("spawnLanes が3つ未満です");
            return;
        }

        int spawned = 0;
        for (int i = 0; i < spawnLanes.Length; i++)
        {
            if (i == excludedLane) continue;

            Vector3 basePos = spawnLanes[i].position;
            Vector3 spawnPos = basePos + moveDirection.normalized * offsetDistance;

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            // 個別速度を設定（例：ランダム）
            float speed = Random.Range(minSpeed, maxSpeed);
            var mover = enemy.GetComponent<StraightEnemy>();
            if (mover != null)
            {
                mover.moveSpeed = speed;
            }

            spawned++;
            if (spawned >= 2) break;
        }
    }

}
