using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("ゲームルール系")]
    [SerializeField, Min(0f)] private float _gameOverRotateValue = 30f;                 // どれくらい回転したらゲームオーバーになるか
    [SerializeField, Min(0f)] private float _rotateLimit = 35.0f;
    [SerializeField, Min(0f)] private float _limitShake = 20f;
    [SerializeField, Min(0f)] private float _forwardShakingRotate = 1.0f;               // 前進したときの揺れ
    [SerializeField, Min(1f)] private float _forwardMaxRandomMagnitude = 3.0f;
    [SerializeField, Min(0f)] private float _forwardSpeedSec = 1.0f;
    [SerializeField, Min(0f)] private float _endureGameOverRotateDurationSec = 0.3f;   // 限界で何秒耐えるか
    [SerializeField, Range(0f, 0.99f)] private float _tiltAttenuationSec = 0.5f;
    [SerializeField, Min(0f)] private float _tiltMinStrSec = 0.2f;
    [SerializeField, Min(0f)] private float _jumpHeight = 1.0f;
    [SerializeField, Min(0f)] private float _jumpDurationSec = 1.0f;
    [SerializeField, Min(0f)] private float _myWeight = 0.2f;
    [Header("model")]
    [SerializeField] private GameObject _rotateModelObject;

    private Transform _transCache = null;
    private Vector3 _firstPos = Vector3.zero;

    public bool IsGame { get; set; } = true;

    public bool IsJump { get; private set; } = false;
    private float _jumpTimer = 0f;

    public float CurShakingSpeed { get; set; }

    private float _curTiltStr = 0f;


    void Start()
    {
        _transCache = transform;
        _firstPos = _transCache.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGame) return;

        if (!IsJump && Input.GetKey(KeyCode.Space))
        {
            _jumpTimer = 0f;
            IsJump = true;
        }

        if(IsJump)
        {
            JumpUpdate();
            return;
        }


        if(Input.GetKey(KeyCode.W))ForwardMove();

        if (Input.GetKey(KeyCode.A))
        {
            TiltBody(1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            TiltBody(-1f);
        }
        else
        {
            TiltBody(0f);
            _curTiltStr = _curTiltStr * _tiltAttenuationSec;
        }
    }

    public  float GetRotateAngle()
    {
        return Vector3.SignedAngle(Vector3.up, _rotateModelObject.transform.up, Vector3.forward);
    }

    private void JumpUpdate()
    {
        _jumpTimer += Time.deltaTime;

        if(_jumpTimer >= _jumpDurationSec)
        {
            IsJump = false;
            _jumpTimer = _jumpDurationSec;
        }

        float t = ((_jumpDurationSec * 0.5f) - Mathf.Abs(_jumpDurationSec * 0.5f - _jumpTimer)) / _jumpDurationSec * 0.5f;
        Debug.Log(t);
        float height = EaseLib.EaseOutQuart(t) * _jumpHeight;

        Vector3 pos = _transCache.position;
        pos.y = _firstPos.y + height;

        _transCache.position = pos;
    }


    private void ForwardMove()
    {
        Vector3 pos = _transCache.transform.position;
        Vector3 fw = _transCache.transform.forward;

        pos += fw * Time.deltaTime * _forwardSpeedSec;
        _transCache.transform.position = pos;

        float mag = UnityEngine.Random.Range(1.0f, _forwardMaxRandomMagnitude);
        CurShakingSpeed += _forwardShakingRotate * Time.deltaTime * RotSign() * mag;
    }

    private void TiltBody(float dir)
    {
        _curTiltStr += dir * _tiltMinStrSec * Time.deltaTime;

        // 現在の傾きの力をshakeに加える
        float zAngle = Vector3.SignedAngle(Vector3.up, _rotateModelObject.transform.up, Vector3.forward);
        CurShakingSpeed += Mathf.Abs(zAngle) * Time.deltaTime * _myWeight * GetSign(zAngle);

        // 振動を制限
        CurShakingSpeed = Mathf.Clamp(CurShakingSpeed, -_limitShake, _limitShake);

        float addRotate = CurShakingSpeed + _curTiltStr;
        if (Mathf.Abs(zAngle) <= _rotateLimit || GetSign(zAngle) != GetSign(addRotate)) _rotateModelObject.transform.localRotation *= Quaternion.Euler(0, 0, addRotate);


        CurShakingSpeed = 0f;
    }

    private float RotSign()
    {
        if (_rotateModelObject.transform.localEulerAngles.z == 0.0f)
        {
            return RandomSign();
        }
        else
        {
            return _rotateModelObject.transform.localEulerAngles.z > 0f ? 1.0f : -1.0f;
        }
    }

    private float GetSign(float value)
    {
        return value > 0f ? 1f : -1f;
    }

    private float  RandomSign()
    {
        int rnd = UnityEngine.Random.Range(0, 2);
        return rnd == 0 ? 1.0f : -1.0f;
    }
}
