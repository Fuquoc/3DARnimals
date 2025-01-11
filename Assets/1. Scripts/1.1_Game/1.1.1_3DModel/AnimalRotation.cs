using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalRotation : MonoBehaviour
{
    public float rotationSpeed = 5f;      
    public float returnSpeed = 2f;      
    public float idleTime = 1f;           

    private bool useReturnBack;
    private Quaternion initialRotation;   
    private bool isTouching = false;     
    private float touchTimer = 0f;        
    private Vector2 previousMousePosition;

    void Start()
    {
        initialRotation = transform.rotation;  // Lưu lại góc ban đầu
    }

    void Update()
    {
        // Kiểm tra thao tác chuột trên máy tính
        if(Input.GetMouseButtonDown(0) && IsPointerOverUI() == false)
        {
            previousMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && IsPointerOverUI() == false)
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
            if(useReturnBack == false) return;
            touchTimer += Time.deltaTime;

            if (touchTimer >= idleTime)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, returnSpeed * Time.deltaTime);
            }
        }

        // Lưu lại vị trí chuột để tính toán delta khi tương tác tiếp
        if (Input.GetMouseButtonDown(0) && IsPointerOverUI() == false)
        {
            previousMousePosition = Input.mousePosition;
        }
    }

    public bool IsPointerOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true; // Chuột chạm vào UI
        }

        // Kiểm tra cho cảm ứng
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return true; // Cảm ứng chạm vào UI
            }
        }

        return false; // Không chạm vào UI
    }

    public void TurnOnOffReturnBack(bool turn)
    {
        useReturnBack = turn;
    }
}
