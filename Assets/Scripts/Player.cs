using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float shakeStrength = 1f;
    [SerializeField] GameObject sprite;

    private Vector3 moveVal;

    public void OnMove(InputValue value)
    {
        moveVal = new Vector3(value.Get<Vector2>().x, value.Get<Vector2>().y, 0f);
    }

    private void FixedUpdate()
    {
        if (isNearZone()) Manager.Instance.Victory();

        if (moveVal == null) return;

        transform.position += moveVal * movementSpeed * Time.fixedDeltaTime;
    }

    bool isNearZone()
    {
        foreach (Zone z in Zone.zones)
        {
            float dist = (transform.position - z.transform.position).magnitude;
            if (z.dist >= dist)
            {
                if (z.win) return true;
                else if (z.dangerRadius >= dist) Manager.Instance.Restart();
                else z.Shake();
            }
        }
        return false;
    }
}
