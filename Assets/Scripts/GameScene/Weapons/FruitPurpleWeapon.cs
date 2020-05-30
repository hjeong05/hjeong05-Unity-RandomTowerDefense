using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPurpleWeapon : MonoBehaviour {
    private Transform target;
    public float range;
    public string enemyTag = "Enemy";

    private float rotSpeed = 10f;
    public Transform Rotate;

    private float fireTerm = 2.5f;
    private float firstTerm;

    public GameObject bulletPrefab;
    public Transform firePoint1;


    public bool IsDestroy = false;
    public void SetFirstDamage()
    {
      bulletPrefab.GetComponent<FruitPurpleBullets>().damage = bulletPrefab.GetComponent<FruitPurpleBullets>().StartDamage;
    }

    void Start()
    {
  
        float up = bulletPrefab.GetComponent<FruitPurpleBullets>().GetDamage();      // 강화시 데미지 업 
        bulletPrefab.GetComponent<FruitPurpleBullets>().SetDamage(up);

        IsDestroy = false;
        firstTerm = fireTerm;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");  // Enemy라고 태그되어있는 오브젝트 enemies 배열에 넣기

        float closestDistance = 10000f;
        GameObject closestEnemy = null;

        foreach (GameObject i in enemies)    // 반복문 foreach
        {
            float EnemyWeaponDistance = Vector3.Distance(transform.position, i.transform.position); // 적과 무기간 거리 구하기
            if (EnemyWeaponDistance < closestDistance)   // 적과 무기간 거리가 가장 짧은거리보다 작으면
            {
                closestDistance = EnemyWeaponDistance;   // 짧은거리 변경
                closestEnemy = i;       // 가장 가까운 적은 i
            }
        }
        if (closestEnemy != null && closestDistance <= range) // 가까운 적 존재하고 가장 가까운거리가 무기의 범위 내일 경우,
        {
            target = closestEnemy.transform; // 타겟은 가까운 적 위치
        }
        else
        {
            target = null;
        }
    }

    public void UpDamage()
    {
        float up = bulletPrefab.GetComponent<FruitPurpleBullets>().GetDamage() + 2f;
        bulletPrefab.GetComponent<FruitPurpleBullets>().SetDamage(up);
        Debug.Log(this.gameObject.name+"보라색 무기 중간에 강화 데미지: " + up);
    }
    void Update()
    {
        if (IsDestroy == true)
        {
            Destroy(this.gameObject);
            Debug.Log("파괴 완료 ");
            IsDestroy = false;
        }
        if (target == null)
        {
            return;
        }
        roateTotarget();

        if (fireTerm <= 0f)
        {
            Shoot();
            fireTerm = firstTerm;
        }
        fireTerm -= Time.deltaTime;

    }
    void roateTotarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(Rotate.rotation, lookRotation, Time.deltaTime * rotSpeed).eulerAngles;
        Rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        Debug.Log(this.gameObject.tag);
        GameObject bulletSpawn1 = (GameObject)Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);

        FruitPurpleBullets bullet = bulletSpawn1.GetComponent<FruitPurpleBullets>();

        if (bullet != null)
        {
            bullet.firstPoint(firePoint1);
            bullet.Find(target);
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
