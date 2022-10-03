using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SoulEater : MonoBehaviour
{
    public int Score = 0;
    public int MaxScore = 0;
    public int CurrentMaxScore = 0;
    public int CostOfSoul = 1;
    public int HungryLevel = 1;
    
    private Player player;
    private bool isPlayerNearby = false;

    [SerializeField] private TextMeshProUGUI eaterOutput;
    
    public AudioSource stage1ForPlay;
    public AudioSource stage2ForPlay;
    public AudioSource stage3ForPlay;
    public AudioSource stage4ForPlay;

    public AudioSource BackgroundMusic;

    void Start()
    {
        CurrentMaxScore = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        eaterOutput.text = Score + "/" + HungryLevel;
    }

    private void Update()
    {
        GetPlayerSouls();
    }

    public void EatSoul()
    {
        Score += CostOfSoul;
        CurrentMaxScore += CostOfSoul;
    }
    public void EatSoul(int Count)
    {
        Score += CostOfSoul * Count;
        CurrentMaxScore += CostOfSoul * Count;
    }
    public string Digesting()
    {
        Score -= HungryLevel;
        HungryLevel++;
        eaterOutput.text = Score + "/" + HungryLevel;
        if (Score <= 0)
        {
            player.TakeDamage(999);
            return ("Death");
        }
        else if (HungryLevel+0.5f >= Score )
        {
            stage4ForPlay.Play();
            return ("I am REALLY hungry!");
        }
        else if (2 * HungryLevel + 2 >= Score)
        {
            stage3ForPlay.Play();
            return ("I'm very hungry");
        }
        else if (3 * HungryLevel + 4.5f >= Score)
        {
            stage2ForPlay.Play();
            return ("I'm hungry");
        }
        else
        {
            stage1ForPlay.Play();
            return ("It would be nice to eat");
        }
    }
    public int GetMaxScore()
    {
        if(MaxScore > CurrentMaxScore) return MaxScore;
        else return CurrentMaxScore;
    }
    public void SetMaxScore(int Value)
    {
        MaxScore = Value;
    }

    private void GetPlayerSouls()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Score += player.GetSouls();
            CurrentMaxScore += player.GetSouls();
            player.ResetSouls();
            eaterOutput.text = Score + "/" + HungryLevel;
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
