using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;  // UI 컴포넌트 사용을 위해 추가

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float timeLimit = 60f;  // 게임이 시작되는 시간 (초)
    [SerializeField] private TMP_Text timeText;      // 화면에 시간을 표시할 Text UI
    [SerializeField] private TMP_Text scoreText;     // 점수를 표시할 Text UI
    [SerializeField] private GameObject resultPanel; // 결과 패널 (UI)

    private float currentTime;

    void Start()
    {
        // 초기 설정
        currentTime = timeLimit;
        resultPanel.SetActive(false); // 게임 시작 시 결과 패널은 비활성화
    }

    void Update()
    {
        // 시간이 0보다 클 때만 진행
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;  // 초단위로 시간 차감
        }
        else if (currentTime <= 0) // 시간이 0이 되면 멈추기
        {
            currentTime = 0;
            ShowResultPanel();  // 시간이 끝나면 결과 패널을 띄운다.
        }

        UpdateTimeDisplay();  // 화면에 시간을 업데이트
    }

    // 시간 화면에 표시 (00:00:00.0 형식)
    private void UpdateTimeDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60); // 분 계산
        int seconds = Mathf.FloorToInt(currentTime % 60); // 초 계산
        float milliseconds = currentTime * 10 % 10; // 소수점 초 (밀리초)

        // 00:00:00.0 형식으로 표시
        timeText.text = string.Format("{0:D2}:{1:D2}:{2:D1}", minutes, seconds, Mathf.FloorToInt(milliseconds));
    }

    // 결과 패널을 띄우는 함수
    private void ShowResultPanel()
    {
        // 점수 애니메이션 추가 (점수는 0부터 목표점수까지 증가)
        StartCoroutine(ScoreIncreaseAnimation());
        resultPanel.SetActive(true);  // 결과 패널을 활성화
        StartCoroutine(TimeStop());
    }

    IEnumerator TimeStop()
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0;
    }
    // 점수 애니메이션 (0부터 목표 점수까지 증가)
    private IEnumerator ScoreIncreaseAnimation()
    {
        int targetScore = ScoreManager.instance.score; // 목표 점수
        int currentScore = 0; // 현재 점수
        float duration = 2f; // 애니메이션 시간 (2초)
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            currentScore = Mathf.FloorToInt(Mathf.Lerp(0, targetScore, elapsedTime / duration)); // 점수를 점진적으로 증가
            scoreText.text = currentScore.ToString(); // 텍스트에 점수 표시
            yield return null;
        }

        scoreText.text = targetScore.ToString(); // 애니메이션 후 최종 점수 표시
    }

    // 시간을 리셋하고 결과 패널을 숨기는 함수 (게임을 다시 시작할 수 있게)
    public void ResetTime()
    {
        currentTime = timeLimit;
        resultPanel.SetActive(false);
    }

    // 게임 시작 후 타이머가 시작되도록 하는 함수
    public void StartTimer()
    {
        currentTime = timeLimit;
        resultPanel.SetActive(false);
    }
}
