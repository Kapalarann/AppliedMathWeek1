using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public static HashSet<Zone> zones = new HashSet<Zone>();
    public float dangerRadius = 1f;
    public float dist;
    public bool win = false;

    public float shakeStart = 5f;
    public float shakeSlow = 3f;
    private float currentShake = 0f;
    private Vector3 initPos;

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (zones == null) return;
        zones.Add(this);

        initPos = transform.position;
    }

    private void OnDestroy()
    {
        zones.Remove(this);
    }

    private void FixedUpdate()
    {
        if(currentShake > 0f)
        {
            float x = Random.Range(-1f, 1f) * currentShake;
            float y = Random.Range(-1f, 1f) * currentShake;

            transform.localPosition = initPos + new Vector3(x, y, 0f);

            currentShake -= shakeSlow * Time.fixedDeltaTime;
        }
    }

    public void Shake()
    {
        currentShake = shakeStart;
    }
}
