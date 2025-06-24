using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject gameWinUi;
    [SerializeField] private TextMeshProUGUI finalScoreText; // Hiển thị điểm khi kết thúc game
    [SerializeField] private TextMeshProUGUI highScoreText;  // Hiển thị điểm cao nhất
    [SerializeField] private TextMeshProUGUI finalScoreTextW; // Hiển thị điểm khi kết thúc game
    [SerializeField] private TextMeshProUGUI highScoreTextW;  // Hiển thị điểm cao nhất
    private bool isGameOver = false;
    private bool isGameWin = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScore();
        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);

        if (PlayerPrefs.HasKey("HighScore"))
        {
            int highScore = PlayerPrefs.GetInt("HighScore");
            Debug.Log("Điểm cao nhất đã lưu: " + highScore);
        }
        else
        {
            Debug.Log("Chưa có điểm cao nhất được lưu.");
        }

        LoadHighScore();
        UpdateHighScoreUI();
    }


    public void AddScore(int points)
    {
        if (!isGameOver && !isGameWin)
        {
            score += points;
            UpdateScore();
            Debug.Log("cong mot diem");
        }

    }
    private void UpdateScore()
    {
        scoreText.text = score.ToString();
        Debug.Log("Hien thi + 1 diem");
    }
    private void LoadHighScore()
    {
        // Lấy điểm cao nhất đã lưu, mặc định 0 nếu chưa có
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "Điểm cao nhất: " + highScore;

        highScoreTextW.text = "Điểm cao nhất: " + highScore;
    }
    private void UpdateHighScoreUI()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            highScoreText.text = "Điểm cao nhất: " + score;
            highScoreTextW.text = "Điểm cao nhất: " + score;
        }
    }

    private void ShowFinalScore()
    {
        if (finalScoreText != null)
            finalScoreText.text = "Điểm của bạn: " + score;
        if (finalScoreTextW != null)
            finalScoreTextW.text = "Điểm của bạn: " + score;

        if (highScoreText != null)
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = "Điểm cao nhất: " + highScore;
        }
        if (highScoreTextW != null)
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreTextW.text = "Điểm cao nhất: " + highScore;
        }
    }
    public void GameOver()
    {
        isGameOver = true;
        UpdateHighScoreUI();
        ShowFinalScore();
        Time.timeScale = 0;

        gameOverUi.SetActive(true);
        gameWinUi.SetActive(false);

    }
    public void GameWin()
    {
        isGameWin = true;
        UpdateHighScoreUI();
        ShowFinalScore();
        Time.timeScale = 0;

        gameWinUi.SetActive(true);
        gameOverUi.SetActive(false);

    }
    public void RestarGame()
    {
        isGameOver = false;
        isGameWin = false;

        score = 0;
        UpdateScore();
        Time.timeScale = 1;

        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);

        SceneManager.LoadScene("Game");
    }
    public void GotoMenu()
    {

        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);

        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
    public bool IsGameWin()
    {
        return isGameWin;
    }
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
        if (highScoreText != null)
            highScoreText.text = "Điểm cao nhất: 0";
        Debug.Log("Điểm cao nhất đã được reset về 0.");
    }


}