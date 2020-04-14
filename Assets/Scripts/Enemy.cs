using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Set dynamically
    [HideInInspector]
    public GameObject[]     route;

    [HideInInspector]
    public int              damage;

    [HideInInspector]
    public int              reward;

    [HideInInspector]
    public GameController   gameController;

    private SpriteRenderer  _sr;
    private float           _speed = 2.0f;
    private int             _currentPoint = 0;
    private float           _pointSwitchTime;
    private int             _health = 1;
    public int Health
    {
        get { return _health; }
        set
        { 
            _health = value;
            _hitTime = Time.time;
            _sr.color = _hitColor;
            if (_health <= 0)
            {
                gameController.ActiveEnemies--;
                gameController.Score++;
                gameController.Gold += reward;
                Destroy(gameObject);
            }
        }
    }

    private Vector3         _startPos = new Vector3();
    private Vector3         _endPos = new Vector3();

    //Hit Animation Vars
    private float           _hitTime;
    private Color           _idleColor = Color.white;
    private Color           _hitColor = Color.red;
    private float           _hitDuration = 0.1f;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _pointSwitchTime = Time.time;
    }

    void Update()
    {
        if (_sr.color == _hitColor && Time.time - _hitTime > _hitDuration)
            _sr.color = _idleColor;

        _startPos = route[_currentPoint].transform.position;
        _endPos = route[_currentPoint + 1].transform.position;

        float pathLength = Vector3.Distance(_startPos, _endPos);
        float time = pathLength / _speed;
        float curTime = Time.time - _pointSwitchTime;
        gameObject.transform.position = Vector2.Lerp(_startPos, _endPos, (curTime / time) );

        if (gameObject.transform.position == _endPos)
        {            
            if (_currentPoint < route.Length - 2)
            {
                _currentPoint++;
                _pointSwitchTime = Time.time;
            }
            else
            {
                gameController.Health -= damage;
                gameController.ActiveEnemies--;
                Destroy(gameObject);                
            }
        }
    }
}
