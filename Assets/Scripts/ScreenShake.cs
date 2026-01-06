using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance { get; private set; }

    [Range(0f, 2f)]
    [SerializeField] private float screenShakeIntensity = 1f;
    [SerializeField] private float screenRotateIntensity = 1f;

    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

    public void Shake(float duration, float magnitude = 0.2f)
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // --- Position shake ---
            float x = Random.Range(-1f, 1f) * magnitude * screenShakeIntensity;
            float y = Random.Range(-1f, 1f) * magnitude * screenShakeIntensity;

            // --- Rotation shake (around Z-axis, like camera tilt) ---
            float zRot = Random.Range(-1f, 1f) * magnitude * 10f * screenRotateIntensity;

            transform.localPosition = initialLocalPosition + new Vector3(x, y, 0f);
            transform.localRotation = initialLocalRotation * Quaternion.Euler(0f, 0f, zRot);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // Reset transform
        transform.localPosition = initialLocalPosition;
        transform.localRotation = initialLocalRotation;
        shakeRoutine = null;
    }
}