using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour
{
    //初期設定時間
    [SerializeField] float firstTime = 120;
    //経過時間
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
        //何秒経過したか
        elapsedTime += Time.deltaTime;
        //経過した時間を初期設定時間から引く
        nowTime = firstTime - elapsedTime;

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


    //呼び出した瞬間に経過時間が最初からになる
    void TimeRestart()
    {
        elapsedTime = 0;
    }
}
