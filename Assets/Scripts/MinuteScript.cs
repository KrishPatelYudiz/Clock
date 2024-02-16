using System;
using System.Collections;
using UnityEngine;

public class MinuteScript : MonoBehaviour
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
        float angle = 360 * (time.Second + time.Minute * 60) / 3600;
        transform.RotateAround(center.position, Vector3.forward, angle);
    }

    void OnPointTimeSet()
    {
        transform.position = OriginPosition;
        transform.rotation = Quaternion.identity;
        var time = System.DateTime.Now;
        float angle = time.Minute * 360 / 60;
        transform.RotateAround(center.position, Vector3.forward, angle);
    }
    IEnumerator WaitAndMove()
    {
        while (true)
        {

            if (System.DateTime.Now.Second == 0)
            {
                float angle = 360 / 60;
                transform.RotateAround(center.position, Vector3.forward, angle);
                yield return new WaitForSecondsRealtime(60);
            }
            yield return null;
        }
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
