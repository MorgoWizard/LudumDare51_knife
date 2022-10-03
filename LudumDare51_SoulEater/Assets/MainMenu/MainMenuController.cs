using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public GameObject StandartHUD;
    public GameObject MainMenu;
    public SoulEater VolumeControl;
    public Slider SetVolumeAudio;
    public Slider SetVolumeMusic;
    [SerializeField] private TextMeshProUGUI MaxScore;

    public void ChangeVolumeAudio()
    {
        VolumeControl.stage1ForPlay.volume = SetVolumeAudio.value;
        VolumeControl.stage2ForPlay.volume = SetVolumeAudio.value;
        VolumeControl.stage3ForPlay.volume = SetVolumeAudio.value;
        VolumeControl.stage4ForPlay.volume = SetVolumeAudio.value;
    }
    public void ChangeVolumeMusic()
    {
        VolumeControl.BackgroundMusic.volume = SetVolumeMusic.value;
    }
    public void ExitGame()
    {
        PlayerPrefs.SetInt("MaxScore", VolumeControl.GetMaxScore());
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
        Application.Quit();
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("MaxScore"))
        {
            VolumeControl.SetMaxScore(PlayerPrefs.GetInt("MaxScore"));
            
            Debug.Log("Game data loaded!");
        }
        else
            Debug.Log("There is no save data!");
        MaxScore.text = "MAX\nSCORE\n" + VolumeControl.GetMaxScore();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            StandartHUD.SetActive(!StandartHUD.activeSelf);
            MainMenu.SetActive(!MainMenu.activeSelf);
            if (MainMenu.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }
    }
}
