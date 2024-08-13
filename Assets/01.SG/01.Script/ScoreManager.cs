using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static ScoreManager instance { get; private set; }

    [SerializeField] private TMP_Text scoreText; // TextMeshPro �ؽ�Ʈ ������Ʈ
    [SerializeField] private Animator anim;

    private int score;

    private void Awake()
    {
        // �ߺ��� �ν��Ͻ��� �ִ��� Ȯ��
        if (instance == null)
        {
            // �ν��Ͻ��� null�� ���� ���� ��ü�� �ν��Ͻ��� ����
            instance = this;
            // �� ��ü�� �� ��ȯ �ÿ��� �ı����� �ʵ��� ����
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // �ν��Ͻ��� �̹� �����ϸ�, ���� ��ü�� �ı�
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // ������ 8�ڸ� ���ڷ� �������Ͽ� UI�� ������Ʈ
        scoreText.text = score.ToString("D8"); // D8�� 8�ڸ� ���� ����, ������ �ڸ��� 0���� ä��
    }

    // ���� �߰� �޼���
    public void AddScore(int amount)
    {
        anim.SetTrigger("Bounce");
        score += amount;
    }
}
