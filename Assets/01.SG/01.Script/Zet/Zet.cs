using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zet : MonoBehaviour
{
    [Header("������")]
    [SerializeField] float moveSpeed; // ������ �ӵ�
    Rigidbody rigid;
    Vector3 dir = Vector3.zero; // ���⺤��

    [Header("����")]
    [SerializeField] GameObject BulletPrefabs;
    [SerializeField] GameObject BulletPrefabs2;
    [SerializeField] float attackCoolTime; // ���� ��Ÿ��
    public Transform FirePoint; // �Ѿ��� �߻�� ��ġ
    public Vector3 firePointOffset; // �߻� ��ġ ������
    float attackCurTime;
    [SerializeField] int bulletCount;

    [Header("�ִϸ��̼�")]
    private Animator anim;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        InputAndDir();
        HandleAnimation();

        if (Input.GetMouseButton(0) && attackCurTime <= 0)
        {
            Fire();
            attackCurTime = attackCoolTime;
        }
        else
        {
            attackCurTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move() // �̵�
    {
        rigid.MovePosition(rigid.position + dir * moveSpeed * Time.deltaTime);
    }

    void InputAndDir() // Ű�Է°� �̵����� ���ϱ�
    {
        dir.x = Input.GetAxis("Horizontal"); // x�� ����Ű �Է�
        dir.y = Input.GetAxis("Vertical");   // y�� ����Ű �Է�
    }

    void HandleAnimation() // �ִϸ��̼� ���� ó��
    {
        // �¿� �̵� �ִϸ��̼� ó��
        if (dir.x < 0) // �������� �̵�
        {
            anim.SetBool("LeftMove", true);
            anim.SetBool("RightMove", false);
        }
        else if (dir.x > 0) // ���������� �̵�
        {
            anim.SetBool("LeftMove", false);
            anim.SetBool("RightMove", true);
        }
        else // �¿� ���� ����
        {
            anim.SetBool("LeftMove", false);
            anim.SetBool("RightMove", false);
        }

        // ���� �̵� �ִϸ��̼� ó��
        if (dir.y > 0) // �������� �̵�
        {
            anim.SetBool("UpMove", true);
            anim.SetBool("DownMove", false);
        }
        else if (dir.y < 0) // �Ʒ������� �̵�
        {
            anim.SetBool("UpMove", false);
            anim.SetBool("DownMove", true);
        }
        else // ���� ���� ����
        {
            anim.SetBool("UpMove", false);
            anim.SetBool("DownMove", false);
        }
    }

    void Fire() // �Ѿ� �߻�
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        Vector3 fireDirection = (mouseWorldPosition - (FirePoint.position + firePointOffset)).normalized;

        // ��Ʈ�� ���� ������ �������� ����
        Vector3 forward = FirePoint.forward;
        if (Vector3.Dot(fireDirection, forward) < 0)
        {
            // ���콺 ������ ��Ʈ�� ������ ���� ��
            return;
        }


        if (bulletCount == 1)
        {
            GameObject bullet = Instantiate(BulletPrefabs, FirePoint.position + firePointOffset, Quaternion.LookRotation(fireDirection));
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Initialize(fireDirection);
        }
        else if (bulletCount == 2)
        {
            GameObject bullet = Instantiate(BulletPrefabs, FirePoint.position + firePointOffset + new Vector3(-0.7f,0,0), Quaternion.LookRotation(fireDirection));
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Initialize(fireDirection);


            GameObject bullet2 = Instantiate(BulletPrefabs, FirePoint.position + firePointOffset + new Vector3(0.7f, 0, 0), Quaternion.LookRotation(fireDirection));
            Bullet bulletScript2 = bullet2.GetComponent<Bullet>();
            bulletScript2.Initialize(fireDirection);

        }
        else if (bulletCount == 3)
        {
            GameObject bullet = Instantiate(BulletPrefabs2  , FirePoint.position + firePointOffset, Quaternion.LookRotation(fireDirection));
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Initialize(fireDirection);


            GameObject bullet2 = Instantiate(BulletPrefabs, FirePoint.position + firePointOffset + new Vector3(0.7f, 0, 0), Quaternion.LookRotation(fireDirection));
            Bullet bulletScript2 = bullet2.GetComponent<Bullet>();
            bulletScript2.Initialize(fireDirection);


            GameObject bullet3 = Instantiate(BulletPrefabs, FirePoint.position + firePointOffset + new Vector3(-0.7f, 0, 0), Quaternion.LookRotation(fireDirection));
            Bullet bulletScript3 = bullet3.GetComponent<Bullet>();
            bulletScript3.Initialize(fireDirection);

        }




    }

    Vector3 GetMouseWorldPosition() // ���콺�� ���� ��ǥ�� ���
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, FirePoint.position); // ��Ʈ�Ⱑ ��ġ�� ���
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }

        return ray.GetPoint(30); // �⺻��
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            var itemScript = other.GetComponent<Item>();

            if (itemScript.itemType == "BulletPlus")
            {
                bulletCount++;
            }

            Destroy(other.gameObject);
        }
    }

}
