using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Set in Inspector
    public Way              way;
    public GameObject       enemyPrefab;
    public GameController   gameController;
    public TextAsset        configFile;

    //Set dynamically
    private int     _currentWave;
    private int     _baseEnemyCount = 5; 
    private int     _enemyCount;
    private int     _enemyCreated = 0;

    //private int _enemyDamage = 2;
    //private int _enemyHealth = 4;
    //private int _enemyReward = 20;
    private int[]   _enemyStats = { 2, 4, 20 }; //damage, health, reward

    private float   _waveStarted;
    private float   _lastEnemySpawned;
    private float   _enemyDelay = 1.2f;
    private float   _waveDelay = 2f;

    public void CreateNewWave()
    {
        _waveStarted = Time.time;
        _currentWave = gameController.CurrentWave;
        _enemyCount = Random.Range(_currentWave + 1, _currentWave + _baseEnemyCount);//случайная величина от K до K + X
        _enemyCreated = 0;

        //Каждую волну одна или несколько характеристик противников повышаются.
        int upgradeCount = Random.Range(0, _enemyStats.Length); //от 1 до 3 характеристик
        for (int i = 0; i <= upgradeCount; i++)
            _enemyStats[i]++;
    }

    void Start()
    {
        //Время между активацией волн задается в конфигурационном файле игры
        _waveDelay = XMLReader.ReadIntervalFromFile(configFile);
        if (_waveDelay == -1f)
            _waveDelay = 2f; //default value
        CreateNewWave();
    }

    void Update()
    {
        if (Time.time - _waveStarted > _waveDelay)
        {
            if (_enemyCreated <= _enemyCount)
            {
                if (Time.time - _lastEnemySpawned > _enemyDelay)
                {
                    CreateNewEnemy();
                }
            }
        }
    }

    private void CreateNewEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab);
        Enemy enemyComp = newEnemy.GetComponent<Enemy>();
        enemyComp.route = way.enemyRoute;
        enemyComp.gameController = gameController;
        enemyComp.damage = _enemyStats[0];
        enemyComp.Health = _enemyStats[1];        
        enemyComp.reward = _enemyStats[2];
        gameController.ActiveEnemies++;
        _enemyCreated++;
        _lastEnemySpawned = Time.time;
    }
}
