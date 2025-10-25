using System.Linq;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] titleObjects;

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
            
            obj.GetComponent<ITitleObject>().TitleSet();
        }
    }

    public void CallGameStart()
    {
        foreach (var obj in titleObjects)
        {
            obj.GetComponent<ITitleObject>().GameStart();
        }
    }
}
