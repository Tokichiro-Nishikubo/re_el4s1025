using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class WindEffect : MonoBehaviour
{
    [Header("風の設定")]
    [SerializeField] private Vector3 windCenterPosition = Vector3.zero;
    [SerializeField] private Vector3 windDirection = Vector3.right;           // 風の方向
    [SerializeField] private float windStrength = 1.0f;                       // 出現頻度に影響 (0.0～1.0)
    [SerializeField] private float windDuration = 5.0f;                       // 風が吹く時間
    [SerializeField] private float spawnRadius = 2.0f;

    [Header("風粒子プレハブ")]
    [SerializeField] private GameObject windParticlePrefab;

    // 内部的に保持
    private Coroutine windCoroutine;
    private Vector3 _windCenterPosition;
    private Vector3 _windDirection;          
    private float _windStrength ;         
    private float _windDuration;             
    private float _spawnRadius;

    private void Start()
    {
        _windCenterPosition = windCenterPosition;
        _windDirection = windDirection;
        _windStrength = windStrength;
        _windDuration = windDuration;
        _spawnRadius = spawnRadius;
    }

    /// <summary>
    /// 風エフェクトのパラメータセット
    /// </summary>
    /// <param name="position"> 中心位置 </param>
    /// <param name="direction"> 風の方向 </param>
    /// <param name="strength"> 風エフェクトの頻度 </param>
    /// <param name="duration"> 風が吹く時間 </param>
    /// <param name="radius"> ランダム出現位置範囲 </param>
    public void SetWindParameters(Vector3 position, Vector3 direction, float strength, float duration, float radius)
    {
        _windCenterPosition = position;
        _windDirection = direction.normalized;
        _windStrength = strength;
        _windDuration = duration;
        _spawnRadius = radius;


        windCenterPosition = _windCenterPosition;
        windDirection = _windDirection;
        windStrength = _windStrength;
        windDuration = _windDuration;
        spawnRadius = _spawnRadius;

    }


    /// <summary>
    /// 風開始
    /// </summary>
    public void StartWind()
    {
        if (windCoroutine != null)
        {
            StopCoroutine(windCoroutine);
        }
        
        
        windCenterPosition = _windCenterPosition;
        windDirection = _windDirection;
        windStrength = _windStrength;
        windDuration = _windDuration;
        spawnRadius = _spawnRadius;

        windCoroutine = StartCoroutine(EmitWindParticles());
        
    }

    /// <summary>
    /// 風停止
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
        Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * spawnRadius;
        Vector3 spawnPosition = windCenterPosition + randomOffset;

        GameObject particle = Instantiate(windParticlePrefab, spawnPosition, Quaternion.identity);

        // WindParticle スクリプトに方向と寿命を渡す
        WindParticle wp = particle.GetComponent<WindParticle>();
        if (wp != null)
        {
            wp.Initialize(windDirection.normalized, 3.0f); // 寿命3秒など
        }
    }


}
