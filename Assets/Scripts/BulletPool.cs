using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int initialSize = 30;

    private readonly Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < initialSize; i++)
        {
            var b = CreateNew();
            Despawn(b);
        }
    }

    GameObject CreateNew()
    {
        var go = Instantiate(bulletPrefab, transform);
        go.SetActive(false);
        var bullet = go.GetComponent<Bullet>();
        if (bullet) bullet.Init(this);
        return go;
    }

    public GameObject Spawn(Vector3 pos, Quaternion rot)
    {
        GameObject go = pool.Count > 0 ? pool.Dequeue() : CreateNew();
        go.transform.SetPositionAndRotation(pos, rot);
        go.SetActive(true);
        return go;
    }

    public void Despawn(GameObject go)
    {
        go.SetActive(false);
        pool.Enqueue(go);
    }

     public void ReturnToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }
}
