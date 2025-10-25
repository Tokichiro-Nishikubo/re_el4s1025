using Unity.Cinemachine;
using UnityEngine;

public class CameraChangeManager : MonoBehaviour,ITitleObject
{
    // Unityエディタから設定できるようにPublicで宣言
    public CinemachineCamera TitleCamera;
    public CinemachineCamera GameCamera;

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
