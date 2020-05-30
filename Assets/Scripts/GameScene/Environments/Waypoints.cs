using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static Transform[] points;   // 위치 배열 생성

    void Awake()
    {
        //길 꺾이는 부분 세서 부분마다 위치 변경
        points = new Transform[transform.childCount];   // 자기 자식의 수로 배열크기 설정
        for(int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);  // 자식을 배열에 하나씩 가져오기
        }
    }
}
