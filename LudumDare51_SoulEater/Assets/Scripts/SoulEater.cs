using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoulEater : MonoBehaviour
{
    public int Score = 0;
    public int MaxScore = 1;
    public int CostOfSoul = 1;
    public int HungryLevel = 1;
    
    private Player player;
    private bool isPlayerNearby = false;

    public AudioSource stage1ForPlay;
    public AudioSource stage2ForPlay;
    public AudioSource stage3ForPlay;
    public AudioSource stage4ForPlay;
    public AudioSource BackgroundMusic;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        GetPlayerSouls();
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
        if (Score <= 0) return ("Смерть");
        else if (Score / (float)MaxScore <= 0.1)
        {
            stage4ForPlay.Play();
            return ("Я КРАЙНЕ ГОЛОДЕН!");
        }
        else if (Score / (float)MaxScore <= 0.25)
        {
            stage3ForPlay.Play();
            return ("Я Очень голоден!");
        }
        else if (Score / (float)MaxScore <= 0.5)
        {
            stage2ForPlay.Play();
            return ("Я голоден.");
        }
        else
        {
            stage1ForPlay.Play();
            return ("Неплохо было бы поесть");
        }
    }
    public float GetPercentOfHungry()
    {
        return Score / (float)MaxScore;
    }

    private void GetPlayerSouls()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Score += player.GetSouls();
            MaxScore += player.GetSouls();
            player.ResetSouls();
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
