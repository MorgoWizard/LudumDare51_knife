using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporalController : MonoBehaviour
{
    private float Timer;
    private float LockTimer;
    public SoulEater Altar;
    public Scrollbar HungerDisplay;
    private bool TimerLock = false;
    void Start()
    {
        Timer = 0;
    }
    void Update()
    {
        Timer += Time.deltaTime;
        if (Mathf.Floor(Timer) % 11 == 10 && !TimerLock)
        {
            Debug.Log(Altar.Digesting());
            TimerLock = true;
            LockTimer = 1;
        }
        else 
        {
            LockTimer -= Time.deltaTime;
            if (LockTimer < 0) TimerLock = false;
        }
        HungerDisplay.size = Altar.GetPercentOfHungry();
    }
}
