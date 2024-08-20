using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP : MonoBehaviour
{
    public float speed = 5f;  // ������ �̵� �ӵ�
    public float curveStrength = 2f;  // ��� ���� (�������� �̵� �Ÿ�)
    public float minStartDelay = 0f;  // ���� ������ �ּ� �� (��)
    public float maxStartDelay = 0.5f;  // ���� ������ �ִ� �� (��)
    public float xp; // �� ����ġ

    [SerializeField] GameObject ExpEffect;

    private Transform player;  // �÷��̾��� Transform
    private Vector3 startPoint;
    private Vector3 controlPoint;
    private Vector3 endPoint;
    private float t = 0f;  // �ð� ����
    private float startTime;  // ���� �ð�
    private bool useCurve = true;  // � ��� ����

    void Start()
    {
        // �÷��̾� ������Ʈ�� �±׸� ���� ã��
        GameObject playerObject = GameObject.FindGameObjectWithTag("Zet");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("�±װ� 'Zet'�� ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        // ������, ���� �ʱ�ȭ
        startPoint = transform.position;
        endPoint = player.position;
        SetRandomControlPoint();  // ���� ������ ����

        // ���� ������ �������� ����
        startTime = Time.time + Random.Range(minStartDelay, maxStartDelay);
    }

    void Update()
    {
        if (player == null)
            return;

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
                endPoint = player.position;
                SetRandomControlPoint();  // ���ο� ���� ������ ����
                t = 0f;  // �ð� ����
                useCurve = false;  // ���� �̵����� ����
            }
        }
        else
        {
            // �������� �̵�
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);

            // ������Ʈ�� ������ �����ϸ� �÷��̾��� ���ο� ��ġ�� �°� ������Ʈ
            if (transform.position == endPoint)
            {
                // ���ο� �������� ���� ����
                startPoint = endPoint;
                endPoint = player.position;
                // ���� �̵����� ��� ����
                useCurve = true;
                SetRandomControlPoint();  // ���ο� ���� ������ ����
            }
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

        // Y�� ���⸸ ����ϵ��� �����Ϸ��� ������ ���� �� �� ����:
        // controlPoint = midPoint + new Vector3(Random.Range(-curveStrength, curveStrength), Random.Range(-curveStrength, curveStrength), Random.Range(-curveStrength, curveStrength));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zet"))
        {
            ZetLevel.instance.currentXp += xp;
            ZetLevel.instance.LV_UP();

            Destroy(gameObject);
            Destroy(Instantiate(ExpEffect, transform.position, Quaternion.identity), 2f);

        }
    }
}
