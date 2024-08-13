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

    // Start is called before the first frame update
    void Start()
    {
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (curHp <= 0 )
        {
            Instantiate(DieExplosionPtc, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
     
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
}
