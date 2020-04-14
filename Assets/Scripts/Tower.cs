using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerLevel
{
    public int cost;
    public int damage;
    public float shotDelay;
    public Color towerColor;
}

public class Tower : MonoBehaviour
{
    //Set in Inspector
    public TowerLevel[]         levels;    
    
    //Set Dynamically
    private GameController      _gameController;
    private SpriteRenderer      _sr;    
    private List<GameObject>    _targets = new List<GameObject>();
    private float               _lastShotTime;
    private int                 _currentLevel;

    public void UpgradeTower()
    {
        if (_currentLevel < levels.Length - 1)
        {
            if (_gameController.Gold >= levels[_currentLevel + 1].cost)
            {
                _currentLevel++;
                _sr.color = levels[_currentLevel].towerColor;
                _gameController.Gold -= levels[_currentLevel].cost;
            }
        }
    }

    void Awake()
    {
        _currentLevel = 0;
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = levels[_currentLevel].towerColor;
        _gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        if (_targets.Count != 0 && Time.time - _lastShotTime > levels[_currentLevel].shotDelay)
        {
            GameObject target = null;
            foreach (GameObject go in _targets)
            {
                if (go != null)
                {
                    target = go;
                    break;
                }
            }
            if (target != null)
                Shoot(target);
        }        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _targets.Add(other.gameObject);        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _targets.Remove(other.gameObject);
    }

    private void Shoot(GameObject target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            _lastShotTime = Time.time;
            enemy.Health -= levels[_currentLevel].damage;
        }        
    }
}
