using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject fadein;
    [SerializeField] GameObject fadeOut;
    private void Start()
    {
        fadeOut.SetActive(false);
        fadeOut.SetActive(true);
    }

    public void GameStart()
    {
        fadein.SetActive(true);
        StartCoroutine(SceneCor());
    }

    IEnumerator SceneCor()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("SampleScene");
    }
}
