using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager instance;

    public static int money;    // 설치용 돈
    private int startmoney = 0;   

    public static int tomato;   // 업그레이드용 돈 

    public static int reward;   // 보상
    public Text KillTxt;
    public static int kill;     // 킬수 

    public static int WinNLose;     // 게임 승리 여부 확인 변수 
    public GameObject[] lives = new GameObject[20];

    private int LifeIndex;    // 라이프 개수 인덱스

    public Text Level;  // 레벨 표현UI
    public static int levels;   

    public static bool IsNext;  // 다음 레벨 확인
    public static bool IsReady; // 다음 준비시간
    public static int lifeDecrease;
    void Start () {
        instance = this;
        WinNLose = 0;
        kill = 0;       // 적 킬 수 초기화
        levels = 0;     // 레벨 변수 초기화
        money = startmoney;     // 초기 돈(당근) 초기화
        tomato = 0;
        reward = 0;
        LifeIndex = 0; 
        IsReady = true;
        IsNext = true;
        lifeDecrease = 0;
    }
    public void Init()
    {
        WinNLose = 0;
        kill = 0;       // 적 킬 수 초기화
        levels = 0;     // 레벨 변수 초기화
        money = startmoney;     // 초기 돈(당근) 초기화
        tomato = 0;
        reward = 0;
        LifeIndex = 0;
        IsReady = true;
        IsNext = true;
        lifeDecrease = 0;
    }
    void Update()
    {
        if (lifeDecrease == 1) {    
            Destroy(lives[LifeIndex]);
            LifeIndex++;
            lifeDecrease = 0;
        }
        if (lifeDecrease == 2)// 보스 못 죽였을 시, 라이프 3개 감소
        {     
            Destroy(lives[LifeIndex]);
            Destroy(lives[LifeIndex + 1]);
            Destroy(lives[LifeIndex + 2]);
            LifeIndex += 3;
            lifeDecrease = 0;
        }
        GameObject life = GameObject.FindGameObjectWithTag("life");

        KillTxt.text = "KILL: " + kill.ToString();  // kill UI
        Level.text = "Level: " + levels.ToString(); // level UI

        if (life == null && levels < 40) {  // 게임 졌을 때
            WinNLose = 1;
            SceneManager.LoadScene("GameOverScene");    // GameOverScene으로 씬전환
        } else if(levels >= 40 && life != null) { // 게임 이겼을 때
            WinNLose = 2;
            SceneManager.LoadScene("GameOverScene");    // GameOverScene으로 씬전환
        }


    }
    public void DestoryAllLife()
    {
        for (int i = 0; i < lives.Length; i++)
            Destroy(lives[i]);

    }
}
