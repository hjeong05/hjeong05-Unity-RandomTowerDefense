using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    public Text OverNClear;     // 클리어,게임오버 텍스트

    public AudioSource audioSourceWin;  // 게임 승리 오디오
    public AudioSource audioSourceLose;  // 게임 졌을 때 오디오

    void Start()
    {
        if (PlayerManager.WinNLose == 2)
        {
            if (audioSourceWin != null)
                audioSourceWin.Play();
            OverNClear.text = "Game Clear !";
        }
        if ( PlayerManager.WinNLose == 1)
        {
            if (audioSourceLose != null)
                audioSourceLose.Play();
            OverNClear.text = "Game Over !";
        }        
    }
    public void loadGameScene()
    {
        PlayerManager.instance.Init();
        SceneManager.LoadScene("GameScene");
    }
    public void loadMenuScene()
    {
        PlayerManager.instance.Init();
        SceneManager.LoadScene("FirstScene");
    }
}

