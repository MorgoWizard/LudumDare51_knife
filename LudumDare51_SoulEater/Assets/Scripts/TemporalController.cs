using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TemporalController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI altarText;
    private float Timer;
    private float LockTimer;
    
    public SoulEater Altar;
    [SerializeField] private EventController eventController;
    // public Scrollbar HungerDisplay;
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
            //eventController.RandomEvent();
            altarText.text = Altar.Digesting();
            TimerLock = true;
            LockTimer = 1;
            
        }
        else 
        {
            LockTimer -= Time.deltaTime;
            if (LockTimer < 0) TimerLock = false;
        }
        // HungerDisplay.size = Altar.GetPercentOfHungry();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
