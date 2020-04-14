using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Set in Inspector
    public EnemySpawner     enemySpawner;
    public TMP_Text         waveText;
    public TMP_Text         healthText;
    public TMP_Text         goldText;
    public GameObject       gameOverPanel;
    public TMP_Text         gameOverText;

    //Set dynamically
    private int _health;
    private int _maxHealth = 100;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                healthText.text = "Health 0/" + _maxHealth;
                GameOver();
            }
            else
            {
                healthText.text = "Health " + _health + "/" + _maxHealth;
            }
        }
    }

    private int _gold;
    public int Gold
    {
        get { return _gold; }
        set
        {
            _gold = value;
            goldText.text = "Gold " + _gold;
        }
    }

    private int _activeEnemies = 0;
    public int ActiveEnemies
    {
        get { return _activeEnemies; }
        set
        {
            _activeEnemies = value;
            if (_activeEnemies == 0 && !_gameOver)
            {
                CurrentWave++;
                enemySpawner.CreateNewWave();
            }
        }
    }

    private int _currentWave;
    public int CurrentWave
    {
        get { return _currentWave; }
        set
        {
            _currentWave = value;
            waveText.text = "Wave: " + (_currentWave + 1);
        }
    }

    private int _score = 0;
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
        }
    }

    private bool _gameOver = false;

    public void ReloadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
        CurrentWave = 0;
        Health = _maxHealth;
        Gold = 500;
    }

    private void GameOver()
    {
        _gameOver = true;
        gameOverText.text = "Enemies destroyed:\n" + Score;
        gameOverPanel.SetActive(true);
    }
}
