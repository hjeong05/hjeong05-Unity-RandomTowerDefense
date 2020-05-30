using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpPanelUI : MonoBehaviour {
    // 타워 별 강화 버튼 오브젝트 
    public GameObject RedBtn;
    public GameObject OrangeBtn;
    public GameObject YellowBtn;
    public GameObject GreenBtn;
    public GameObject PurpleBtn;

    public Text TowerName;  // 해당 타워 이름 텍스트
    public Text Uplevel;    // 강화 레벨 텍스트
    public Text TomatoTxt;  // 토마토 개수 텍스트

    // 타워 별 레벨 구분 변수
    private int redLevel;
    private int OrangeLevel;
    private int YellowLevel;
    private int GreenLevel;
    private int PurpleLevel;

    // 강화시 토마토 사용 량 변수
    private int RedTomatoUse;
    private int OrangeTomatoUse;
    private int YellowTomatoUse;
    private int GreenTomatoUse;
    private int PurpleTomatoUse;

    // Use this for initialization
    void Start()
    {
        RedBtn.SetActive(false);
        OrangeBtn.SetActive(false);
        YellowBtn.SetActive(false);
        GreenBtn.SetActive(false);
        PurpleBtn.SetActive(false);

        RedTomatoUse = 10;
        OrangeTomatoUse = 10;
        YellowTomatoUse = 10;
        GreenTomatoUse = 10;
        PurpleTomatoUse = 10;

    }

    // Update is called once per frame
    void Update () {
        TowerName.text = TowerManager.TName.ToString();
        TowerManager.TName = TowerName.text;

        // 클릭된 타워이름 별 텍스트 변환
        if (TowerManager.TName.Equals("Red Tower"))
        {
            if (PlayerManager.tomato < RedTomatoUse || redLevel >= 15 )
            {
                AllNoActiveBtn();
            }
            else
            {
                RedBtn.SetActive(true);
                OrangeBtn.SetActive(false);
                YellowBtn.SetActive(false);
                GreenBtn.SetActive(false);
                PurpleBtn.SetActive(false);
            }
            Uplevel.text = redLevel.ToString();
            TomatoTxt.text = RedTomatoUse.ToString();
        }
        else if (TowerManager.TName.Equals("Orange Tower"))
        {
            if (PlayerManager.tomato < OrangeTomatoUse || OrangeLevel >= 15)
            {
                AllNoActiveBtn();
            }
            else
            {
                RedBtn.SetActive(false);
                OrangeBtn.SetActive(true);
                YellowBtn.SetActive(false);
                GreenBtn.SetActive(false);
                PurpleBtn.SetActive(false);
            }
            Uplevel.text = OrangeLevel.ToString();
            TomatoTxt.text = OrangeTomatoUse.ToString();
        }
        else if (TowerManager.TName.Equals("Yellow Tower"))
        {
            if (PlayerManager.tomato < YellowTomatoUse || YellowLevel >= 15)
            {
                AllNoActiveBtn();
            }
            else
            {
                RedBtn.SetActive(false);
                OrangeBtn.SetActive(false);
                YellowBtn.SetActive(true);
                GreenBtn.SetActive(false);
                PurpleBtn.SetActive(false);
            }
            Uplevel.text = YellowLevel.ToString();
            TomatoTxt.text = YellowTomatoUse.ToString();
        }
        else if (TowerManager.TName.Equals("Green Tower"))
        {
            if (PlayerManager.tomato < GreenTomatoUse || GreenLevel >= 15)
            {
                AllNoActiveBtn();
            }
            else
            {
                RedBtn.SetActive(false);
                OrangeBtn.SetActive(false);
                YellowBtn.SetActive(false);
                GreenBtn.SetActive(true);
                PurpleBtn.SetActive(false);
            }
            Uplevel.text = GreenLevel.ToString();
            TomatoTxt.text = GreenTomatoUse.ToString();
        }
        else if (TowerManager.TName.Equals("Purple Tower"))
        {
            if (PlayerManager.tomato < PurpleTomatoUse || PurpleLevel >= 15) 
            {
                AllNoActiveBtn();
            }
            else
            {
                RedBtn.SetActive(false);
                OrangeBtn.SetActive(false);
                YellowBtn.SetActive(false);
                GreenBtn.SetActive(false);
                PurpleBtn.SetActive(true);
            }
            Uplevel.text = PurpleLevel.ToString();
            TomatoTxt.text = PurpleTomatoUse.ToString();
        }
    }
    public void WeaponReinforce()   // 색깔별 무기 강화 메소드 
    {
        if (TowerManager.TName.Equals("Red Tower"))
        {
            if (PlayerManager.tomato >= RedTomatoUse && redLevel < 16) {
                PlayerManager.tomato -= RedTomatoUse;
                Debug.Log("RedTower강화클릭");
                redLevel += 1;
                RedTomatoUse += 1;
            }
            else {
                Debug.Log("토마토 부족!!");
            }
        }
        if (TowerManager.TName.Equals("Orange Tower"))
        {
            if (PlayerManager.tomato >= OrangeTomatoUse && OrangeLevel < 16)
            {
                PlayerManager.tomato -= OrangeTomatoUse;
                Debug.Log("Orange강화클릭");
                OrangeLevel += 1;              
                OrangeTomatoUse += 1;
            }
            else
            {
                OrangeBtn.SetActive(false);
                Debug.Log("토마토 부족!!");
            }
        }
        if (TowerManager.TName.Equals("Yellow Tower"))
        {
            if (PlayerManager.tomato >= YellowTomatoUse && YellowLevel < 16)
            {
                PlayerManager.tomato -= YellowTomatoUse;
                Debug.Log("Yellow강화클릭");
                YellowLevel += 1;               
                YellowTomatoUse += 1;
            }
            else
            {
                Debug.Log("토마토 부족!!");
            }
        }
        if (TowerManager.TName.Equals("Green Tower"))
        {
            if (PlayerManager.tomato >= GreenTomatoUse && GreenLevel < 16)
            {
                PlayerManager.tomato -= GreenTomatoUse;
                Debug.Log("Green강화클릭");
                GreenLevel += 1;                
                GreenTomatoUse += 1;
            }
            else
            {
                GreenBtn.SetActive(false);
                Debug.Log("토마토 부족!!");
            }
        }
        if (TowerManager.TName.Equals("Purple Tower"))
        {
            if (PlayerManager.tomato >= PurpleTomatoUse && PurpleLevel < 16)
            {
                PlayerManager.tomato -= PurpleTomatoUse;
                Debug.Log("Purple강화클릭");
                PurpleLevel += 1;            
                PurpleTomatoUse += 1;
            }
            else
            {
                Debug.Log("토마토 부족!!");
            }
        }
    }
    void AllNoActiveBtn()
    {
        RedBtn.SetActive(false);
        OrangeBtn.SetActive(false);
        YellowBtn.SetActive(false);
        GreenBtn.SetActive(false);
        PurpleBtn.SetActive(false);
    }   // 버튼 전체 비활성화 
    public void NoUpgrade() // 업그레이드 취소시 실행 
    {
        build.instance.NoActiveCircle();
    }
    public void Sell()  // 무기 팔기 버튼 클릭시 실행
    {
        build.instance.NoActiveCircle();
        build.instance.GetNode().getThisWeapon.GetComponent<WeaponCircle>().circle.SetActive(true);
    }
    public void SellOk()    // 무기 팔기 확인 버튼 클릭시 실행
    {
        AudioManager.instance.PlayAudio("Panel");
        build.instance.GetNode().SellWeapon();
    }
}
