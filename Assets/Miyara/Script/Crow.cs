using UnityEngine;

public class Crow : MonoBehaviour
{
    [System.Serializable]
    public struct LaneSpawnSetting
    {
        public int laneIndex;
        public bool mode; // true �� �����[����2�̃X�|�[���Afalse �� �w�背�[����1��
    }


    [Header("�ړ��ݒ�")]
    [SerializeField] public Vector3 moveDirection = Vector3.forward;
    [SerializeField] public float moveSpeed = 2.0f;

    [Header("���[���ݒ�")]
    [SerializeField] public Transform[] spawnLanes;
    [SerializeField] public LaneSpawnSetting[] laneSettings;

    [Header("�G�l�~�[�ݒ�")]
    [SerializeField] private GameObject enemyPrefab;


    private int spawnCount = 0; // Spawn�֐��̌Ăяo����



    /// <summary>
    /// �J���X�̃X�|�[��
    /// </summary>
    public void SpawnCrow(float offsetDistance)
    {
        if (laneSettings.Length == 0 || spawnLanes.Length == 0)
        {
            Debug.LogWarning("laneSettings �܂��� spawnLanes �����ݒ�ł�");
            return;
        }

        int index = spawnCount % laneSettings.Length;
        LaneSpawnSetting setting = laneSettings[index];

        if (setting.mode)
        {
            SpawnCrowOtherLanes(offsetDistance, setting.laneIndex);
        }
        else
        {
            SpawnCrowOneLane(offsetDistance, setting.laneIndex);
        }

        spawnCount++;
    }

    /// <summary>
    /// �w�背�[���ɓG���X�|�[��
    /// </summary>
    private void SpawnCrowOneLane(float offsetDistance, int lane)
    {
        if (lane < 0 || lane >= spawnLanes.Length)
        {
            Debug.LogWarning($"laneIndex �̒l {lane} �� spawnLanes �͈̔͊O�ł�");
            return;
        }

        Vector3 basePos = spawnLanes[lane].position;
        Vector3 spawnPos = basePos + moveDirection.normalized * offsetDistance;

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

    }

    /// <summary>
    /// �w�背�[���ȊO��2���[���ɓG���X�|�[��
    /// </summary>
    private void SpawnCrowOtherLanes(float offsetDistance, int excludedLane)
    {
        if (spawnLanes.Length < 3)
        {
            Debug.LogWarning("spawnLanes ��3�����ł�");
            return;
        }

        int spawned = 0;
        for (int i = 0; i < spawnLanes.Length; i++)
        {
            if (i == excludedLane) continue;

            Vector3 basePos = spawnLanes[i].position;
            Vector3 spawnPos = basePos + moveDirection.normalized * offsetDistance;

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            spawned++;
            if (spawned >= 2) break;
        }
    }

}
