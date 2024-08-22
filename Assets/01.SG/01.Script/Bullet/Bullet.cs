using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed; // 총알 속도
    [SerializeField] private float damage;
    private Vector3 direction;

    public void Initialize(Vector3 direction)
    {
        this.direction = direction;
    }

    protected void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
         var enemyScript =    other.GetComponent<Enemy>();
            enemyScript.TakeDamage(damage); // enemy 스크립트를 가져와 피격처리
           ObjectPool.ReturnToPool("Bullet1", gameObject);
           ObjectPool.ReturnToPool("Bullet2", gameObject);



        }
    }
}
