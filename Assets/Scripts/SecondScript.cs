using System;
using UnityEngine;

public class SecondScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform center;
    Vector3 OriginPosition;
    bool ClockType = true;
    int LastSecond = 0;
    private void Awake()
    {
        ClockScript.ChangeClockType += ChangeType;

    }
    void Start()
    {
        center = transform.parent.transform;

        OriginPosition = transform.position;
        SmoothTimeSet();
    }
    private void Update()
    {
        if (ClockType)
        {
            SmoothMove();
        }
        else
        {
            WaitAndMove();
        }
    }

    void ChangeType(object v, EventArgs e)
    {
        ClockType = !ClockType;
        SmoothTimeSet();
        LastSecond = System.DateTime.Now.Second;
    }

    void SmoothTimeSet()
    {
        transform.position = OriginPosition;
        transform.rotation = Quaternion.identity;

        var time = System.DateTime.Now;
        float angle = 360 * time.Second / 60;
        transform.RotateAround(center.position, Vector3.forward, angle);
    }

    public void WaitAndMove()
    {
        if (System.DateTime.Now.Second == LastSecond)
        {

            float angle = 360 / 60;
            transform.RotateAround(center.position, Vector3.forward, angle);
            LastSecond++;
            LastSecond %= 60;

        }

    }
    public void SmoothMove()
    {
        float angle = Time.deltaTime * 360 / 60;
        transform.RotateAround(center.position, Vector3.forward, angle);
    }
}
