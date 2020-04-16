using UnityEngine;


public class PatrolMonster : MonoBehaviour
{
    #region Fields

    // скорость передвижения противника
    [SerializeField] private float _speed;
    // растояние от противника до игрока
    [SerializeField] private float _stoppingDistance;
    // на сколько клеток может патрулировать монстр
    [SerializeField] private int _positionOfPatrol;

    // если true то идем направо, если false то идем налево
    private bool _isMoveingRight = true;

    private bool _isIdle = false;
    private bool _isAngry = false;
    private bool _isGoBackPoint = false;

    // точка от которой происходит патрулирование + точка для возврата
    [SerializeField] private Transform _point;
    // расположение игрока
    private Transform _player;

    #endregion


    #region Unitymethod

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // если, расстояние от противника до точки патрулирования < _positionOfPatrol, то:
        if (Vector2.Distance(transform.position, _point.position) < _positionOfPatrol && _isAngry == false)
        {
            // патрулирование должно осуществляться, только если монстр не агресивен
            _isIdle = true;
        }
        if (Vector2.Distance(transform.position, _player.position) < _stoppingDistance)
        {
            // в случае агрессии противник прекращает патрулировать и не идет на точку
            _isAngry = true;
            _isIdle = false;
            _isGoBackPoint = false;
        }
        if (Vector2.Distance(transform.position, _player.position) > _stoppingDistance)
        {
            // при возвращении на точку агрессия монстра должна прекратится
            _isGoBackPoint = true;
            _isAngry = false;
        }

        State();
        //// состояния нужны для исключения конфликтов, чтобы противник не дергался на месте
        //if (_isIdle == true)
        //{
        //    Idle();
        //}
        //else if (_isAngry == true)
        //{
        //    Angry();
        //}
        //else if (_isGoBackPoint == true)
        //{
        //    GoBackPoint();
        //}
    }

    #endregion

    #region Method

    private void Idle()
    {
        // если расположение монстра > _point.position.x + _positionOfPatrol, то идем налево
        if (transform.position.x > _point.position.x + _positionOfPatrol)
        {
            _isMoveingRight = false;
        }
        // меняем курс направо
        else if (transform.position.x < _point.position.x - _positionOfPatrol)
        {
            _isMoveingRight = true;
        }

        // если не писать условие, то по умолчанию означает true
        if (_isMoveingRight)
        {
            // если условие верно, то идем направо
            transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            // если условие ложно, то идем налево
            transform.position = new Vector2(transform.position.x - _speed * Time.deltaTime, transform.position.y);
        }
    }

    private void Angry()
    {
        // MoveTowards - следование за целью
        transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
    }

    private void GoBackPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, _point.position, _speed * Time.deltaTime);
    }

    private void State()
    {
        // состояния нужны для исключения конфликтов, чтобы противник не дергался на месте
        if (_isIdle == true)
        {
            Idle();
        }
        else if (_isAngry == true)
        {
            Angry();
        }
        else if (_isGoBackPoint == true)
        {
            GoBackPoint();
        }
    }

    #endregion
}