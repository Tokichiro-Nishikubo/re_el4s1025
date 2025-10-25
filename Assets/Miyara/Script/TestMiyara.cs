using UnityEngine;

public class TestMiyara : MonoBehaviour
{

    public WindEffect wind;

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
    }
}
