using UnityEngine;

public class TestMiyara : MonoBehaviour
{

    public WindEffect wind;
    public Crow crow;

    float pos = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wind.StartWind();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            wind.StopWind();
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            crow.SpawnCrow(pos);
            pos += 10.0f;
        }
    }
}
