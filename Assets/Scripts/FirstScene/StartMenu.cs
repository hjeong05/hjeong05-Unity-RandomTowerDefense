using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
    public GameObject[] texts;

    void Start()
    {
        texts[0].SetActive(true);
        texts[1].SetActive(false);
    }
    public void RightBtn()  // 오른쪽 화살표
    {
        texts[1].SetActive(true);
        texts[0].SetActive(false);
    }
    public void LeftBtn()   // 왼쪽 화살표 
    {
        texts[0].SetActive(true);
        texts[1].SetActive(false);
    }
    public void loadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
