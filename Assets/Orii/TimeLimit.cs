using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour
{
    //�����ݒ莞��
    [SerializeField] float firstTime = 120;
    //�o�ߎ���
    float elapsedTime = 0;
    float nowTime = 0;
    Text timeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeText = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //���b�o�߂�����
        elapsedTime += Time.deltaTime;
        //�o�߂������Ԃ������ݒ莞�Ԃ������
        nowTime = firstTime - elapsedTime;

        //�}�C�i�X�b�ɂȂ�Ȃ��悤�ɂ���
        if (nowTime < 0)
        {
            nowTime = 0;
        }

        //�e�L�X�g�̏o�͕���
        {
            //��
            int minutes = Mathf.FloorToInt(nowTime / 60f);
            //�b
            int seconds = Mathf.FloorToInt(nowTime % 60f);
            //�~���b
            int milliseconds = Mathf.FloorToInt((nowTime % 1f) * 100f);

            timeText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
        }
    }


    //�Ăяo�����u�ԂɌo�ߎ��Ԃ��ŏ�����ɂȂ�
    void TimeRestart()
    {
        elapsedTime = 0;
    }
}
