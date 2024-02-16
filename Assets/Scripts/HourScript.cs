using System;
using System.Collections;
using UnityEngine;

public class HourScript : MonoBehaviour
{
    [SerializeField] Transform center;
    Vector3 OriginPosition;
    bool ClockType = true;
    Coroutine smoothMove;
    Coroutine waitAndMove;

    private void Awake()
    {
        ClockScript.ChangeClockType += ChangeType;

    }
    void Start()
    {
        center = transform.parent.transform;

        OriginPosition = transform.position;
        SmoothTimeSet();
        smoothMove = StartCoroutine(SmoothMove());
    }
    void ChangeType(object v, EventArgs e)
    {
        ClockType = !ClockType;
        if (ClockType)
        {
            SmoothTimeSet();
            StopCoroutine(waitAndMove);
            smoothMove = StartCoroutine(SmoothMove());
        }
        else
        {
            OnPointTimeSet();
            StopCoroutine(smoothMove);
            waitAndMove = StartCoroutine(WaitAndMove());
        }
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
        transform.RotateAround(center.position, Vector3.forward, angle);
    }



    IEnumerator WaitAndMove()
    {
        if (System.DateTime.Now.Minute % 12 == 0)
        {
            float angle = 360 / 60;
            transform.RotateAround(center.position, Vector3.forward, angle);
            yield return new WaitForSecondsRealtime(12 * 60);
        }
        yield return null;


    }
    IEnumerator SmoothMove()
    {
        while (true)
        {
            float angle = Time.deltaTime * 360 / 3600;
            transform.RotateAround(center.position, Vector3.forward, angle);
            yield return null;
        }
    }
}
