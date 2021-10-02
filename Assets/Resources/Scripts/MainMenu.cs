using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButton);
    }

    private void OnStartButton()
    {
        SceneManager.LoadScene(1);
    }
}
