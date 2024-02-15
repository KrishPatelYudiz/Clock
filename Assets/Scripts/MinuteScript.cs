using System;
using UnityEngine;

public class MinuteScript : MonoBehaviour
{
    [SerializeField] Transform center;
    Transform OriginPosition;
    bool ClockType = true;
    int LastMinute = 0;
    void Start()
    {
        center = transform.parent.transform;

        OriginPosition = transform;
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
        if (ClockType)
        {
            SmoothTimeSet();
        }
        else
        {
            OnPointTimeSet();
        }
        LastMinute = System.DateTime.Now.Minute;
    }
    void SmoothTimeSet()
    {
        transform.position = OriginPosition.position;
        transform.rotation = OriginPosition.rotation;
        var time = System.DateTime.Now;
        float angle = 360 * (time.Second + time.Minute * 60) / 3600;
        transform.RotateAround(center.position, Vector3.forward, angle);
    }

    void OnPointTimeSet()
    {
        transform.position = OriginPosition.position;
        transform.rotation = OriginPosition.rotation;
        var time = System.DateTime.Now;
        float angle = time.Minute * 360 / 60;
        transform.RotateAround(center.position, Vector3.forward, angle);
    }
    public void WaitAndMove()
    {

        if (System.DateTime.Now.Minute == LastMinute)
        {
            LastMinute++;
            LastMinute %= 60;
            float angle = 360 / 60;
            transform.RotateAround(center.position, Vector3.forward, angle);
        }
    }
    public void SmoothMove()
    {
        float angle = Time.deltaTime * 360 / 3600;
        transform.RotateAround(center.position, Vector3.forward, angle);
    }
}
