using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // ตัวละครผู้เล่น
    private Vector3 offset; // ระยะห่างระหว่างกล้องกับผู้เล่น
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
