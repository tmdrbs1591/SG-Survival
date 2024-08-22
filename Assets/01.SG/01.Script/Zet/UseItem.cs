using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{

    [Header("로켓 아이템")]
    [SerializeField] bool useRoketItem;
    [SerializeField] GameObject RoketPrefabs;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseRoket();
        }
    }


    void UseRoket()
    {
        if (useRoketItem)
        {
            StartCoroutine(UseRoketCor());
        }
    }

    IEnumerator UseRoketCor()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(RoketPrefabs, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
