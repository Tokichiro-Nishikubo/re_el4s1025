using System;
using System.Collections;
using UnityEngine;

public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
    private CanvasGroup _canvasGroup;

    private float _duration = 1f;
    Action _callback = null;

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeSet(float x)
    {
        _canvasGroup.alpha = x;
    }

    public void FadeIn(float duration, Action callback = null)
    {
        _duration = duration;
        _callback = callback;
        StartCoroutine("FadeInCorutine");
    }

    public void FadeOut(float duration, Action callback = null)
    {
        _duration = duration;
        _callback = callback;
        StartCoroutine("FadeOutCorutine");
    }


    IEnumerator FadeInCorutine()
    {
        if (_duration == 0f)
        {
            _canvasGroup.alpha = 1f;
        }
        else
        {
            while (true)
            {
                yield return null; // 1フレーム待つ
                _canvasGroup.alpha += Time.deltaTime / _duration;

                if (_canvasGroup.alpha >= 1f) break;

            }
        }

        if (_callback != null) _callback.Invoke();
    }

    IEnumerator FadeOutCorutine()
    {
        if (_duration == 0f)
        {
            _canvasGroup.alpha = 0f;
        }
        else
        {
            while (true)
            {
                yield return null; // 1フレーム待つ
                _canvasGroup.alpha -= Time.deltaTime / _duration;

                if (_canvasGroup.alpha <= 0f) break;

            }
        }

        if (_callback != null) _callback.Invoke();
    }
}
