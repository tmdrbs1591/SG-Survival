using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime; // �� ���� ����

    [SerializeField] private List<GameObject> StraightEnemys = new List<GameObject>(); // ���� �� ���
    [SerializeField] private List<GameObject> SideEnemys = new List<GameObject>(); // ���̵� �� ���
    [SerializeField] private List<GameObject> Item = new List<GameObject>(); // ������ ���

    [Header("���� ����Ʈ")]
    [SerializeField] private List<Transform> straightSpawnPoints = new List<Transform>(); // ���� �� ���� ����Ʈ
    [SerializeField] private List<Transform> sideSpawnPoints = new List<Transform>(); // ���̵� �� ���� ����Ʈ

    private void Start()
    {
        // ���� Ÿ�̸� ����
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            EnemySpawn();
            yield return new WaitForSeconds(spawnTime); // �ֱ������� �� ����
        }
    }

    private void EnemySpawn()
    {
        // ���� ������ �������� ����
        bool spawnStraight = Random.value > 0.5f; // 50% Ȯ���� ���� �� �Ǵ� ���̵� �� ����

        // �� �������� �������� ����
        List<GameObject> enemyList = spawnStraight ? StraightEnemys : SideEnemys;
        List<Transform> spawnPoints = spawnStraight ? straightSpawnPoints : sideSpawnPoints;

        if (enemyList.Count == 0)
        {
            Debug.LogWarning("No enemies assigned for the selected type.");
            return;
        }

        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points assigned for the selected type.");
            return;
        }

        // 10% Ȯ���� �������� �������� ����
        bool spawnItem = Random.value < 0.1f; // 10% Ȯ��

        if (spawnItem && spawnStraight)
        {
            // ������ ����� ��� ���� ������ Ȯ��
            if (Item.Count > 0)
            {
                GameObject itemPrefab = Item[Random.Range(0, Item.Count)];
                Transform spawnPoint = straightSpawnPoints[Random.Range(0, straightSpawnPoints.Count)];

                // ������ ����Ʈ�� ȸ�������� �������� ����
                Instantiate(itemPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        else
        {
            // ���� ����
            GameObject enemyPrefab = enemyList[Random.Range(0, enemyList.Count)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // ������ ����Ʈ�� ȸ�������� ���� ����
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
