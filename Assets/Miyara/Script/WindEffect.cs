using System.Collections;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    [Header("���̐ݒ�")]
    [SerializeField] private Vector3 windCenterPosition = Vector3.zero;
    [SerializeField] private Vector3 windDirection = Vector3.right;           // ���̕���
    [SerializeField] private float windStrength = 1.0f;                       // �o���p�x�ɉe�� (0.0�`1.0)
    [SerializeField] private float windDuration = 5.0f;                       // ������������
    [SerializeField] private float spawnRadius = 2.0f;

    [Header("�����q�v���n�u")]
    [SerializeField] private GameObject windParticlePrefab;


    private Coroutine windCoroutine; // �R���[�`���̎Q�Ƃ�ێ�


    /// <summary>
    /// ���G�t�F�N�g�̃p�����[�^�Z�b�g
    /// </summary>
    /// <param name="position"> ���S�ʒu </param>
    /// <param name="direction"> ���̕��� </param>
    /// <param name="strength"> ���G�t�F�N�g�̕p�x </param>
    /// <param name="duration"> ������������ </param>
    /// <param name="radius"> �����_���o���ʒu�͈� </param>
    public void SetWindParameters(Vector3 position, Vector3 direction, float strength, float duration, float radius)
    {
        windCenterPosition = position;
        windDirection = direction.normalized;
        windStrength = strength;
        windDuration = duration;
        spawnRadius = radius;
    }


    /// <summary>
    /// ���J�n
    /// </summary>
    public void StartWind()
    {
        if (windCoroutine == null)
        {
            windCoroutine = StartCoroutine(EmitWindParticles());
        }
    }

    /// <summary>
    /// ����~
    /// </summary>
    public void StopWind()
    {
        if (windCoroutine != null)
        {
            StopCoroutine(windCoroutine);
            windCoroutine = null;
        }
    }


    private IEnumerator EmitWindParticles()
    {
        float elapsed = 0f;
        while (elapsed < windDuration)
        {
            SpawnWindParticle();
            float interval = Mathf.Lerp(0.5f, 0.05f, windStrength);
            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }
    }

    private void SpawnWindParticle()
    {
        Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
        Vector3 spawnPosition = windCenterPosition + randomOffset;

        GameObject particle = Instantiate(windParticlePrefab, spawnPosition, Quaternion.identity);

        // WindParticle �X�N���v�g�ɕ����Ǝ�����n��
        WindParticle wp = particle.GetComponent<WindParticle>();
        if (wp != null)
        {
            wp.Initialize(windDirection.normalized, 3.0f); // ����3�b�Ȃ�
        }
    }


}
