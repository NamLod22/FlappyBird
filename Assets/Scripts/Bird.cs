using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Bird : MonoBehaviour
{
    [SerializeField] float _velocity = 1.5f;
    [SerializeField] float _rotationSpeed = 10f;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _gameOverPanel;

    private int _score = 0;
    private Rigidbody2D _rb;
    private bool _isGameOver = false;
    private Vector3 _startPosition;
    private int _resetFrame = -1;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
    }

    void Start()
    {
        ResetBird();
    }

    public void ResetBird()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        if (_rb != null)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _score = 0;
        _isGameOver = false;
        _resetFrame = Time.frameCount;

        if (_scoreText != null)
        {
            _scoreText.gameObject.SetActive(true);
            _scoreText.text = "Score: 0";
        }

        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (_isGameOver) return;
        // Prevent input on the exact frame the game starts/resets from button click
        if (Time.frameCount == _resetFrame) return;

        bool jump = false;

        if ((Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame))
        {
            jump = true;
        }
        else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            jump = true;
        }
        else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (jump && _rb != null)
        {
            _rb.linearVelocity = Vector2.up * _velocity;
        }
    }

    private void FixedUpdate()
    {
        if (_rb != null)
        {
            transform.rotation = Quaternion.Euler(0, 0, _rb.linearVelocity.y * _rotationSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_isGameOver || other.GetComponent<SceneLoader>() != null) return;

        _score++;
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + _score;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (_isGameOver || other.gameObject.GetComponent<SceneLoader>() != null) return;

        _isGameOver = true;
        Time.timeScale = 0f;
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
