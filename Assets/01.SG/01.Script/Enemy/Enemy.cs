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

        // ���׸����� hitMaterial�� ����
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
        // ���׸����� hitMaterial�� ����
        mesh.GetComponent<Renderer>().material = hitMaterial;

        // 0.1�� ���
        yield return new WaitForSeconds(0.1f);

        // ���׸����� normalMaterial�� ����
        mesh.GetComponent<Renderer>().material = normalMaterial;
    }
}
