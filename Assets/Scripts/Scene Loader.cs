using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneLoader : MonoBehaviour
{
    public enum ButtonAction
    {
        StartGame,
        ShowMenu,
        LoadScene
    }

    [SerializeField] private ButtonAction _action = ButtonAction.StartGame;
    public string targetScene = "Ingame";

    private Collider2D _col;
    private int _lastClickFrame = -1;

    public void SetAction(ButtonAction action)
    {
        _action = action;
    }

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        bool clicked = false;
        Vector2 screenPos = Vector2.zero;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            clicked = true;
            screenPos = Mouse.current.position.ReadValue();
        }
        else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            clicked = true;
            screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
            screenPos = Input.mousePosition;
        }

        if (clicked && Camera.main != null && _col != null)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            if (_col.OverlapPoint(worldPos))
            {
                ExecuteAction();
            }
        }
    }

    private void OnMouseDown()
    {
        ExecuteAction();
    }

    public void ExecuteAction()
    {
        if (Time.frameCount == _lastClickFrame) return;
        _lastClickFrame = Time.frameCount;

        if (_action == ButtonAction.StartGame)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.StartGame();
                return;
            }
        }
        else if (_action == ButtonAction.ShowMenu)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ShowStartMenu();
                return;
            }
        }

        // Fallback: Load target scene if GameManager is not found
        Time.timeScale = 1f;
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
    }

    public void LoadTargetScene()
    {
        ExecuteAction();
    }
}
