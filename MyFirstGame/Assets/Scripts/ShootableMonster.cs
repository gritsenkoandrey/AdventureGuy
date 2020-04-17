using UnityEngine;


public class ShootableMonster : Monster
{
    #region Fields

    [SerializeField] private float _rate = 2.0f;

    private Bullet _bullet;
    private SpriteRenderer _sprite;

    private Color _bulletColor = Color.green;
    private Vector3 _direction;
    private Vector3 _position;

    #endregion


    #region UnityMethods

    protected override void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _bullet = Resources.Load<Bullet>("Bullet");
    }

    protected override void Start()
    {
        InvokeRepeating(nameof(Shoot), _rate, _rate);
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        // этого монстра можно убить только прыгнув на него сверху
        // Mathf.Abs - модуль числа, если мы заходим слева при Х = -число, то модуль Х = +число
        var unit = collider.GetComponent<Unit>();
        if (unit && unit as Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.5f)
                ReceiveDamage();
            else 
                unit.ReceiveDamage();
        }
    }

    #endregion


    #region Methods

    private void Shoot()
    {
        _direction = -transform.right * (_sprite.flipX ? -1 : 1);
        _position = transform.position;
        // из-за инверсии спрайта _direction идет наоборот
        if (_direction.x > 0)
        {
            _position.x += 1;
        }
        else
        {
            _position.x -= 1;
        }

        var newBullet = Instantiate(_bullet, _position, _bullet.transform.rotation);
        newBullet.Direction = -newBullet.transform.right * (_sprite.flipX ? -1 : 1);
        //newBullet.Parent = gameObject;
        newBullet.Color = _bulletColor;
    }

    #endregion
}