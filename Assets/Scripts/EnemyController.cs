using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    public int health = 3;
    private Rigidbody2D rb;
    GameObject player;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
    }
}
