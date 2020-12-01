using UnityEngine;


public class FlyingMonster : Monster
{
    #region Fields

    [SerializeField] private Transform _point;
    private SpriteRenderer _sprite;

    private Vector2 _position;
    private Vector2 _direction;

    [SerializeField] private float _speed;
    [SerializeField] private float _range;

    private bool _isMovingRight = true;

    #endregion


    #region UnityMethod

    protected override void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        MoveMonster();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        var bullet = collider.GetComponent<Bullet>();
        if (bullet)
        {
            ReceiveDamage();
            Explosion();
        }

        var unit = collider.GetComponent<Unit>();
        if (unit && unit as Character)
        {
            unit.ReceiveDamage();
        }
    }

    #endregion


    #region Method

    private void MoveMonster()
    {
        var speed = _speed * Time.deltaTime;
        _direction = transform.right;
        _position = transform.position;

        if (transform.position.x > _point.position.x + _range)
        {
            _isMovingRight = false;
            _sprite.flipX = _direction.x > 0;
        }

        else if (transform.position.x < _point.position.x - _range)
        {
            _isMovingRight = true;
            _sprite.flipX = _direction.x < 0;
        }

        if (_isMovingRight)
        {
            transform.position = new Vector2(_position.x + speed, _position.y);
        }
        else
        {
            transform.position = new Vector2(_position.x - speed, _position.y);
        }
    }

    #endregion
}