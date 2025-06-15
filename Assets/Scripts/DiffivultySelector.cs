using UnityEngine;
using TMPro;

public class DifficultySelector : MonoBehaviour
{
    public TMP_Dropdown difficultyDropdown;

    void Start()
    {
        difficultyDropdown.value = (int)GameSettings.currentDifficulty;
        difficultyDropdown.onValueChanged.AddListener(SetDifficulty);
    }

    public void SetDifficulty(int index)
    {
        GameSettings.currentDifficulty = (Difficulty)index;
        Debug.Log("Selected difficulty: " + GameSettings.currentDifficulty);
    }
}
