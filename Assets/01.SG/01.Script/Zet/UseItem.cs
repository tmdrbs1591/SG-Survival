using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{

    [SerializeField] Image itemImage;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite roketSprite;
    [Header("로켓 아이템")]
    [SerializeField] bool useRoketItem;
    [SerializeField] GameObject RoketPrefabs;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseRoket();
        }

        ItemSprite();
    }


    void UseRoket()
    {
        if (useRoketItem)
        {
            useRoketItem = false;
          
            StartCoroutine(UseRoketCor());
        }
    }

    IEnumerator UseRoketCor()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(RoketPrefabs, transform.position, Quaternion.identity);
            AudioManager.instance.PlaySound(transform.position, 3, Random.Range(1f, 1.5f), 1f);
            yield return new WaitForSeconds(0.1f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            var itemScript = other.GetComponent<Item>();

            if (itemScript.itemType == "RoketItem")
            {
                Active();
                useRoketItem = true;
            }

            Destroy(other.gameObject);
        }
    }

    void ItemSprite()
    {
        if (useRoketItem) itemImage.sprite = roketSprite;
        else itemImage.sprite = defaultSprite;
    }

    void Active()
    {
        itemImage.gameObject.SetActive(false);
        itemImage.gameObject.SetActive(true);

    }
}
