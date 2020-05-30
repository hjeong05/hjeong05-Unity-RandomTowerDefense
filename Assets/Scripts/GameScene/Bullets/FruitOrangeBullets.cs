using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitOrangeBullets : MonoBehaviour {
    private Transform point;    // 총알 시작 포인트
    private Transform target;   // 타겟
    private float moveTime = 0.5f;// 총알 이동 시간 
    private float startTime;
    public float damage;
    public float StartDamage;
    public GameObject impactEffect; // 이펙트
    void Start()
    {
        damage = StartDamage;
        startTime = Time.time;
    }
    public void Find(Transform target)  // 타깃 찾기
    {
        this.target = target;
    }
    public void firstPoint(Transform point) // 총알 시작 포인트 
    {
        this.point = point;
    }
    public float GetDamage()
    {
        return damage;
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    void Update()
    {
        if (target == null)  // 타깃 없으면
        {
            Destroy(this.gameObject);    // 총알 파괴
            return;
        }
        else
        {
            Debug.Log(target);
            Vector3 center = (point.position + target.position) * 0.5F;
            center -= new Vector3(0, 1, 0);
            Vector3 riseRelCenter = point.position - center;
            Vector3 setRelCenter = target.position - center;
            float fracComplete = (Time.time - startTime) / moveTime;

            Vector3 dir = target.position - transform.position; // 타깃과 총알 사이 거리
            if (dir.magnitude <= fracComplete)
            {
                AttackTarget();
                return;
            }

            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position += center;
        }
    }
    void AttackTarget()
    {
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 0.5f);
        target.GetComponent<EnemyManager>().Damage(damage); // 해당 타깃 hp 깎기
        Destroy(gameObject);
    }
}
