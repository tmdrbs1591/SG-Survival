using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    public string enemyName; // ���� �̸�
    public float health; // ü��
    public int score; // �ִ� ���ھ�
    public float speed; // �ӵ�
    public GameObject bulletPrefab; // �ҷ� ������

    // �ʿ��ϴٸ� �� ���� �Ӽ� �߰� ����
}