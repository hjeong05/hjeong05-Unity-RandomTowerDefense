using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour {

    public Text CarrotText;
    public Text TomatoText;

	// Update is called once per frame
	void Update () {
        CarrotText.text = PlayerManager.money.ToString();
        TomatoText.text = PlayerManager.tomato.ToString();

    }
}
