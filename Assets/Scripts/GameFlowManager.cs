using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance { get; private set; }

    [Header("Canvas / UI")]
    public Canvas targetCanvas;
    public TextMeshProUGUI targetText;
    public Canvas hudCanvas;
    public Canvas endCanvas;
    public TextMeshProUGUI endText;

    [Header("Death UI")]
    public Canvas deathCanvas;
    public TextMeshProUGUI deathText;

    [Header("Config")]
    public float revealDuration = 2.5f;
    public int minTarget = 36;
    public int maxTargetInclusive = 81;

    int targetScore;
    bool isPlaying = false, isEnded = false;

    // ✨ Quan trọng: gán Instance ở đây
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (targetCanvas) targetCanvas.gameObject.SetActive(true);
        if (hudCanvas) hudCanvas.gameObject.SetActive(false);
        if (endCanvas) endCanvas.gameObject.SetActive(false);
        if (deathCanvas) deathCanvas.gameObject.SetActive(false);

        targetScore = Random.Range(minTarget, maxTargetInclusive + 1);
        if (targetText) targetText.text = $"TARGET: {targetScore}";

        if (ScoreManager.Instance) ScoreManager.Instance.ResetScore();
        StartCoroutine(TargetRevealThenPlay());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Application.CanStreamedLevelBeLoaded("MainMenu"))
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                Debug.LogError("Scene 'MainMenu' chưa add vào Build Settings hoặc tên sai!");
            }
        }
    }


    IEnumerator TargetRevealThenPlay()
    {
        yield return new WaitForSeconds(revealDuration);

        if (targetCanvas) targetCanvas.gameObject.SetActive(false);
        if (hudCanvas) hudCanvas.gameObject.SetActive(true);

        isPlaying = true;

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
    }

    void OnDestroy()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged -= OnScoreChanged;
        if (Instance == this) Instance = null; // dọn singleton
    }

    void OnScoreChanged(int newScore)
    {
        if (!isPlaying || isEnded) return;

        if (newScore >= targetScore)
        {
            isEnded = true;
            isPlaying = false;

            if (hudCanvas) hudCanvas.gameObject.SetActive(false);
            if (endCanvas) endCanvas.gameObject.SetActive(true);
            if (endText) endText.text = "RE ROI RE ROI REEEE!";

            // đảm bảo end canvas nằm trên cùng
            var c = endCanvas.GetComponent<Canvas>();
            if (c) { c.renderMode = RenderMode.ScreenSpaceOverlay; c.sortingOrder = 100; }
        }
    }

    public void OnPlayerDead()
    {
        Debug.Log("OnPlayerDead() fired");

        if (hudCanvas) hudCanvas.gameObject.SetActive(false);
        if (deathCanvas) deathCanvas.gameObject.SetActive(true);
        if (deathText) deathText.text = "NON!";

        // đảm bảo nằm trên cùng
        var c = deathCanvas ? deathCanvas.GetComponent<Canvas>() : null;
        if (c) { c.renderMode = RenderMode.ScreenSpaceOverlay; c.sortingOrder = 200; }
    }

    public void OnReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
