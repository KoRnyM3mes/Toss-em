using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;  // Import TextMeshPro namespace

[RequireComponent(typeof(TextMeshProUGUI))]  // Ensure the component is TextMeshProUGUI
public class TextLabelBehavior : MonoBehaviour
{
    private TextMeshProUGUI label;  // Change the label type to TextMeshProUGUI
    public UnityEvent startEvent;

    private void Start()
    {
        label = GetComponent<TextMeshProUGUI>();  // Get the TextMeshProUGUI component
        startEvent.Invoke();
    }

    public void UpdateLabel(IntData obj)
    {
        if (label != null)
        {
            // Update the text using TextMeshPro formatting (if needed)
            label.text = obj.value.ToString(CultureInfo.InvariantCulture);
        }
    }
}

