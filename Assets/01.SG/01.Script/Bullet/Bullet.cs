using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed; // �Ѿ� �ӵ�
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
            enemyScript.TakeDamage(1); // enemy ��ũ��Ʈ�� ������ �ǰ�ó��
            Destroy(gameObject);
        }
    }
}
