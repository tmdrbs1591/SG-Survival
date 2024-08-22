using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roket : MonoBehaviour
{
    public float speed = 5f;  // 일정한 이동 속도
    public float curveStrength = 2f;  // 곡선의 강도 (제어점의 이동 거리)
    public float minStartDelay = 0f;  // 시작 지연의 최소 값 (초)
    public float maxStartDelay = 0.5f;  // 시작 지연의 최대 값 (초)
    public float xp; // 줄 경험치

    [SerializeField] GameObject BoomEffect;

    private Transform enemy;  // 몬스터의 Transform
    private Vector3 startPoint;
    private Vector3 controlPoint;
    private Vector3 endPoint;
    private float t = 0f;  // 시간 변수
    private float startTime;  // 시작 시간
    private bool useCurve = true;  // 곡선 사용 여부

    void Start()
    {
        FindClosestEnemy(); // 가장 가까운 적을 찾아서 설정

        // 시작점 초기화
        startPoint = transform.position;

        if (enemy != null)
        {
            endPoint = enemy.position;
            SetRandomControlPoint();  // 랜덤 제어점 설정
        }
        else
        {
            // 목표가 없을 경우 직선으로 쭉 가도록 설정
            endPoint = transform.position + transform.forward * 100f; // 100 units forward in the current direction
            useCurve = false; // 직선 이동으로 설정
        }

        // 시작 지연을 랜덤으로 설정
        startTime = Time.time + Random.Range(minStartDelay, maxStartDelay);
    }

    void Update()
    {
        // 지연이 지나지 않았으면 기다림
        if (Time.time < startTime)
            return;

        // 현재 목표 점으로 이동
        if (useCurve)
        {
            // 곡선 경로를 따라 이동
            t += Time.deltaTime * speed / Vector3.Distance(startPoint, endPoint);
            t = Mathf.Clamp01(t);  // t를 0과 1 사이로 제한

            // 베지어 곡선의 위치를 계산
            Vector3 curvePosition = CalculateBezierCurve(startPoint, controlPoint, endPoint, t);
            transform.position = curvePosition;

            // 오브젝트가 끝점에 도달하면 플레이어의 새로운 위치에 맞게 업데이트
            if (t >= 1f)
            {
                // 정확한 위치로 설정
                transform.position = endPoint;

                // 새로운 시작점과 끝점 설정
                startPoint = endPoint;

                FindClosestEnemy(); // 새로운 가장 가까운 적을 찾아서 설정

                if (enemy != null)
                {
                    endPoint = enemy.position;
                    SetRandomControlPoint();  // 새로운 랜덤 제어점 설정
                }
                else
                {
                    // 목표가 없을 경우 직선으로 이동
                    endPoint = transform.position + transform.forward * 100f;
                    useCurve = false; // 직선 이동으로 설정
                }

                t = 0f;  // 시간 리셋
            }
        }
        else
        {
            // 직선으로 이동
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);

            // 직선 이동은 계속 직진하도록 설정
        }
    }

    // 2차 베지어 곡선 계산
    Vector3 CalculateBezierCurve(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        float u = 1f - t;
        return u * u * start + 2f * u * t * control + t * t * end;
    }

    // 제어점을 랜덤하게 설정
    void SetRandomControlPoint()
    {
        // 제어점을 현재 시작점과 끝점의 중간점에서 랜덤한 방향으로 이동
        Vector3 midPoint = (startPoint + endPoint) / 2;
        Vector3 randomDirection = Random.onUnitSphere;  // 랜덤 방향 벡터
        controlPoint = midPoint + randomDirection * curveStrength;
    }

    // 가장 가까운 적을 찾아서 설정
    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemyObject in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemyObject.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemyObject.transform;
            }
        }
        enemy = closestEnemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemyScript = other.GetComponent<Enemy>();
            enemyScript.TakeDamage(1); // enemy 스크립트를 가져와 피격 처리

            Destroy(gameObject);
            ObjectPool.SpawnFromPool("RoketBoom", transform.position, Quaternion.identity);
        }
    }
}
