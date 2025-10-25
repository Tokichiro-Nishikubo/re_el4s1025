using System.Collections;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] titleObjects;
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private float _fadeOutDurationSec = 1.5f;

    private bool _shouldHideTitle = false;

    private void Start()
    {
    }

    private void Update()
    {
        if(!_shouldHideTitle && Input.GetKeyDown(KeyCode.Space))
        {
            CallTitleEnd();
            _shouldHideTitle = true;
        }
    }

    public void CallTitleSet()
    {
        foreach (var obj in titleObjects)
        {
            
            obj.GetComponent<ITitleObject>().TitleSet();
        }
    }

    public void CallTitleEnd()
    {
        foreach (var obj in titleObjects)
        {
            obj.GetComponent<ITitleObject>().TitleEnd();
        }

        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        while (true)
        {
            yield return null; // 1フレーム待つ
            _canvasGroup.alpha -= Time.deltaTime / _fadeOutDurationSec;

            if (_canvasGroup.alpha <= 0f) break;

        }

        GameManager.Instance.StartGame();
    }
}
