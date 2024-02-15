using System;
using UnityEngine;

public class ClockScript : MonoBehaviour
{

    [SerializeField]
    bool ClockType = true;
    bool LastClockType = true;
    public static event EventHandler ChangeClockType;

    void Update()
    {
        if (ClockType != LastClockType)
        {
            ChangeClockType?.Invoke(this, EventArgs.Empty);
            LastClockType = ClockType;
        }

    }
}
