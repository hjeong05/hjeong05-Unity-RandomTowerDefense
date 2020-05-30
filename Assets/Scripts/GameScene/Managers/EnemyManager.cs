using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour { 
    // Hp 관련 변수
    public float hp;
    public float Starthp;
    public Image HpBar;

    public float Speed; // 적 이동 스피드
    private Transform WayPoint;     // 길 포인트
    private int WayPointIndex = 0;  // 길포인트 인덱스

    public GameObject deathEffect;  // 적 이펙트

    void Start()
    {
        Debug.Log(this.gameObject.name);
           hp = Starthp;
        Debug.Log("초기 적hp: " + hp);
        WayPoint = Waypoints.points[0]; // 첫번째 points배열로 초기화
    }
    void Update()
    {
        Vector3 dir = WayPoint.position - transform.position; // Waypoint와 적 사이의 거리
        transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);  // 속도 거리 이동
        if(Vector3.Distance(transform.position,WayPoint.position) <= 0.6f)  // 게임오브젝트가 포인트에 다 다르면
        {
            GetNextWayPoint();  // 다음 포인트 가져오기
            transform.Rotate(0, 90, 0); // 꺾이는 표현 위해 90도 회전
        }
    }
    public void Damage(float amount)
    {
        hp -= amount;
        HpBar.fillAmount = hp / Starthp ;
        if(hp<= 0)
        {
            if (PlayerManager.levels % 10 == 0)
                MissionManager.IsBoss = true;
            AudioManager.instance.PlayAudio("Die");
            Destroy(this.gameObject);
            Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
            PlayerManager.kill += 1;
        }
    }
    void GetNextWayPoint()
    {
        if(WayPointIndex >= Waypoints.points.Length - 1)    // 마지막 포인트에 도달시
        {

            if (this.gameObject.name.StartsWith("Boss"))
                PlayerManager.lifeDecrease = 2;
            else
                PlayerManager.lifeDecrease = 1; // 라이프 파괴시키기 위한 제어 변수 

            Destroy(gameObject);    // 오브젝트 파괴
            return;
        }
        WayPointIndex++;    // 인덱스 +1 
        WayPoint = Waypoints.points[WayPointIndex];
    }
}
