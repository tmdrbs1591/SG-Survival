using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roket : MonoBehaviour
{
    public float speed = 5f;  // ������ �̵� �ӵ�
    public float curveStrength = 2f;  // ��� ���� (�������� �̵� �Ÿ�)
    public float minStartDelay = 0f;  // ���� ������ �ּ� �� (��)
    public float maxStartDelay = 0.5f;  // ���� ������ �ִ� �� (��)
    public float xp; // �� ����ġ

    [SerializeField] GameObject BoomEffect;

    private Transform enemy;  // ������ Transform
    private Vector3 startPoint;
    private Vector3 controlPoint;
    private Vector3 endPoint;
    private float t = 0f;  // �ð� ����
    private float startTime;  // ���� �ð�
    private bool useCurve = true;  // � ��� ����

    void Start()
    {
        FindClosestEnemy(); // ���� ����� ���� ã�Ƽ� ����

        // ������ �ʱ�ȭ
        startPoint = transform.position;

        if (enemy != null)
        {
            endPoint = enemy.position;
            SetRandomControlPoint();  // ���� ������ ����
        }
        else
        {
            // ��ǥ�� ���� ��� �������� �� ������ ����
            endPoint = transform.position + transform.forward * 100f; // 100 units forward in the current direction
            useCurve = false; // ���� �̵����� ����
        }

        // ���� ������ �������� ����
        startTime = Time.time + Random.Range(minStartDelay, maxStartDelay);
    }

    void Update()
    {
        // ������ ������ �ʾ����� ��ٸ�
        if (Time.time < startTime)
            return;

        // ���� ��ǥ ������ �̵�
        if (useCurve)
        {
            // � ��θ� ���� �̵�
            t += Time.deltaTime * speed / Vector3.Distance(startPoint, endPoint);
            t = Mathf.Clamp01(t);  // t�� 0�� 1 ���̷� ����

            // ������ ��� ��ġ�� ���
            Vector3 curvePosition = CalculateBezierCurve(startPoint, controlPoint, endPoint, t);
            transform.position = curvePosition;

            // ������Ʈ�� ������ �����ϸ� �÷��̾��� ���ο� ��ġ�� �°� ������Ʈ
            if (t >= 1f)
            {
                // ��Ȯ�� ��ġ�� ����
                transform.position = endPoint;

                // ���ο� �������� ���� ����
                startPoint = endPoint;

                FindClosestEnemy(); // ���ο� ���� ����� ���� ã�Ƽ� ����

                if (enemy != null)
                {
                    endPoint = enemy.position;
                    SetRandomControlPoint();  // ���ο� ���� ������ ����
                }
                else
                {
                    // ��ǥ�� ���� ��� �������� �̵�
                    endPoint = transform.position + transform.forward * 100f;
                    useCurve = false; // ���� �̵����� ����
                }

                t = 0f;  // �ð� ����
            }
        }
        else
        {
            // �������� �̵�
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);

            // ���� �̵��� ��� �����ϵ��� ����
        }
    }

    // 2�� ������ � ���
    Vector3 CalculateBezierCurve(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        float u = 1f - t;
        return u * u * start + 2f * u * t * control + t * t * end;
    }

    // �������� �����ϰ� ����
    void SetRandomControlPoint()
    {
        // �������� ���� �������� ������ �߰������� ������ �������� �̵�
        Vector3 midPoint = (startPoint + endPoint) / 2;
        Vector3 randomDirection = Random.onUnitSphere;  // ���� ���� ����
        controlPoint = midPoint + randomDirection * curveStrength;
    }

    // ���� ����� ���� ã�Ƽ� ����
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
            enemyScript.TakeDamage(1); // enemy ��ũ��Ʈ�� ������ �ǰ� ó��

            Destroy(gameObject);
            ObjectPool.SpawnFromPool("RoketBoom", transform.position, Quaternion.identity);
        }
    }
}
