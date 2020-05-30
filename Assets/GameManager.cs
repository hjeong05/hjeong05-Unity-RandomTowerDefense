using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private static float TSpeed;

    public GameObject[] Reds;
    public GameObject[] Oranges;
    public GameObject[] Yellows;
    public GameObject[] Greens;
    public GameObject[] Purples;

    void Start()    // 게임 시작
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            // 게임 시작시 무기들 첫 데미지 초기화 
            for (int i = 0; i < (Reds.Length - 1); i++)
            {
                Reds[i].GetComponent<RedWeapon>().SetFirstDamage();
            }
            Reds[3].GetComponent<FruitRedWeapon>().SetFirstDamage();

            for (int i = 0; i < (Oranges.Length - 1); i++)
            {
                Oranges[i].GetComponent<OrangeWeapon>().SetFirstDamage();
            }
            Oranges[3].GetComponent<FruitOrangeWeapon>().SetFirstDamage();

            for (int i = 0; i < (Yellows.Length - 1); i++)
            {
                Yellows[i].GetComponent<YellowWeapon>().SetFirstDamage();
            }
            Yellows[3].GetComponent<FruitYellowWeapon>().SetFirstDamage();

            for (int i = 0; i < (Greens.Length - 1); i++)
            {
                Greens[i].GetComponent<GreenWeapon>().SetFirstDamage();
            }
            Greens[3].GetComponent<FruitGreenWeapon>().SetFirstDamage();

            for (int i = 0; i < (Purples.Length - 1); i++)
            {
                Purples[i].GetComponent<PurpleWeapon>().SetFirstDamage();
            }
            Purples[3].GetComponent<FruitPurpleWeapon>().SetFirstDamage();
        }
        instance = this;    // 인스턴스 화
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){  // 게임 종료 esc
            Application.Quit();
        }
        if (SceneManager.GetActiveScene().name == "GameScene")  // 게임 씬 내에서만 사용 되도록
        {
            if (build.instance.Select_Node != null)
            {
                if (Input.GetKeyDown(KeyCode.B))    // 무기 빌드 키보드  
                    Node.instance.OkToBuild();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))    // (텍스트 활성화)미션 확인 Admin
                PlayerManager.kill = 145;
            if (Input.GetKeyDown(KeyCode.Alpha2))    // 보스 미션 확인(보상패널 활성화 확인) Admin
                MissionManager.IsBoss = true;
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {    // 게임 클리어 확인 Admin
                PlayerManager. WinNLose = 2;
                SceneManager.LoadScene("GameOverScene");    // GameOverScene으로 씬전환
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))    // 게임 오버 확인 Admin
            {
                PlayerManager.WinNLose = 1;
                SceneManager.LoadScene("GameOverScene");    // GameOverScene으로 씬전환
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))    // 돈 증가 Admin
            {
                PlayerManager.money += 500;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))    // 토마토 증가 Admin
            {
                PlayerManager.tomato += 500;
            }
        }
    }
    public void BuildWeapon()   // build 버튼 
    {
        if (build.instance.Select_Node != null)
        {
            Node.instance.OkToBuild();
        }
    }
    public void loadMenuScene()
    {
        PlayerManager.instance.Init();
        SceneManager.LoadScene("FirstScene");
    }  // 처음 화면으로 돌아가기 
    public void GameClose()
    {
        Application.Quit();
    }   // 게임 종료
    public void PauseGame() // 게임 Stop
    {
        Time.timeScale = 0;
    }
    public void ResumeGame() // 게임 Resume
    {
        Time.timeScale = TSpeed;
    }
    public void TimeSpeed1() // 게임 1배속
    {
        TSpeed = 1f;
        Time.timeScale = TSpeed;
    }
    public void TimeSpeed1_5()  // 게임 1.5배속
    {
        TSpeed = 1.5f; 
        Time.timeScale = TSpeed;
       
    }
    public void TimeSpeed2()    // 게임 2배속
    {
        TSpeed = 2f;
        Time.timeScale = TSpeed;
    }
}
