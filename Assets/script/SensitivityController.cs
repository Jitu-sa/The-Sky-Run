using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SensitivityController : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TextMeshProUGUI slidervalue;
    public int sensitivityValue=0;

    private void Start()
    {
        try
        {
            // Load sensitivity from PlayerPrefs
            sensitivityValue = PlayerPrefs.GetInt("sensitivity", 30);
            sensitivitySlider.value = sensitivityValue;
        }
        catch { }
    }

    public void OnSensitivityChanged()
    {
        sensitivityValue = Mathf.RoundToInt(sensitivitySlider.value);
        SaveSensitivity();
    }

    public void SaveSensitivity()
    {
        // Save sensitivity to PlayerPrefs
        PlayerPrefs.SetInt("sensitivity", sensitivityValue);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        try
        {
            slidervalue.text = sensitivityValue.ToString();
        }

        catch {}
    }

}
