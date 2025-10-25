using System.Linq;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] resultObjects;



    public void CallResultSet()
    {
        foreach (var obj in resultObjects)
        {
            
            obj.GetComponent<IResultObject>().ResultSet();
        }
    }

    public void CallResultEnd()
    {
        foreach (var obj in resultObjects)
        {
            obj.GetComponent<IResultObject>().ResultEnd();
        }
    }
}
