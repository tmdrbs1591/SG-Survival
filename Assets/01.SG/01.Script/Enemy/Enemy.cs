using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float curHp;

    [SerializeField] GameObject mesh;
    [SerializeField] Material normalMaterial;
    [SerializeField] Material hitMaterial;

    [SerializeField] GameObject DieExplosionPtc;


    private bool isDie;

    // Start is called before the first frame update
    void Start()
    {
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * 5 * Time.deltaTime, Space.World);
        Die();
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;

        // 메테리얼을 hitMaterial로 변경
        StartCoroutine(ChangeMaterialTemporary());
    }
    void Die()
    {
        if (curHp <= 0 && !isDie) // 계속 실행 방지를 위해 bool 값 추가
        {
            isDie = true;

            CameraShake.instance.Shake(0.5f,0.07f);

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
