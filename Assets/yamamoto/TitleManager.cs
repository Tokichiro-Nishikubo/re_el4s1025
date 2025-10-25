using System.Linq;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private ITitleObject[] titleObjects;

    [System.Obsolete]
    private void Awake()
    {
        // �V�[����ɂ��� ITitleObject �����ׂĎ擾
        titleObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<ITitleObject>()
            .ToArray();
    }

    private void Start()
    {
        CallTitleSet();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CallGameStart();
        }
    }
    public void CallTitleSet()
    {
        foreach (var obj in titleObjects)
        {
            obj.TitleSet();
        }
    }

    public void CallGameStart()
    {
        foreach (var obj in titleObjects)
        {
            obj.GameStart();
        }
    }
}
