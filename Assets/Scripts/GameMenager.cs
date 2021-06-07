

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenager : MonoBehaviour
{
    AudioSource audioSource;

    public int points = 0;

    public MusicScript musicScript;

    bool lessTime = false;

    public Text clockText;
    public Text diamondText;
    public Image freezeTime;

    public GameObject infoPanel;
    public Text pauseEnd;
    public Text reloadInfo;
    public Text useInfo;



    public void AddPoints(int point)
    {
        points += point;
        diamondText.text = points.ToString();
    }

    public void AddTime(int addTime)
    {
        timeToEnd += addTime;
        clockText.text = timeToEnd.ToString();
    }

    public void FreezTime(int freez)
    {
        CancelInvoke("Stopper");
        freezeTime.enabled = true;
        InvokeRepeating("Stopper", freez, 5);
    }


    public int redKey = 0;
    public int greenKey = 0;
    public int goldkey = 0;


    void Stopper()
    {
        timeToEnd--;
        clockText.text = timeToEnd.ToString();
        freezeTime.enabled = false;
        Debug.Log("Time: " + timeToEnd + "s");

        if (timeToEnd <= 0)
        {
            timeToEnd = 0;
            endGame = true;
            Time.timeScale = 0f;
            gamePaused = true;
        }
        if (endGame)
        {
            EndGame();
        }

        if (timeToEnd<20 && !lessTime)
        {
            LessTimeOn();
            lessTime = true;
        }

        if (timeToEnd > 20 && lessTime)
        {
            LessTimeOff();
            lessTime = false;
        }
    }
    public void PauseGame()
    {
        PlayClip(pauseGame);
        infoPanel.SetActive(true);
        Debug.Log("Pause Game");
        Time.timeScale = 0f;
        gamePaused = true;
        musicScript.OnPauseGame();
    }
    public void ResumeGame()
    {
        PlayClip(resumeGame);
        infoPanel.SetActive(false);
        Debug.Log("Resume Game");
        Time.timeScale = 1f;
        gamePaused = false;
        musicScript.OnResumeGame();
    }
    public void EndGame()
    {
        CancelInvoke("Stopper");
        infoPanel.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        if (win)
        {
            pauseEnd.text = "You Win!";
            reloadInfo.text = "Realod? Y/N";
            PlayClip(winClip);
        }
        else
        {
            PlayClip(loseClip);
            pauseEnd.text = "You Lose!";
            reloadInfo.text = "Reload? Y/N";
        }
            
            
    }

    public void LessTimeOn()
    {
        musicScript.PitchThis(1.5f);
    }

    public void LessTimeOff()
    {
        musicScript.PitchThis(1f);
    }

    public void AddKey(KeyColor color)
    {
        if (color == KeyColor.Gold)
        {
            goldkey++;
        }
        else if(color == KeyColor.Green)
        {
            greenKey++;
        }
        else if (color == KeyColor.Red)
        {
            redKey++;
        }
    }

    public void PlayClip(AudioClip playClip)
    {
        audioSource.clip = playClip;
        audioSource.Play();
    }

    public AudioClip resumeGame;
    public AudioClip pauseGame;
    public AudioClip winClip;
    public AudioClip loseClip;


    public static GameMenager gameMenager;
    public int timeToEnd;
    bool gamePaused = false;
    bool endGame = false;
    bool win = false;
    // Start is called before the first frame update
    void Start()
    {
        if (gameMenager == null)
        {
            gameMenager = this;
        }
        if (timeToEnd <= 0)
        {
            timeToEnd = 20;
        }
        Debug.Log("Time " + timeToEnd + "s");
        InvokeRepeating("Stopper", 2, 1);

        audioSource = GetComponent<AudioSource>();

        freezeTime.enabled = false;
        clockText.text = timeToEnd.ToString();
        infoPanel.SetActive(false);
        pauseEnd.text = "Pause";
        reloadInfo.text = "";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gamePaused)
                {
                    ResumeGame();
                }
            else { PauseGame(); }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Time: " + timeToEnd);
            Debug.Log("Key red: " + redKey + "green: " + greenKey + "gold: " + goldkey);
            Debug.Log("Points: " + points);
        }

        
    }
}
