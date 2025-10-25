using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour
{
    float nowTime = 0;
    Text timeText;

    void Start()
    {
        timeText = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        //経過した時間を初期設定時間から引く
        nowTime = GameManager.Instance.GameTimer;

        //マイナス秒にならないようにする
        if (nowTime < 0)
        {
            nowTime = 0;
        }

        //テキストの出力部分
        {
            //分
            int minutes = Mathf.FloorToInt(nowTime / 60f);
            //秒
            int seconds = Mathf.FloorToInt(nowTime % 60f);
            //ミリ秒
            int milliseconds = Mathf.FloorToInt((nowTime % 1f) * 100f);

            timeText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
        }
    }
}
