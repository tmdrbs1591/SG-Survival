using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime; // 적 스폰 간격

    [SerializeField] private List<GameObject> StraightEnemys = new List<GameObject>(); // 직선 적 목록
    [SerializeField] private List<GameObject> SideEnemys = new List<GameObject>(); // 사이드 적 목록
    [SerializeField] private List<GameObject> Item = new List<GameObject>(); // 아이템 목록

    [Header("스폰 포인트")]
    [SerializeField] private List<Transform> straightSpawnPoints = new List<Transform>(); // 직선 적 스폰 포인트
    [SerializeField] private List<Transform> sideSpawnPoints = new List<Transform>(); // 사이드 적 스폰 포인트

    private void Start()
    {
        // 스폰 타이머 시작
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            EnemySpawn();
            yield return new WaitForSeconds(spawnTime); // 주기적으로 적 스폰
        }
    }

    private void EnemySpawn()
    {
        // 적의 유형을 랜덤으로 선택
        bool spawnStraight = Random.value > 0.5f; // 50% 확률로 직선 적 또는 사이드 적 선택

        // 적 프리팹을 랜덤으로 선택
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

        // 10% 확률로 아이템을 스폰할지 결정
        bool spawnItem = Random.value < 0.1f; // 10% 확률

        if (spawnItem && spawnStraight)
        {
            // 아이템 목록이 비어 있지 않은지 확인
            if (Item.Count > 0)
            {
                GameObject itemPrefab = Item[Random.Range(0, Item.Count)];
                Transform spawnPoint = straightSpawnPoints[Random.Range(0, straightSpawnPoints.Count)];

                // 선택한 포인트의 회전값으로 아이템을 스폰
                Instantiate(itemPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        else
        {
            // 적을 스폰
            GameObject enemyPrefab = enemyList[Random.Range(0, enemyList.Count)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // 선택한 포인트의 회전값으로 적을 스폰
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
