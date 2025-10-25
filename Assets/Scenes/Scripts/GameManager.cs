using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] public float MaxPlayDurationSec = 100f;
    [SerializeField] public PlayerController PlayerController;
    [SerializeField] private Crow _spawner = null;
    [SerializeField] private WindEffect _wind = null;

    [SerializeField, Min(0f)] private float _crowSpawnTimer = 0f;
    private float _spawnTimer = 0f;

    [SerializeField, Min(0f)] private float _windIntervalDurationSec = 0f;
    private float _windTimer = 0f;

    private float _gameTimer = 0f;
    public float GameTimer { get { return MaxPlayDurationSec - _gameTimer; } }

    public bool IsGame { get; set; } = false;

    private void Start()
    {
        FadeManager.Instance.FadeSet(1f);
        FadeManager.Instance.FadeOut(1f);
    }

    public void StartGame()
    {
        PlayerController.IsGame = true;
        IsGame = true;
    }

    public void GameOver()
    {
        FadeManager.Instance.FadeIn(2f, () =>
        {
            // GameOverシーンを呼ぶ
            SceneManager.LoadScene("GameOver");
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGame) return;
        _spawnTimer += Time.deltaTime;

        if(_spawnTimer >= _crowSpawnTimer)
        {
            _spawnTimer = 0f;
            _spawner.SpawnCrow(0f);
        }

        _windTimer += Time.deltaTime;

        if(_windTimer >= _windIntervalDurationSec)
        {
            _windTimer = 0f;
            _wind.StartWind();
        }

        _gameTimer += Time.deltaTime;
        if(GameTimer <= 0f)
        {
            GameOver();
        }
    }

    public void Clear()
    {

    }
}
