using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;       // ความเร็วกระสุน
    public float lifeTime = 5f;     // อายุสูงสุดของกระสุน (กันบั๊ก)
    private BulletPool pool;

    private Rigidbody2D rb;

    void OnEnable()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        // ยิงกระสุนไปในทิศทางขึ้น (หรือทิศที่กระสุนหัน)
        rb.linearVelocity = transform.up * speed;

        // ตั้งเวลาให้กระสุนหายเองหลัง lifeTime
        CancelInvoke();
        Invoke(nameof(Disable), lifeTime);
    }

     public void Init(BulletPool bulletPool)
    {
        pool = bulletPool;
    }

    private void OnDisable()
    {
        // เวลา Bullet หายไป (เช่น ชนศัตรู หรือ ออกจากหน้าจอ)
        // จะถูกคืนกลับไปใน Pool
        if (pool != null)
        {
            pool.ReturnToPool(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ถ้าโดนศัตรู (Enemy) เท่านั้น ถึงจะหาย
        if (other.CompareTag("Enemy"))
        {
            // TODO: เพิ่มโค้ดลดพลังชีวิต Enemy ตรงนี้ได้
            Disable();
        }
    }

    void OnBecameInvisible()
    {
        // หายเมื่อออกนอกกล้อง
        Disable();
    }

    void Disable()
    {
        gameObject.SetActive(false); // คืนไปที่ Pool
    }

    
}
