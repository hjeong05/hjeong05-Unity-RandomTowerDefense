using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build : MonoBehaviour {

    public static build instance;

    public Node node;

    public Transform UpgradeNode;   // 업그레이드 시킬 노드 변수
    public GameObject weaponBuild;  

    public GameObject Select_Node;  // 현재 선택한 노드
    public GameObject before_node;  // 이전에 선택한 노드

    private GameObject[] weaponsLv1 = new GameObject[5];
    public GameObject red_1;
    public GameObject orange_1;
    public GameObject yellow_1;
    public GameObject green_1;
    public GameObject purple_1;

    private GameObject[] weaponsLv2 = new GameObject[5];
    public GameObject red_2;
    public GameObject orange_2;
    public GameObject yellow_2;
    public GameObject green_2;
    public GameObject purple_2;

    private GameObject[] weaponsLv3 = new GameObject[5];
    public GameObject red_3;
    public GameObject orange_3;
    public GameObject yellow_3;
    public GameObject green_3;
    public GameObject purple_3;

    private GameObject[] weaponsLv4 = new GameObject[5];
    public GameObject red_4;
    public GameObject orange_4;
    public GameObject yellow_4;
    public GameObject green_4;
    public GameObject purple_4;

    private string tagName;

    public GameObject sellEffect;
    void Start()
    {
        setWeapons();
        weaponBuild = null;
    }

    public void SetNode(Node node)
    {
        this.node = node;
    }
    public Node GetNode()
    {
        return node;
    }
    void setWeapons()
    {
        weaponsLv1[0] = red_1;
        weaponsLv1[1] = orange_1;
        weaponsLv1[2] = yellow_1;
        weaponsLv1[3] = green_1;
        weaponsLv1[4] = purple_1;

        weaponsLv2[0] = red_2;
        weaponsLv2[1] = orange_2;
        weaponsLv2[2] = yellow_2;
        weaponsLv2[3] = green_2;
        weaponsLv2[4] = purple_2;

        weaponsLv3[0] = red_3;
        weaponsLv3[1] = orange_3;
        weaponsLv3[2] = yellow_3;
        weaponsLv3[3] = green_3;
        weaponsLv3[4] = purple_3;

        weaponsLv4[0] = red_4;
        weaponsLv4[1] = orange_4;
        weaponsLv4[2] = yellow_4;
        weaponsLv4[3] = green_4;
        weaponsLv4[4] = purple_4;

    }
    
    public void SetTagName(string tagName) {
        this.tagName = tagName;      
    }   // 태그 이름 가져오기
    public string GetTagName()
    {
        return tagName;
    }   // 태그이름 전달하기

    public int checkNumTag()    // 태그된 오브젝트 개수 리턴
    {
        int TagNum = 0;
        if (tagName != null)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName);
            TagNum = objects.Length;
        }
        return TagNum;
    }
    public void NoActiveCircle()    // 원 비활성화
    {
        if (tagName != null)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName);
            for (int i = 0; i < objects.Length; i++)
            { 
                // 원을 가지고 있는 무기만 ( 열매 무기는 원이 없어도 됨 -> 업그레이드 할 수 없기 때문 )
                if (objects[i].gameObject.GetComponent<WeaponCircle>() != null)  
                    objects[i].gameObject.GetComponent<WeaponCircle>().circle.SetActive(false);
            }
        }
    }
    public void ActiveCircle()      // 원 활성화
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName);
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].gameObject.GetComponent<WeaponCircle>() != null)
                objects[i].gameObject.GetComponent<WeaponCircle>().circle.SetActive(true);
        }
    }
    public GameObject GetWeaponToUpgrade()  
    {
        int PrefabIndex = Random.Range(0, 5);
        if (tagName.StartsWith("1"))    // 새싹 무기
        {
            weaponBuild = weaponsLv2[PrefabIndex];
        }
        if (tagName.StartsWith("2"))    // 꽃 무기
        {
            weaponBuild = weaponsLv3[PrefabIndex];
        }
        if (tagName.StartsWith("3"))    // 나무 무기 
        {
            weaponBuild = weaponsLv4[PrefabIndex];
        }
        return weaponBuild;
    }
    public GameObject GetWeaponToBuildLv1()     // 새싹무기 랜덤 설정 메소드
    {
        int PrefabIndex = Random.Range(0, 5);
        weaponBuild = weaponsLv1[PrefabIndex];
        return weaponBuild;
    }

    // 미션 보상시 레벨3 무기 랜덤 설치 될 무기 리턴하는 메소드
    public GameObject GetWeaponToBuildLv3() 
    {
        int PrefabIndex = Random.Range(0, 5);
        weaponBuild = weaponsLv3[PrefabIndex];
        return weaponBuild;
    }
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }
    public void clear_node()    // 변경된 노드 색상 원래 색으로 초기화
    {
        Select_Node.GetComponent<Renderer>().material.color = Select_Node.GetComponent<Node>().InitialColor;  
    }
}
