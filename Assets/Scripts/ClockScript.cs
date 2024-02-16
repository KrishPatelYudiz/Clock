using System;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    public static event EventHandler ChangeClockType;

    public void ChangeType()
    {
        ChangeClockType?.Invoke(this, EventArgs.Empty);
    }
}
