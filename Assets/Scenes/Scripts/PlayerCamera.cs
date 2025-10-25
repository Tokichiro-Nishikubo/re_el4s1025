using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;

    [SerializeField, Min(0f)] private float _angleLimit = 15f;

    void Start()
    {
        
    }


    void Update()
    {
        if (!_controller.IsGame) return;

        float angle =  _controller.GetRotateAngle();

        if(Mathf.Abs(angle) > _angleLimit)
        {
            angle = _angleLimit * (angle > 0f ? 1.0f : -1.0f);
        }


        // 回転方向を反転
        angle *= -1.0f;

        transform.rotation = Quaternion.AngleAxis(angle, transform.up);
    }
}
