using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance; // �̱��� �ν��Ͻ�

    private Camera mainCamera; // ��鸱 ���� ī�޶�
    private Vector3 originalCameraPos; // ī�޶� �ʱ� ��ġ ���� ����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // �ν��Ͻ� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� ���� ��ü�� �ı�
            return;
        }

        mainCamera = Camera.main; // "MainCamera" �±װ� ���� ī�޶� �ڵ����� ã��
        if (mainCamera != null)
        {
            originalCameraPos = mainCamera.transform.position; // �ʱ� ��ġ ����
        }
        else
        {
            Debug.LogError("Main camera not found. Please tag the main camera as 'MainCamera'.");
        }
    }

    public void Shake(float shakeRange = 0.5f, float duration = 0.1f)
    {
        if (mainCamera != null)
        {
            StopAllCoroutines(); // �ٸ� ��鸲�� ���� ���̸� ����
            StartCoroutine(ShakeCoroutine(shakeRange, duration));
        }
        else
        {
            Debug.LogError("Main camera is not assigned.");
        }
    }

    private IEnumerator ShakeCoroutine(float shakeRange, float duration)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float CameraPosX = Random.value * shakeRange * 2 - shakeRange;
            float CameraPosY = Random.value * shakeRange * 2 - shakeRange;

            Vector3 newCameraPos = originalCameraPos;
            newCameraPos.x += CameraPosX;
            newCameraPos.y += CameraPosY;

            mainCamera.transform.position = newCameraPos; // ī�޶� ��ġ ����

            elapsed += Time.deltaTime;

            yield return null;
        }

        mainCamera.transform.position = originalCameraPos; // �ʱ� ��ġ�� ����
    }
}
