using UnityEngine;

[CreateAssetMenu(fileName = "New Int Data", menuName = "Int Data")]
public class IntData : ScriptableObject
{
    public int value;
    private string saveKey;

    private void OnEnable()
    {
        saveKey = "HighScore_" + name; // Unique key based on ScriptableObject name
        LoadValue(); // Load saved data when enabled
    }

    public void SetValue(int num)
    {
        value = num;
        SaveValue();
    }

    public void CompareValue(IntData obj)
    {
        if (value < obj.value)
        {
            value = obj.value;
            SaveValue();
        }
    }

    public void SetValue(IntData obj)
    {
        value = obj.value;
        SaveValue();
    }

    public void UpdateValue(int num)
    {
        value += num;
        SaveValue();
    }

    public void SaveValue()
    {
        PlayerPrefs.SetInt(saveKey, value);
        PlayerPrefs.Save(); // Ensure data is saved immediately
    }

    public void LoadValue()
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            value = PlayerPrefs.GetInt(saveKey);
        }
    }
}

