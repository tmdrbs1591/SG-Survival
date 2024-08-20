using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [SerializeField] public  EnemyData enemyData; // 스크립터블 오브젝트 참조

    [SerializeField] public float curHp;

    [SerializeField] GameObject mesh;
    [SerializeField] Material normalMaterial;
    [SerializeField] Material hitMaterial;

    [SerializeField] GameObject DieExplosionPtc;
    [SerializeField] GameObject EXP;


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

        AudioManager.instance.PlaySound(transform.position, 2, Random.Range(1f, 1.5f), 0.2f);

        // 메테리얼을 hitMaterial로 변경
        StartCoroutine(ChangeMaterialTemporary());
    }
    void Die()
    {
        if (curHp <= 0 && !isDie) // 계속 실행 방지를 위해 bool 값 추가
        {
            ScoreManager.instance.AddScore(enemyData.score);
            AudioManager.instance.PlaySound(transform.position, 1, Random.Range(1f, 1.8f), 0.4f);

            isDie = true;

            CameraShake.instance.Shake(0.5f,0.07f);

            for (int i = 0; i < enemyData.exp; i++) // enemyData에 exp 개수만큼 경험치 생성
            {
               var es =  Instantiate(EXP, transform.position, Quaternion.identity) .GetComponent<EXP>();
                es.xp = enemyData.expxp; // 생성할때 xp 에 넣기
            }

        



         Instantiate(DieExplosionPtc, transform.position, Quaternion.identity);
            StartCoroutine(DieSequence());
        }
    }

    IEnumerator DieSequence()
    {
        yield return TimeStop();  // 시간 멈춤 효과가 끝날 때까지 대기
        Destroy(gameObject);      // 객체를 삭제
    }


    IEnumerator ChangeMaterialTemporary()
    {
        // 메테리얼을 hitMaterial로 변경
        mesh.GetComponent<Renderer>().material = hitMaterial;

        // 0.1초 대기
        yield return new WaitForSeconds(0.1f);

        // 메테리얼을 normalMaterial로 복원
        mesh.GetComponent<Renderer>().material = normalMaterial;
    }

    IEnumerator TimeStop()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.55f);
        Time.timeScale = 1f;


    }
}
