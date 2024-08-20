using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // ����� Ŭ�� �迭
    public AudioClip[] clips;
    public AudioClip[] songs;
    public GameObject audioObject;
    public static AudioManager instance;
    private AudioSource aud;
    private float initVolume;
    private int curSong;

    void Awake()
    {
        // ���� ����� �Ŵ��� ������Ʈ�� �ı����� �ʵ��� ����
        // DontDestroyOnLoad(gameObject);

        // �̹� �����ϴ� ����� �����ʸ� ã��
        AudioListener existingListener = FindObjectOfType<AudioListener>();

        // �̹� ����� �����ʰ� ������ �̸� ��Ȱ��ȭ
        if (existingListener != null)
        {
            existingListener.enabled = false;
        }
        else
        {
            // ����� �����ʰ� ������ ����
            gameObject.AddComponent<AudioListener>();
        }

        // �̱��� �ν��Ͻ� ����
        instance = this;

        // ����� �ҽ� ������Ʈ�� ������
        aud = gameObject.GetComponent<AudioSource>();

        // ����� �ҽ� ������Ʈ�� ������ �߰�
        if (aud == null)
        {
            aud = gameObject.AddComponent<AudioSource>();
        }

        // �ʱ� ���� ����
        initVolume = aud.volume;
    }

    // ������ �ε����� �뷡�� ��ȯ
    public IEnumerator SwitchSong(int index)
    {
        // ���� ��� ���� �뷡�� �����ϸ� ���� ����
        if (curSong == index) yield break;

        // ���� �뷡�� ������ ���������� ����
        for (float i = 0; i < 1; i += Time.unscaledDeltaTime)
        {
            aud.volume = Mathf.Lerp(initVolume, 0, i);
            yield return null;
        }

        // �뷡 ���� �� ���ο� �뷡 ���� �� ���
        aud.Stop();
        aud.clip = songs[index];
        aud.Play();

        // ���ο� �뷡�� ������ ���������� ����
        for (float i = 0; i < 1; i += Time.unscaledDeltaTime)
        {
            aud.volume = Mathf.Lerp(0, initVolume, i);
            yield return null;
        }

        // ���� �뷡 �ε��� ������Ʈ
        curSong = index;
        yield break;
    }

    // ������ ��ġ���� ���� ���
    public void PlaySound(Vector3 position, int index, float pitch = 1, float volume = 1, Transform follower = null)
    {
        // ����� ������Ʈ ����
        AudioObject aud = GameObject.Instantiate(audioObject, new Vector3(position.x, position.y, -5), Quaternion.identity).GetComponent<AudioObject>();

        // ����� ������Ʈ�� ����ٴ� ��� ����
        aud.follow = follower;

        // ����� Ŭ�� ����
        aud.clip = clips[index];

        // ��ġ ����
        aud.pitch = pitch;

        // ���� ����
        volume = aud.volume;
    }
}
