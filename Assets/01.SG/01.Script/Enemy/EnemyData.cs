using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    public string enemyName; // 적의 이름
    public float health; // 체력
    public int score; // 주는 스코어
    public float speed; // 속도
    public GameObject bulletPrefab; // 불렛 프리팹

    // 필요하다면 더 많은 속성 추가 가능
}