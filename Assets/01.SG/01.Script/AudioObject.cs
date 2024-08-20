using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    // ����� �ҽ� ������Ʈ
    private AudioSource aud;

    // ����� Ŭ��
    public AudioClip clip;

    // ����� ��ġ
    public float pitch = 1;

    // ����� ����
    public float volume = 1;

    // ����ٴ� Ʈ������
    public Transform follow;

    // ������Ʈ�� ����ٴϴ��� ����
    private bool following;

    void Start()
    {
        // ����� �ҽ� ������Ʈ�� ������
        aud = gameObject.GetComponent<AudioSource>();

        // ����� �ҽ� ����
        aud.clip = clip;
        aud.pitch = pitch;
        aud.volume = volume;

        // ����� ���
        aud.Play();

        // ����ٴ� Ʈ�������� �����Ǿ� �ִ��� ���� Ȯ��
        following = follow != null;
    }

    // �� �����Ӹ��� ȣ��Ǵ� ������Ʈ �޼���
    void Update()
    {
        // ����ٴ� Ʈ�������� ������ ������Ʈ ��ġ�� ������Ʈ
        if (follow != null)
            transform.position = new Vector3(follow.position.x, follow.position.y, -5);

        // ����ٴϴ� �߿� Ʈ�������� �������� ������Ʈ�� �ı�
        if (following && follow == null)
            Destroy(gameObject);

        // ����� ����� ������ ������Ʈ�� �ı�
        if (!aud.isPlaying)
            Destroy(gameObject); // ObjectPool.ReturnToPool("AudioObject", gameObject); // Ǯ�� �ý����� ����ϴ� ���
    }
}
