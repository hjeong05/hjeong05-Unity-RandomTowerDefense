using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UpgradePanel에 위치
public class WeaponCount : MonoBehaviour {

    build _build;
    public Text CntTxt;     // 개수 텍스트

    void Start()
    {
        _build = build.instance;
    }
    public void Upgrade()
    {
        _build.GetNode().UpgradeWeapon();
    }
    // Update is called once per frame
    void Update()
    {
        int cnt = build.instance.checkNumTag();
        CntTxt.text = build.instance.GetTagName() + "현재 보유 개수 : " + cnt.ToString();
    }
}
