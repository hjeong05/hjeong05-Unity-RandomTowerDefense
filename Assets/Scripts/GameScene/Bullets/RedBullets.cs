using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullets : MonoBehaviour {

    private Transform target;   // 타깃 (적)
    public float speed = 50f;   // 총알 속도
    public GameObject impactEffect; // 이펙트
    public float damage;
    public float StartDamage;
    
    void Start()
    {
        damage = StartDamage;
    }

    public void Find(Transform target)  // 타깃 찾기
    {
        this.target = target;
    }
    public float GetDamage() {
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
            AudioManager.instance.PlayAudio("Hit");
            Destroy(gameObject);    // 총알 파괴
            return;
        }
        Vector3 dir = target.position - transform.position; // 타깃과 총알 사이 거리
        float dis = speed * Time.deltaTime;

        if (dir.magnitude <= dis) // dir.magnitude : 타깃과 총알 사이 벡터 거리
        {
            AttackTarget();
            return;
        }
        transform.Translate(dir.normalized * dis, Space.World); 
    }
    void AttackTarget()
    {
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 0.5f);
        target.GetComponent<EnemyManager>().Damage(damage); // 해당 타깃 hp 깎기
        Destroy(gameObject);
    }
}

