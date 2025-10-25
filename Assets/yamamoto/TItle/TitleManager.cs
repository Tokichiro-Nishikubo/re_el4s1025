using System.Linq;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] titleObjects;


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
    }
}
