using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SpawnEnemy : MonoBehaviour {

    public Transform[] Candies = new Transform[10]; // 1~10 단계 적
    public Transform[] Chocos = new Transform[2]; // 11~20 단계 적
    public Transform[] Chips = new Transform[2]; // 21~30 단계 적
    public Transform[] Burgers = new Transform[2]; // 21~30 단계 적

    public static int EnemyNum;  // 적 스폰 개수
    public Transform spawnpoint;    // 스폰 위치
    public Text countdown;  // 준비시간
    public Text Level;  // 레벨 ( 웨이브 )
    public Button startBtn; // 시작 버튼

    private int i; // 적 인덱스 
    private int j; // 적 캔디 스폰시킬때 배열 인덱스
    private float ReadyT;
    private float FirstReadyTime = 10f;  // 준비시간

    private bool Isvalue;   // 보상 제어 변수
    private float startHP = 10f; // 웨이브 별 적 초기 체력 
    private float plusHP = 10f;  // 웨이브 별 적 체력 증가 

    void Start()
    {
        Isvalue = false;
        ReadyT = FirstReadyTime;
        
        i = 0;  // 적 배열 인덱스

        EnemyNum = 15;  // 적 개수   

    }
    void Update()
    {
        GameObject checkEnemies = GameObject.FindGameObjectWithTag("Enemy");

        if (checkEnemies == null) {               // 적이 없다면
            PlayerManager.IsReady = true;
            nextlevel();    // 다음 단계 시작
        }
        if (PlayerManager.IsReady)
        {
            countdown.gameObject.SetActive(true);
            ReadyT -= Time.deltaTime;
            countdown.text = "준비시간: " + ReadyT.ToString("F0");
            if (ReadyT <= 0) {      // 준비시간이 끝났으면
                if (PlayerManager.IsNext == false) {
                    PlayerManager.IsNext = true;
                }
                countdown.gameObject.SetActive(false);
                StartCoroutine(Spawn()); // 코루틴 사용, 적 스폰하기
                ReadyT = FirstReadyTime;
                PlayerManager.IsReady = false;
            }
        }
    }
    void startgame()
    {
        PlayerManager.IsReady = true;
    }
    public void retry()     // 다시하기 버튼
    {
        startgame();
    }
    void nextlevel()     // 다음 레벨 버튼
    {
        if (PlayerManager.IsNext) {
            PlayerManager.levels++;
                Isvalue = true;     // 보상 제어 변수
                if (Isvalue)
                {
                    PlayerManager.money += 300;
                    Isvalue = false;
                }   // 보상 제공 
            PlayerManager.IsNext = false;
        }      
    }
    IEnumerator Spawn()
    {
        if (PlayerManager.levels == 10 || PlayerManager.levels == 20 || 
            PlayerManager.levels == 30 || PlayerManager.levels == 40)
        {
            EnemyNum = 1;
        }
        else
        {
            EnemyNum = 15;
        }
        for (int i = 0; i < EnemyNum; i++)
        {
            EnemySpawn();
            yield return new WaitForSeconds(0.5f);    // 1초 기다렸다가 다시 for문 실행
        }
        i++;
        plusHP += 5f;   // 적 체력 증가
        startHP = startHP + plusHP;
    }
    void EnemySpawn()   // 적 생성하기 
    {
        if (PlayerManager.levels <= 9)
        {
            Candies[i].GetComponent<EnemyManager>().Starthp = startHP;
            Instantiate(Candies[i], spawnpoint.position, spawnpoint.rotation);
        }
        else if (PlayerManager.levels == 10)
        {
            Candies[9].GetComponent<EnemyManager>().Starthp = 700f;
            Instantiate(Candies[9], spawnpoint.position, spawnpoint.rotation);

            startHP = 400f;
            plusHP = 0f;
        }
        else if (PlayerManager.levels <= 19 && PlayerManager.levels > 10)
        {
            Chocos[0].GetComponent<EnemyManager>().Starthp = startHP;
            Instantiate(Chocos[0], spawnpoint.position, spawnpoint.rotation);
        }
        else if (PlayerManager.levels == 20)
        {
            Chocos[1].GetComponent<EnemyManager>().Starthp = 1000f;  
            Instantiate(Chocos[1], spawnpoint.position, spawnpoint.rotation);

            startHP = 600f;
            plusHP = 0f;
        }
        else if (PlayerManager.levels <= 29 && PlayerManager.levels > 20)
        {
            Chips[0].GetComponent<EnemyManager>().Starthp = startHP;
            Instantiate(Chips[0], spawnpoint.position, spawnpoint.rotation);
        }
        else if (PlayerManager.levels == 30)
        {
            Chips[1].GetComponent<EnemyManager>().Starthp = 2000f;
            Instantiate(Chips[1], spawnpoint.position, spawnpoint.rotation);

            startHP = 800f;
            plusHP = 0f;
        }
        else if (PlayerManager.levels <= 39 && PlayerManager.levels > 30)
        {
            Burgers[0].GetComponent<EnemyManager>().Starthp = startHP;
            Instantiate(Burgers[0], spawnpoint.position, spawnpoint.rotation);
        }
        else if (PlayerManager.levels == 40)
        {
            Burgers[1].GetComponent<EnemyManager>().Starthp = 3000f;
            Instantiate(Burgers[1], spawnpoint.position, Burgers[1].rotation);
        }
    }
}


