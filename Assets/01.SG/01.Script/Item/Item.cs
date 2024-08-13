using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public string itemType;

    private void Update()
    {
        transform.Translate(Vector3.forward * 10 * Time.deltaTime, Space.Self);
    }
}
