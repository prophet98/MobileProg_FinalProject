using TMPro;
using UnityEngine;

public class HighScoreVisualizer : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = $"you scored {PlayerPrefs.GetFloat("PlayerScore")} points!";
    }
}