using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonDataHandler 
{
    // Hàm lưu dữ liệu JSON
    public static void SaveData<T>(T data) {
        // Lấy tên class T làm tên file
        string fileName = typeof(T).Name + ".json";
        // Chuyển dữ liệu sang JSON
        string json = JsonUtility.ToJson(data, true); // true để format dễ đọc
        // Đường dẫn lưu file
        string path = Application.persistentDataPath + "/" + fileName;
        // Ghi file
        File.WriteAllText(path, json);
        Debug.Log($"Dữ liệu đã được lưu tại: {path}");
    }

    public static T SaveAndLoadData<T>(T data) where T : new() {
        // Lấy tên class T làm tên file
        string fileName = typeof(T).Name + ".json";
        // Đường dẫn lưu file
        string path = Application.persistentDataPath + "/" + fileName;

        // Lưu dữ liệu
        string json = JsonUtility.ToJson(data, true); // true để format dễ đọc
        File.WriteAllText(path, json);
        Debug.Log($"Dữ liệu đã được lưu tại: {path}");

        // Đọc lại dữ liệu từ file
        if (File.Exists(path)) {
            string loadedJson = File.ReadAllText(path);
            T loadedData = JsonUtility.FromJson<T>(loadedJson);
            Debug.Log($"Dữ liệu đã được đọc từ: {path}");
            return loadedData;
        } else {
            Debug.LogWarning($"File không tồn tại tại: {path}. Trả về dữ liệu mặc định.");
            return new T(); // Trả về dữ liệu mặc định nếu file không tồn tại
        }
    }
    
    // Hàm đọc dữ liệu JSON
    public static T LoadDataOrCreateNew<T>() where T : new() {
        // Lấy tên class T làm tên file
        string fileName = typeof(T).Name + ".json";
        // Đường dẫn file
        string path = Application.persistentDataPath + "/" + fileName;
        if (File.Exists(path)) {
            // Đọc file JSON
            string json = File.ReadAllText(path);
            // Chuyển JSON thành đối tượng
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log($"Dữ liệu đã được đọc từ: {path}");
            return data;
        } else {
            // Tạo dữ liệu mặc định
            Debug.LogWarning($"File không tồn tại tại: {path}. Đang tạo file mới...");
            T defaultData = new T();
            SaveData(defaultData); // Lưu dữ liệu mặc định
            return defaultData;
        }
    }

    public static T LoadData<T>() where T : new() {
        // Lấy tên class T làm tên file
        string fileName = typeof(T).Name + ".json";
        // Đường dẫn file
        string path = Application.persistentDataPath + "/" + fileName;
        if (File.Exists(path)) 
        {
            // Đọc file JSON
            string json = File.ReadAllText(path);
            // Chuyển JSON thành đối tượng
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log($"Dữ liệu đã được đọc từ: {path}");
            return data;
        } 
        else 
        {
            return default;
        }
    }

    public static void DeleteAllData()
    {
        // Lấy đường dẫn thư mục lưu dữ liệu
        string directoryPath = Application.persistentDataPath;

        // Lấy danh sách tất cả các file trong thư mục
        string[] files = Directory.GetFiles(directoryPath, "*.json");

        // Xóa từng file
        foreach (string file in files)
        {
            File.Delete(file);
            Debug.Log($"Đã xóa file: {file}");
        }

        Debug.Log("Tất cả dữ liệu đã được xóa.");
    }
}