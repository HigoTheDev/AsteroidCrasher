using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    private float minX, maxX, minY, maxY;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // mượt hơn
    }

    void Start()
    {
        // Tính giới hạn dựa trên Camera
        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        minX = -width / 2;
        maxX = width / 2;
        minY = -height / 2;
        maxY = height / 2;
    }

    void Update()
    {
        // Lấy input WASD hoặc phím mũi tên
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        Vector3 movement = new Vector3(moveX, moveY, 0f).normalized * speed * Time.deltaTime;

        // Cập nhật vị trí mới
        Vector3 newPosition = transform.position + movement;

        // Giới hạn trong biên
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }
}
