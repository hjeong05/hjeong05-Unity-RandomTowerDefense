using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWeapon : MonoBehaviour {
    private float damage = 2; // 강화클릭 때마다 더해주는 데미지 변수 값
    private float rotSpeed = 10f;   // 회전 속도
    public Transform Rotate;    // 회전하는 부분 ( 무기 Head부분 )

    private float fireTerm = 0.5f;  // 총알 발사 간격 
    private float firstTerm;        

    public GameObject bulletPrefab;     // 총알 프리팹
    public Transform firePoint1;         // 발사위치1
    public Transform firePoint2;        // 발사위치 2

    public bool IsDestroy = false;  // 오브젝트 파괴 제어 변수
    public GameObject circle;       // 개수 표현시 사용될 원 오브젝트

    private Transform target;
    public float range;
    public string enemyTag = "Enemy";

    void Start()
    {     
        float up = bulletPrefab.GetComponent<RedBullets>().GetDamage();      // 강화시 데미지 업 
        bulletPrefab.GetComponent<RedBullets>().SetDamage(up);
        Debug.Log(this.gameObject.name + " 빨간색 무기 시작 데미지: " + up);
        IsDestroy = false;
        firstTerm = fireTerm;
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // 반복함수("함수명","최초호출대기시간","반복호출대기시간")
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");  // Enemy라고 태그되어있는 오브젝트 enemies 배열에 넣기

        float closestDistance = 10000f;     // 무한적인 수로 초기화
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
        else {
            target = null;
        }
    }
    void Shoot()
    {
        GameObject bulletSpawn1 = (GameObject)Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);

        if (firePoint2 != null)
        {
            GameObject bulletSpawn2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
            RedBullets bullet2 = bulletSpawn2.GetComponent<RedBullets>();

            if (bullet2 != null)
                bullet2.Find(target);
        }
        RedBullets bullet = bulletSpawn1.GetComponent<RedBullets>();

        if (bullet != null)
            bullet.Find(target);
    }
    public void SetFirstDamage()  // 데미지 초기화 메소드 
    {
        bulletPrefab.GetComponent<RedBullets>().damage = bulletPrefab.GetComponent<RedBullets>().StartDamage;
    }
    public void UpDamage()      // 데미지 강화 메소드
    {
        // damage 변수 값만큼 더하고 총알 이펙트에 저장
        float up = bulletPrefab.GetComponent<RedBullets>().GetDamage() + damage;
        bulletPrefab.GetComponent<RedBullets>().SetDamage(up);
        Debug.Log(this.gameObject.name + "빨강 무기 강화 데미지: " + up);
    }
    void Update()
    {
        if (IsDestroy == true)
        {
            Destroy(this.gameObject);
            Debug.Log("파괴 완료 ");
            IsDestroy = false;
        }
        if (target == null) {
            return;
        }
        roateTotarget();

        if (fireTerm <= 0f) {
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
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

