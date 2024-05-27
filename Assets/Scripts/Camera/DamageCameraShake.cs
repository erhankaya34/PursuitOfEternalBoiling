using UnityEngine;
using Cinemachine;
using System.Collections;

public class DamageCameraShake : MonoBehaviour
{
    public static DamageCameraShake Instance;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin perlinNoise;
    [SerializeField] private float originalOrthoSize;
    [SerializeField] private float zoomOrthoSize = 2f; // Inspector üzerinden bu değeri ayarlayabilirsiniz

    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeAmplitude = 1.2f;
    [SerializeField] private float shakeFrequency = 2.0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            virtualCamera = GetComponent<CinemachineVirtualCamera>() ?? throw new System.NullReferenceException("VirtualCamera is not attached.");
            perlinNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            originalOrthoSize = virtualCamera.m_Lens.OrthographicSize;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerShake()
    {
        StartCoroutine(Shake(shakeDuration, shakeAmplitude, shakeFrequency));
    }

    private IEnumerator Shake(float duration, float amplitude, float frequency)
    {
        perlinNoise.m_AmplitudeGain = amplitude;
        perlinNoise.m_FrequencyGain = frequency;
        yield return new WaitForSeconds(duration);
        perlinNoise.m_AmplitudeGain = 0;
        perlinNoise.m_FrequencyGain = 0;
    }

    public void TriggerHighImpactEffect(Transform target)
    {
        StartCoroutine(HighImpactSequence(target));
    }

    private IEnumerator HighImpactSequence(Transform target)
    {
        virtualCamera.Follow = target; // Start following the target
        float currentOrthoSize = virtualCamera.m_Lens.OrthographicSize;
        virtualCamera.m_Lens.OrthographicSize = zoomOrthoSize; // Zoom in

        yield return new WaitForSecondsRealtime(2); // Zoom effect duration

        virtualCamera.m_Lens.OrthographicSize = currentOrthoSize; // Reset orthographic size
        virtualCamera.Follow = null; // Stop following the target
        Time.timeScale = 1.0f; // Reset game speed
    }
}
