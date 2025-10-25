using KanKikuchi.AudioManager;
using System.Threading;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;

    private bool _isplay = false;
    private float _timer = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BGMManager.Instance.Play(BGMPath.GOAL);

        _isplay = true;
        FadeManager.Instance.FadeSet(1.0f);
        FadeManager.Instance.FadeOut(0.6f, () =>
        {
            _director.Play();
        });
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= 5f && _isplay)
        {
            _isplay = false;

            FadeManager.Instance.FadeIn(4.0f, () =>
            {
                SceneManager.LoadScene("Tunawatari");
            });
        }
    }
}
