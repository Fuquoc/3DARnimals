using System.Collections;
using UnityEngine;

public class AnimalRotation : MonoBehaviour
{
    public float rotationSpeed = 5f;      // Tốc độ xoay khi người dùng chạm hoặc kéo chuột
    public float returnSpeed = 2f;        // Tốc độ quay về vị trí ban đầu
    public float idleTime = 1f;           // Thời gian không tương tác để trở về góc ban đầu

    private Quaternion initialRotation;   // Góc xoay ban đầu
    private bool isTouching = false;      // Cờ kiểm tra có đang tương tác không
    private float touchTimer = 0f;        // Bộ đếm thời gian không tương tác
    private Vector2 previousMousePosition; // Vị trí chuột hoặc touch trước đó

    void Start()
    {
        initialRotation = transform.rotation;  // Lưu lại góc ban đầu
    }

    void Update()
    {
        // // Kiểm tra thao tác trên điện thoại
        // if (Input.touchCount > 0)
        // {
        //     isTouching = true;
        //     touchTimer = 0f;

        //     Touch touch = Input.GetTouch(0);
        //     if (touch.phase == TouchPhase.Moved)
        //     {
        //         float rotationX = touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
        //         transform.Rotate(rotationX, 0, 0, Space.Self);
        //     }
        // }
        // Kiểm tra thao tác chuột trên máy tính
        if(Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            isTouching = true;
            touchTimer = 0f;

            Vector2 mouseDelta = (Vector2)Input.mousePosition - previousMousePosition;
            float rotationX = mouseDelta.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, -rotationX, 0, Space.Self);

            previousMousePosition = Input.mousePosition;
        }
        else
        {
            isTouching = false;
        }

        // Nếu không tương tác thì tăng timer và trở về góc ban đầu
        if (!isTouching)
        {
            touchTimer += Time.deltaTime;

            if (touchTimer >= idleTime)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, returnSpeed * Time.deltaTime);
            }
        }

        // Lưu lại vị trí chuột để tính toán delta khi tương tác tiếp
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }
    }
}
