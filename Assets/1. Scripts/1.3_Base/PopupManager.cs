using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] private GameObject popup;

    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TextMeshProUGUI textButtonRight;
    [SerializeField] private TextMeshProUGUI textButtonLeft;

    public void ShowPopup(
        int buttonCount, 
        string textButtonLeft, 
        string textButtonRight, 
        string title, 
        string description, 
        Action leftAction = null, 
        Action rightAction = null
    )
    {
        // Hiển thị popup
        popup.SetActive(true);

        // Cập nhật tiêu đề và mô tả
        textTitle.text = title;
        textDescription.text = description;

        // Xóa sự kiện cũ để tránh bị chồng lặp
        leftButton.onClick.RemoveAllListeners();
        rightButton.onClick.RemoveAllListeners();

        // Xử lý theo số lượng nút
        if (buttonCount == 1)
        {
            // Chỉ hiển thị nút phải
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(true);

            // Cập nhật văn bản và gắn sự kiện cho nút phải
            this.textButtonRight.text = textButtonRight;
            rightButton.onClick.AddListener(() =>
            {
                popup.SetActive(false); // Đóng popup
                rightAction?.Invoke(); // Gọi hành động
            });
        }
        else if (buttonCount == 2)
        {
            // Hiển thị cả hai nút
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);

            // Cập nhật văn bản và gắn sự kiện cho nút trái
            this.textButtonLeft.text = textButtonLeft;
            leftButton.onClick.AddListener(() =>
            {
                popup.SetActive(false); // Đóng popup
                leftAction?.Invoke(); // Gọi hành động
            });

            // Cập nhật văn bản và gắn sự kiện cho nút phải
            this.textButtonRight.text = textButtonRight;
            rightButton.onClick.AddListener(() =>
            {
                popup.SetActive(false); // Đóng popup
                rightAction?.Invoke(); // Gọi hành động
            });
        }
        else
        {
            Debug.LogWarning("PopupManager: Số lượng nút không hợp lệ. Chỉ hỗ trợ 1 hoặc 2 nút.");
        }
    }
}
