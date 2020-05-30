using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour {

    public Text MissionTxt;
    public GameObject RewardPanel;  // 보상 선택 패널
    public Text Tomato;
    public Text Carrot;
    public GameObject Weapon;
    private int TomatoReward;   // 토마토 보상 변수
    private int CarrotReward;   // 당근 보상 변수

    // 미션 제어 부울 변수
    private bool AllSprout;
    private bool IsApple;
    private bool IsFlower;
    private bool IsKill1;
    private bool IsKill2;
    public static bool IsDice;
    public static bool IsBoss;
    private bool IsTropical;
    public static bool IsWeaponReward;  // 보상 제어 변수
 
    void Start () {
        TomatoReward = 0;
        CarrotReward = 0;
        AllSprout = true;
        IsApple = true;
        IsFlower = true;
        IsKill1 = true;
        IsKill2 = true;
        IsDice = false;
        IsBoss = false;
        IsTropical = true;
    }   // 변수들 초기화

    void Update()
    {
        if (MissionTxt.gameObject.GetComponent<Text>().color.a == 0)
        {  // 텍스트 알파값이 0이 되면 
            MissionTxt.gameObject.SetActive(false);     // 텍스트 비활성화 
            MissionTxt.GetComponent<MissionText>().boolcheck(); // 텍스트 소리 부울함수
        }
        Tomato.text = TomatoReward.ToString();
        Carrot.text = CarrotReward.ToString();

        BossMission();
        SproutMission();
        TropicalMission();
        AppleMission();
        FlowerMission();
        DiceMission();
        KillMission();

    }
    void BossMission()  // 보스 미션
    {
        if (IsBoss)    // 보스를 죽이면
        {
            MissionTxt.gameObject.SetActive(true);
            RewardPanel.SetActive(true);
            AudioManager.instance.PlayAudio("Success");
            TomatoReward = 100;
            CarrotReward = 200;
            Weapon.SetActive(true);
            MissionTxt.text = "보스단계 클리어!";
            IsBoss = false;
        }
    } 
    void SproutMission()    // 새싹 무기 보두 보유 미션
    {
        GameObject Red1 = GameObject.FindGameObjectWithTag("1-Red");
        GameObject Orange1 = GameObject.FindGameObjectWithTag("1-Orange");
        GameObject Yellow1 = GameObject.FindGameObjectWithTag("1-Yellow");
        GameObject Green1 = GameObject.FindGameObjectWithTag("1-Green");
        GameObject Purple1 = GameObject.FindGameObjectWithTag("1-Purple");

        if (Red1 && Orange1 && Yellow1 && Green1 && Purple1 && AllSprout)
        {
            AudioManager.instance.PlayAudio("Success");
            MissionTxt.gameObject.SetActive(true);
            RewardPanel.SetActive(true);
            TomatoReward = 70;
            CarrotReward = 200;
            Weapon.SetActive(true);
            Debug.Log("새싹 레벨 무기 모두 보유 미션성공");
            MissionTxt.text = "새싹 레벨 무기 모두 보유 미션성공!";
            AllSprout = false;
        }
    }
    void AppleMission() // 아침에 사과 미션
    {
        GameObject RedApple = GameObject.FindGameObjectWithTag("3-Red");
        GameObject GreenApple = GameObject.FindGameObjectWithTag("3-Green");
        if (RedApple && GreenApple && IsApple)
        {
            AudioManager.instance.PlayAudio("Success");

            MissionTxt.gameObject.SetActive(true);
            RewardPanel.SetActive(true);
            TomatoReward = 80;
            CarrotReward = 200;
            Weapon.SetActive(true);
            Debug.Log("아침에 사과 미션성공 ");
            MissionTxt.text = "아침에 사과 미션성공 !";
            IsApple = false;
        }
    }
    void TropicalMission()  // 열대과일이 좋아 미션
    {
        GameObject Mango = GameObject.FindGameObjectWithTag("2-Yellow");
        GameObject Pineapple = GameObject.FindGameObjectWithTag("4-Yellow");
        if (Mango && Pineapple && IsTropical) //열대과일이 좋아 : 망고, 파인애플 무기 보유
        {
            MissionTxt.gameObject.SetActive(true);
            RewardPanel.SetActive(true);
            TomatoReward = 80;
            CarrotReward = 200;
            Weapon.SetActive(true);
            Debug.Log("열대과일이 좋아! ");
            MissionTxt.text = "열대과일이 좋아! 미션성공 !";
            IsTropical = false;
        }
    }
    void FlowerMission()    // 꽃 한 송이 미션
    {
        GameObject RedFlower = GameObject.FindGameObjectWithTag("2-Red");
        GameObject OrangeFlower = GameObject.FindGameObjectWithTag("2-Orange");
        if (RedFlower && OrangeFlower && IsFlower) // 꽃 레벨 빨간색, 주황색 1개씩 보유
        {
            AudioManager.instance.PlayAudio("Success");
            TomatoReward = 80;
            CarrotReward = 300;
            Weapon.SetActive(false);
            MissionTxt.gameObject.SetActive(true);
            RewardPanel.SetActive(true);
            Debug.Log("꽃 한송이 미션성공 ");
            MissionTxt.text = "꽃 한송이 미션성공 !";
            IsFlower = false;
        }
    }
    void DiceMission()  // 토마토 변환 운 미션
    {
        if (IsDice) {
            PlayerManager.money += 200; //보상
            AudioManager.instance.PlayAudio("Success");
            MissionTxt.gameObject.SetActive(true);
            Weapon.SetActive(false);
            MissionTxt.text = "I'm lucky Guy!! 미션성공! - 보상 200 당근";
            IsDice = false;
        }
    }
    void KillMission()  // 적 킬 수 미션
    {
        if (PlayerManager.kill >= 150 && IsKill1) //- 150킬 이상      
        {
            AudioManager.instance.PlayAudio("Success");
            MissionTxt.gameObject.SetActive(true);
            Weapon.SetActive(false);
            PlayerManager.money += 300; //보상
            MissionTxt.text = "그대는 중급자! 미션성공! - 보상 300 당근";
            IsKill1 = false;
            if (PlayerManager.kill >= 400 && IsKill2) //- 400킬 이상      
            {
                AudioManager.instance.PlayAudio("Success");
                MissionTxt.gameObject.SetActive(true);
                Weapon.SetActive(false);
                PlayerManager.money += 500; //보상
                MissionTxt.text = "그대는 고수! 미션성공! - 보상 500 당근";
                IsKill2 = false;
            }
        }
    }
    public void ClickTomato()   // 보상을 토마토로 선택하였을 때 실행
    {
        PlayerManager.tomato += TomatoReward;
        Debug.Log("토마토 보상 : " + TomatoReward);
    }
    public void ClickCarrot()   // 보상을 당근으로 선택했을 때 실행
    {
        PlayerManager.money += CarrotReward;
        Debug.Log("당근 보상 : " + CarrotReward);
    }
    public void ClickWeapon()   // 보상을 무기로 선택했을 때 실행
    {
        Node.instance.RewardWeapon = true;
        MissionTxt.gameObject.SetActive(true);
        MissionTxt.text = "다음 설치시, '나무'레벨 무기가 설치됩니다. ";
    }
}
