using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoulEater : MonoBehaviour
{
    public int Score;
    private int MaxScore;
    public int CostOfSoul = 1;
    public int HungryLevel = 1;
    void Start()
    {
        Score = 0;
        MaxScore = 1;
    }
    public void EatSoul()
    {
        Score += CostOfSoul;
        MaxScore += CostOfSoul;
    }
    public void EatSoul(int Count)
    {
        Score += CostOfSoul * Count;
        MaxScore += CostOfSoul * Count;
    }
    public string Digesting()
    {
        Score -= HungryLevel;
        HungryLevel++;
        if (Score == 0) return("������");
        else if (Score / MaxScore <= 0.1) return ("� ������ �������!");
        else if (Score / MaxScore <= 0.25) return ("� ����� �������!");
        else if (Score / MaxScore <= 0.5) return ("� �������.");
        else return ("������� ���� �� ������");
    }
    public float GetPercentOfHungry()
    {
        return Score / (float)MaxScore;
    }
}
