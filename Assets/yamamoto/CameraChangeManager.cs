using Unity.Cinemachine;
using UnityEngine;

public class CameraChangeManager : MonoBehaviour,ITitleObject,IResultObject
{
    [SerializeField]private CinemachineCamera TitleResultCamera;
    [SerializeField] private CinemachineCamera GameCamera;

    private int activePriority = 10;
    private int inactivePriority = 5;

    public void TitleSet()
    {
        GameCamera.Priority = inactivePriority;
        TitleResultCamera.Priority = activePriority;
    }

    public void TitleEnd()
    {
        GameCamera.Priority = activePriority;
        TitleResultCamera.Priority = inactivePriority;
    }

    public void ResultSet()
    {
        GameCamera.Priority = inactivePriority;
        TitleResultCamera.Priority = activePriority;
    }

    public void ResultEnd()
    {
        GameCamera.Priority = activePriority;
        TitleResultCamera.Priority = inactivePriority;
    }
}
