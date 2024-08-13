using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private EnemyData enemyData; // ��ũ���ͺ� ������Ʈ ����

    [SerializeField] private float curHp;

    [SerializeField] GameObject mesh;
    [SerializeField] Material normalMaterial;
    [SerializeField] Material hitMaterial;

    [SerializeField] GameObject DieExplosionPtc;


    private bool isDie;

    // Start is called before the first frame update
    void Start()
    {
        curHp = enemyData.health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * enemyData.speed * Time.deltaTime, Space.Self);
        Die();
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;

        // ���׸����� hitMaterial�� ����
        StartCoroutine(ChangeMaterialTemporary());
    }
    void Die()
    {
        if (curHp <= 0 && !isDie) // ��� ���� ������ ���� bool �� �߰�
        {
            ScoreManager.instance.AddScore(enemyData.score);

            isDie = true;

            CameraShake.instance.Shake(0.5f,0.07f);

            Instantiate(DieExplosionPtc, transform.position, Quaternion.identity);
            StartCoroutine(DieSequence());
        }
    }

    IEnumerator DieSequence()
    {
        yield return TimeStop();  // �ð� ���� ȿ���� ���� ������ ���
        Destroy(gameObject);      // ��ü�� ����
    }


    IEnumerator ChangeMaterialTemporary()
    {
        // ���׸����� hitMaterial�� ����
        mesh.GetComponent<Renderer>().material = hitMaterial;

        // 0.1�� ���
        yield return new WaitForSeconds(0.1f);

        // ���׸����� normalMaterial�� ����
        mesh.GetComponent<Renderer>().material = normalMaterial;
    }

    IEnumerator TimeStop()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.55f);
        Time.timeScale = 1f;


    }
}
