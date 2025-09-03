using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting")]
    public BulletPool bulletPool;
    public Transform firePoint;     // จุดกำเนิดกระสุน (ตั้ง child ไว้หน้าจมูกยาน)
    public float fireRate = 0.12f;  // ยิ่งน้อยยิงยิ่งถี่
    public bool holdToFire = true;  // กดค้างเพื่อยิง

    private float fireTimer;

    void Update()
    {
        fireTimer -= Time.deltaTime;

        bool firePressed = holdToFire ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");
        if (firePressed && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
    }

    void Shoot()
    {
        if (!bulletPool || !firePoint) return;
        bulletPool.Spawn(firePoint.position, firePoint.rotation);
        // TODO: เล่นเสียงยิง/เอฟเฟกต์ปากกระบอกปืนที่นี่
    }
}
