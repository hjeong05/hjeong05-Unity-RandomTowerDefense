using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static Node instance;

    string tagName; // 태그 이름 값 저장
    public Color InitialColor; // 초기 색깔
    public Color SelectColor;   // 선택시 색깔
    public Renderer rend;   // 렌더러 함수

    build build;

    public Transform Upgrade_node;      // 업그레이드 할 노드 위치 값
    public GameObject getThisWeapon;    // 노드위 무기 저장 변수
    public GameObject Upgrade;          // 업그레이드 UI

    public bool RewardWeapon;   // 미션 성공시 받는 무기 보상확인 변수

    void Start()
    {
        RewardWeapon = false;

        rend = GetComponent<Renderer>();    // Renderer 컴포넌트 
        InitialColor = rend.material.color;
        getThisWeapon = null;

        instance = this;

        build = build.instance;
        build.before_node = this.gameObject; // 이전에 선택한 노드변수 현재 노드로 초기화
    }
    public void MouseDown() // 마우스 클릭 / 터치
    {
        change_select_node();
        if (rend.material.color != SelectColor)   // 노드가 색상 변경되지 않았다면
        {
            if (getThisWeapon != null)          // 무기가 설치되어 있다면
            {
                Upgrade_node = this.transform;  // 업그레이드 할 노드 저장
                build.SetTagName(getThisWeapon.tag); // 해당 무기 개수 확인
                build.ActiveCircle();   // 원 활성화
                build.SetNode(this);
                AudioManager.instance.PlayAudio("Panel");
                if(getThisWeapon.tag.StartsWith("4") == false)
                    Upgrade.SetActive(true);    // 업그레이드/팔기 캔버스 활성화
                else
                    Upgrade.SetActive(false);    // 업그레이드/팔기 캔버스 비활성화
                return;
            }
            build.SetNode(this);
            rend.material.color = SelectColor;  // 노드 색상 변경
            build.Select_Node = this.gameObject;    // 현 노드 저장.     
        }
        build.Select_Node = this.gameObject;   // 선택한 노드 위치 build 클래스에 전달
        build.before_node = this.gameObject;    // 선택 노드 before_node에 저장

    }
    public void OkToBuild() // 무기 설치 가능 확인
    {
        if (build.GetNode().getThisWeapon == null) {   // 현재 노드에 무기가 없고 선택되었다면      
            if (RewardWeapon) {   // 미션 보상 성공시
                Build(build.GetWeaponToBuildLv3());              
            }
            else {
                Build(build.GetWeaponToBuildLv1());
            }         
        }
        else
            Debug.Log("이미 무기가 존재합니다.");
    }   
    public void Build(GameObject weapon_)    // 초기 lv1 무기 설치 메소드
    {
        AudioManager.instance.PlayAudio("Install");
        if (PlayerManager.money < 100)  // 설치 시 돈이 부족 할 경우
        {
            build.Select_Node.GetComponent<Renderer>().material.color = InitialColor;       
            return;
        }
        PlayerManager.money -= 100;                      // 돈 무기값 빼기
        // 생성할 오브젝트의 위치, 각도에 생성
        GameObject weapon = (GameObject)Instantiate(weapon_, build.Select_Node.transform.position, Quaternion.identity);

        build.GetNode().getThisWeapon = weapon;         // 해당 노드 무기확인 값에 생성된 무기 저장
        Debug.Log("무기 설치 완료");
        RewardWeapon = false;
        build.Select_Node.GetComponent<Renderer>().material.color = InitialColor;
    }

    public void SellWeapon()
    {
        AudioManager.instance.PlayAudio("Sell");
        PlayerManager.money += 50;      // 당근 +50 보상
        // 이펙트 생성 위치 값 지정
        Vector3 pos = new Vector3(build.GetNode().transform.position.x, 
                                  build.GetNode().transform.position.y + 1f, build.GetNode().transform.position.z);
        // 팔기 이펙트 생성
        GameObject effect = (GameObject)Instantiate(build.sellEffect, pos, build.sellEffect.transform.rotation);  
        Destroy(effect, 5f);

        Destroy(getThisWeapon);
        getThisWeapon = null;       // 노드 위 무기 비우기
    }

    public void UpgradeWeapon()     // 무기 업그레이드( 합치기 )
    {
        tagName = getThisWeapon.tag; // 태그 이름 저장

        // 태그로 오브젝트 가져오고 리스트로 변환해 삭제하기
        GameObject[] weapons = GameObject.FindGameObjectsWithTag(tagName);
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < weapons.Length; i++)
        {
            list.Add(weapons[i].gameObject);
        }
        list.Remove(getThisWeapon.gameObject);  //  현재 클릭된 노드 위 무기 리스트에서 삭제
   
        if (list.Count >= 2)    // 같은 무기 2개 이상이면 ( 현 노드 무기 제외 )
        {
            AudioManager.instance.PlayAudio("Upgrade");
            DestroyThis();
            int cnt = list.Count - 1;
            int index = Random.Range(0, cnt);

            Destroy(list[index].gameObject);    // 랜덤 무기 파괴
            list.RemoveAt(index);
        }
        else if(list.Count == 1)    // 같은 무기 1개면 ( 현 노드 무기 제외 )
        {
            AudioManager.instance.PlayAudio("Upgrade");
            DestroyThis();
            Destroy(list[0].gameObject);    // 하나 마져 파괴하기
            list.RemoveAt(0);
        }
        else
            Debug.Log("합칠수 있는 무기가 없어요. ");

        build.NoActiveCircle();     // 무기 원 이미지 비활성화
    }   
    private void DestroyThis() // 노드 위 무기 파괴(업그레이드 시)
    {
        if (tagName != null)
        {
            build.SetTagName(tagName); // 태그이름 전달 
            Destroy(getThisWeapon);    //  현재 클릭된 노드 위 무기 삭제
            getThisWeapon = null;

            GameObject weapon_ = build.GetWeaponToUpgrade(); // 업그레이드할 무기 가져오기
            // 생성할 오브젝트의 위치, 각도에 생성
            GameObject weapon = (GameObject)Instantiate(weapon_, Upgrade_node.position, Quaternion.identity); 
            getThisWeapon = weapon;
        }
    }
    public void change_select_node()
    {
        if (build.before_node != this.gameObject)   // 이전 노드와 현 선택 노드가 다르면
            build.before_node.GetComponent<Renderer>().material.color = InitialColor;   // 이전 노드 색상 원래 색으로 변경
    }
}

