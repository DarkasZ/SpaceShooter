using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public Vector2 padding = new Vector2(0.5f, 0.5f); // กันออกนอกขอบจอ
    public bool tiltOnMove = true;
    public float tiltAmount = 10f;

    private Rigidbody2D rb;
    private Camera cam;
    private Vector2 moveInput;
    private Vector2 minBounds, maxBounds;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        CalculateCameraBounds();
    }

    void Update()
    {
        // รองรับ Keyboard/จอย (Horizontal/Vertical)
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(x, y).normalized;

        Vector2 newPos = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;

        // Clamp ขอบเขต
        newPos.x = Mathf.Clamp(newPos.x, minBounds.x + padding.x, maxBounds.x - padding.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y + padding.y, maxBounds.y - padding.y);

        // ขยับตัวละคร
        rb.MovePosition(newPos);

        // ป้องกันเลยขอบจอ – ล็อกตำแหน่งด้วย clamp
        /*Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minBounds.x + padding.x, maxBounds.x - padding.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y + padding.y, maxBounds.y - padding.y);
        transform.position = pos;*/

        // เอียงยานเล็ก ๆ ตามทิศทาง
        if (tiltOnMove)
        {
            float zRot = -moveInput.x * tiltAmount;
            transform.rotation = Quaternion.Euler(0, 0, zRot);
        }
        
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    void CalculateCameraBounds()
    {
        // คิดขอบกล้องแบบ Orthographic
        float ortho = cam.orthographicSize;
        float aspect = (float)Screen.width / Screen.height;
        float halfWidth = ortho * aspect;

        minBounds = new Vector2(cam.transform.position.x - halfWidth, cam.transform.position.y - ortho);
        maxBounds = new Vector2(cam.transform.position.x + halfWidth, cam.transform.position.y + ortho);
    }

    // เผื่อเปลี่ยนขนาดหน้าจอระหว่างเล่น
    void OnDrawGizmosSelected()
    {
        if (!cam) cam = Camera.main;
        if (!cam || !cam.orthographic) return;

        float ortho = cam.orthographicSize;
        float aspect = (float)Screen.width / Screen.height;
        float halfWidth = ortho * aspect;

        Vector3 c = cam.transform.position;
        Vector3 bl = new Vector3(c.x - halfWidth, c.y - ortho);
        Vector3 tr = new Vector3(c.x + halfWidth, c.y + ortho);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube((bl + tr) / 2f, tr - bl);
    }
}
