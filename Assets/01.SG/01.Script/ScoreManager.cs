using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static ScoreManager instance { get; private set; }

    [SerializeField] private TMP_Text scoreText; // TextMeshPro 텍스트 컴포넌트
    [SerializeField] private Animator anim;

    private int score;

    private void Awake()
    {
        // 중복된 인스턴스가 있는지 확인
        if (instance == null)
        {
            // 인스턴스가 null일 때만 현재 객체를 인스턴스로 설정
            instance = this;
            // 이 객체가 씬 전환 시에도 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 인스턴스가 이미 존재하면, 현재 객체를 파괴
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // 점수를 8자리 숫자로 포맷팅하여 UI에 업데이트
        scoreText.text = score.ToString("D8"); // D8은 8자리 숫자 포맷, 부족한 자리는 0으로 채움
    }

    // 점수 추가 메서드
    public void AddScore(int amount)
    {
        anim.SetTrigger("Bounce");
        score += amount;
    }
}
