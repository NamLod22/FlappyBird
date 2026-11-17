using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Start Menu")]
    [SerializeField] private GameObject _startMenu;

    [Header("In-Game Elements")]
    [SerializeField] private Bird _bird;
    [SerializeField] private PipeSpawner _pipeSpawner;
    [SerializeField] private GameObject _menuButton;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _gameOverPanel;

    private Vector3 _birdStartPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (_bird != null)
        {
            _birdStartPosition = _bird.transform.position;
        }
    }

    private void Start()
    {
        ShowStartMenu();
    }

    public void ShowStartMenu()
    {
        Time.timeScale = 1f;

        // Clear existing pipes
        ClearAllPipes();

        // Show Start Menu assets
        if (_startMenu != null)
        {
            _startMenu.SetActive(true);
        }

        // Hide In-Game assets
        if (_bird != null)
        {
            _bird.gameObject.SetActive(false);
            _bird.transform.position = _birdStartPosition;
            _bird.transform.rotation = Quaternion.identity;
        }

        if (_pipeSpawner != null)
        {
            _pipeSpawner.gameObject.SetActive(false);
        }

        if (_menuButton != null)
        {
            _menuButton.SetActive(false);
        }

        if (_scoreText != null)
        {
            _scoreText.gameObject.SetActive(false);
        }

        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(false);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        // Hide Start Menu assets
        if (_startMenu != null)
        {
            _startMenu.SetActive(false);
        }

        // Hide Game Over panel
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(false);
        }

        // Show HUD
        if (_scoreText != null)
        {
            _scoreText.gameObject.SetActive(true);
            _scoreText.text = "Score: 0";
        }

        if (_menuButton != null)
        {
            _menuButton.SetActive(true);
        }

        // Activate and reset gameplay elements
        if (_bird != null)
        {
            _bird.transform.position = _birdStartPosition;
            _bird.transform.rotation = Quaternion.identity;
            _bird.gameObject.SetActive(true);
            _bird.ResetBird();
        }

        if (_pipeSpawner != null)
        {
            _pipeSpawner.gameObject.SetActive(true);
            _pipeSpawner.ResetSpawner();
        }
    }

    public void RestartGame()
    {
        ClearAllPipes();
        StartGame();
    }

    public void ClearAllPipes()
    {
        var pipes = FindObjectsByType<Pipe>(FindObjectsSortMode.None);
        foreach (var pipe in pipes)
        {
            if (pipe != null && pipe.gameObject != null)
            {
                Destroy(pipe.gameObject);
            }
        }
    }
}

