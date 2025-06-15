using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TMP_Text playerScoreText;
    public TMP_Text aiScoreText;

    public GameObject winPanel;
    public GameObject failPanel;

    public AudioSource voiceSource;
    public AudioClip eldenRingClip;
    public AudioClip witcherClip;

    private int playerScore = 0;
    private int aiScore = 0;
    public int maxScore = 5;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        UpdateScoreUI();

        if (winPanel != null) winPanel.SetActive(false);
        if (failPanel != null) failPanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void AddPlayerScore()
    {
        playerScore++;
        UpdateScoreUI();
        CheckEndCondition();
    }

    public void AddAIScore()
    {
        aiScore++;
        UpdateScoreUI();

        // Play voice line if AI reaches 5 and player has 0
        if (aiScore == maxScore && playerScore == 0)
        {
            PlayFailureVoiceLine();
        }

        CheckEndCondition();
    }

    void UpdateScoreUI()
    {
        playerScoreText.text = "Player Score: " + playerScore;
        aiScoreText.text = "AI Score: " + aiScore;
    }

    void CheckEndCondition()
    {
        if (playerScore >= maxScore)
            StartCoroutine(ShowEndScreen(true));
        else if (aiScore >= maxScore)
            StartCoroutine(ShowEndScreen(false));
    }

    IEnumerator ShowEndScreen(bool playerWon)
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;

        if (playerWon)
            winPanel.SetActive(true);
        else
            failPanel.SetActive(true);
    }

    void PlayFailureVoiceLine()
    {
        if (voiceSource != null)
        {
            if (Random.value < 0.5f)
            {
                AudioClip clipToPlay = Random.value < 0.5f ? eldenRingClip : witcherClip;
                voiceSource.PlayOneShot(clipToPlay);
            }
        }
    }
}
