using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;


public class TowerManager : MonoBehaviour {

    public GameObject UpPanel;
    public GameObject TomatoPanel;

    public static string TName; // 타워 이름 

    void Start()
    {
        UpPanel.SetActive(false);
        TomatoPanel.SetActive(false);
    }

    private void OnMouseDown()  // 마우스 타워 클릭 메소드
    {
        Debug.Log( this.gameObject.name + "클릭됨");
        
        TName = this.gameObject.name;   // 타워이름 저장
        if (TName == "TOMATO") { // 토마토 클릭했을 경우  
            TomatoPanel.SetActive(true);    // 토마토 패널 활성화
            UpPanel.SetActive(false);
        }
        else {
            UpPanel.SetActive(true);    // 업패널 활성화
            TomatoPanel.SetActive(false);
        }
    }
    public void ChangeToTomato()    // 토마토로 변환 
    {
        PlayerManager.money -= 100;
        int[] tomatoNum = { 10, 10, 20, 20, 30, 30, 40, 40, 50, 50, 60, 70, 80, 90, 100 };
        int i = Random.Range(0, tomatoNum.Length);
        Debug.Log(tomatoNum[i] + " 토마토 변환");
        PlayerManager.tomato += tomatoNum[i];

        if(tomatoNum[i] == 100)
        {
            MissionManager.IsDice = true;
        }
    }
}
