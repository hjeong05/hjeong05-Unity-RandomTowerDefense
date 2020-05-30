using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public Text level;  // 레벨
    public Text reward;     // 보상
	// Use this for initialization
	void Start () {
		
	}
	void OnEnable()
    {
        reward.text = PlayerManager.reward.ToString() + "획득!";
        level.text = "Level " + PlayerManager.levels.ToString();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
