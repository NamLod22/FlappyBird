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

    void Start()
    {
        Time.timeScale = 1f;
        _rb = GetComponent<Rigidbody2D>();
        _score = 0;
        _isGameOver = false;

        if (_scoreText != null)
        {
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

        if ((Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame))
        {
            _rb.linearVelocity = Vector2.up * _velocity;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, _rb.linearVelocity.y * _rotationSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_isGameOver) return;

        _score++;
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + _score;
        }
       
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (_isGameOver) return;

        _isGameOver = true;
        Time.timeScale = 0f;
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(true);
        }
       
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
