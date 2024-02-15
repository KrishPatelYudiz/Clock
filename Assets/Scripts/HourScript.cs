using System;
using UnityEngine;

public class HourScript : MonoBehaviour
{
    [SerializeField] Transform center;
    Vector3 OriginPosition;
    bool ClockType = true;
    int LastMinute = 0;

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
        transform.position = OriginPosition;
        transform.rotation = Quaternion.identity;
        var time = System.DateTime.Now;
        float angle = 360 * (time.Second + (time.Minute * 60) + ((time.Hour > 12 ? time.Hour - 12 : time.Hour) * 3600)) / 43200;

        transform.RotateAround(center.position, Vector3.forward, angle);
    }

    void OnPointTimeSet()
    {
        transform.position = OriginPosition;
        transform.rotation = Quaternion.identity;
        var time = System.DateTime.Now;
        float angle = 360 * (((int)(time.Minute / 12)) + ((time.Hour > 12 ? time.Hour - 12 : time.Hour) * 5)) / 60;
        print(angle);
        transform.RotateAround(center.position, Vector3.forward, angle);
    }



    public void WaitAndMove()
    {
        if (System.DateTime.Now.Minute % 12 == 0 && System.DateTime.Now.Minute != LastMinute)
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
