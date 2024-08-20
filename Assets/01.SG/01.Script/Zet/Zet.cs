using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zet : MonoBehaviour
{
    [Header("움직임")]
    [SerializeField] float moveSpeed; // 움직임 속도
    Rigidbody rigid;
    Vector3 dir = Vector3.zero; // 방향벡터

    [Header("공격")]
    [SerializeField] GameObject BulletPrefabs;
    [SerializeField] GameObject BulletPrefabs2;
    [SerializeField] GameObject BulletPrefabs3;
    [SerializeField] float attackCoolTime; // 공격 쿨타임
    public Transform FirePoint; // 총알이 발사될 위치
    public Vector3 firePointOffset; // 발사 위치 오프셋
    float attackCurTime;
    [SerializeField] int bulletCount;

    [Header("애니메이션")]
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

    void Move() // 이동
    {
        rigid.MovePosition(rigid.position + dir * moveSpeed * Time.deltaTime);
    }

    void InputAndDir() // 키입력과 이동방향 구하기
    {
        dir.x = Input.GetAxis("Horizontal"); // x축 방향키 입력
        dir.y = Input.GetAxis("Vertical");   // y축 방향키 입력
    }

    void HandleAnimation() // 애니메이션 상태 처리
    {
        // 좌우 이동 애니메이션 처리
        if (dir.x < 0) // 왼쪽으로 이동
        {
            anim.SetBool("LeftMove", true);
            anim.SetBool("RightMove", false);
        }
        else if (dir.x > 0) // 오른쪽으로 이동
        {
            anim.SetBool("LeftMove", false);
            anim.SetBool("RightMove", true);
        }
        else // 좌우 정지 상태
        {
            anim.SetBool("LeftMove", false);
            anim.SetBool("RightMove", false);
        }

        // 상하 이동 애니메이션 처리
        if (dir.y > 0) // 위쪽으로 이동
        {
            anim.SetBool("UpMove", true);
            anim.SetBool("DownMove", false);
        }
        else if (dir.y < 0) // 아래쪽으로 이동
        {
            anim.SetBool("UpMove", false);
            anim.SetBool("DownMove", true);
        }
        else // 상하 정지 상태
        {
            anim.SetBool("UpMove", false);
            anim.SetBool("DownMove", false);
        }
    }

    void Fire() // 총알 발사
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        Vector3 fireDirection = (mouseWorldPosition - (FirePoint.position + firePointOffset)).normalized;

        AudioManager.instance.PlaySound(transform.position, 0, Random.Range(1f, 1.5f), 0.4f);


        // 제트기 앞쪽 방향을 기준으로 제한
        Vector3 forward = FirePoint.forward;
        if (Vector3.Dot(fireDirection, forward) < 0)
        {
            // 마우스 방향이 제트기 뒤쪽을 향할 때
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
        else if (bulletCount == 4)
        {
            GameObject bullet = Instantiate(BulletPrefabs2, FirePoint.position + firePointOffset, Quaternion.LookRotation(fireDirection));
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Initialize(fireDirection);


            GameObject bullet2 = Instantiate(BulletPrefabs, FirePoint.position + firePointOffset + new Vector3(0.7f, 0, 0), Quaternion.LookRotation(fireDirection));
            Bullet bulletScript2 = bullet2.GetComponent<Bullet>();
            bulletScript2.Initialize(fireDirection);


            GameObject bullet3 = Instantiate(BulletPrefabs, FirePoint.position + firePointOffset + new Vector3(-0.7f, 0, 0), Quaternion.LookRotation(fireDirection));
            Bullet bulletScript3 = bullet3.GetComponent<Bullet>();
            bulletScript3.Initialize(fireDirection);

            GameObject bullet4 = Instantiate(BulletPrefabs, FirePoint.position + firePointOffset + new Vector3(0, 0.7f, 0), Quaternion.LookRotation(fireDirection));
            Bullet bulletScript4 = bullet4.GetComponent<Bullet>();
            bulletScript4.Initialize(fireDirection);

        }

        else if (bulletCount == 5)
        {
            GameObject bullet = Instantiate(BulletPrefabs2, FirePoint.position + firePointOffset, Quaternion.LookRotation(fireDirection));
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Initialize(fireDirection);


            GameObject bullet2 = Instantiate(BulletPrefabs, FirePoint.position + firePointOffset + new Vector3(0.7f, 0, 0), Quaternion.LookRotation(fireDirection));
            Bullet bulletScript2 = bullet2.GetComponent<Bullet>();
            bulletScript2.Initialize(fireDirection);


            GameObject bullet3 = Instantiate(BulletPrefabs, FirePoint.position + firePointOffset + new Vector3(-0.7f, 0, 0), Quaternion.LookRotation(fireDirection));
            Bullet bulletScript3 = bullet3.GetComponent<Bullet>();
            bulletScript3.Initialize(fireDirection);

            GameObject bullet4 = Instantiate(BulletPrefabs, FirePoint.position + firePointOffset + new Vector3(0, 0.7f, 0), Quaternion.LookRotation(fireDirection));
            Bullet bulletScript4 = bullet4.GetComponent<Bullet>();
            bulletScript4.Initialize(fireDirection);

            GameObject bullet5 = Instantiate(BulletPrefabs, FirePoint.position + firePointOffset + new Vector3(0, -0.7f, 0), Quaternion.LookRotation(fireDirection));
            Bullet bulletScript5 = bullet5.GetComponent<Bullet>();
            bulletScript5.Initialize(fireDirection);
        }
        else if (bulletCount == 6)
        {
            GameObject bullet = Instantiate(BulletPrefabs3, FirePoint.position + firePointOffset, Quaternion.LookRotation(fireDirection));
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Initialize(fireDirection);
        }
    }

    Vector3 GetMouseWorldPosition() // 마우스의 월드 좌표를 계산
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, FirePoint.position); // 제트기가 위치한 평면
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }

        return ray.GetPoint(30); // 기본값
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
