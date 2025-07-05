using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementChapter4 : MonoBehaviour
{
    public float speed = 5f;
    public Camera cam;
    [Tooltip("Czas zwalniania po puszczeniu klawisza (w sekundach)")]
    public float decelerationTime = 0.3f;

    float currentVelocity = 0f;

    void Start()
    {
        if (cam == null) cam = Camera.main;
    }

    void FixedUpdate()
    {
        var kb = Keyboard.current;
        bool left = kb.aKey.isPressed;
        bool right = kb.dKey.isPressed;
        bool up = kb.wKey.isPressed;
        bool down = kb.sKey.isPressed;

        float target = 0f;
        if (left && !right) target = -1f;
        else if (right && !left) target = 1f;

        currentVelocity = Mathf.MoveTowards(currentVelocity, target, Time.deltaTime / decelerationTime);

        Vector3 delta = new Vector3(currentVelocity, 0, 0) * speed * Time.deltaTime;
        Vector3 newPos = transform.position + delta;

        Vector3 vp = cam.WorldToViewportPoint(newPos);
        vp.x = Mathf.Clamp(vp.x, 0f, 1f);
        Vector3 cw = cam.ViewportToWorldPoint(vp);

        transform.position = new Vector3(cw.x, transform.position.y, transform.position.z);
    }
}
