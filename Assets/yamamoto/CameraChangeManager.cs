using Unity.Cinemachine;
using UnityEngine;

public class CameraChangeManager : MonoBehaviour,ITitleObject
{
    [SerializeField]private CinemachineCamera TitleCamera;
    [SerializeField] private CinemachineCamera GameCamera;

    private int activePriority = 10;
    private int inactivePriority = 5;

    public void GameStart()
    {
        GameCamera.Priority = activePriority;
        TitleCamera.Priority = inactivePriority;
    }

    public void TitleSet()
    {
        GameCamera.Priority = inactivePriority;
        TitleCamera.Priority = activePriority;
    }

}
