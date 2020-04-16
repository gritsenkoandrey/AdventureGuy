using UnityEngine;


public class ShootableMonster : Monster
{
    #region Fields

    [SerializeField] private float _rate = 2.0f;

    private Bullet _bullet;
    private Color _bulletColor = Color.red;
    private Vector3 _direction;
    private SpriteRenderer _sprite;

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
            else unit.ReceiveDamage();
        }
    }

    #endregion


    #region Methods

    private void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.2f;
        var newBullet = Instantiate(_bullet, position, _bullet.transform.rotation);
        
        newBullet.Parent = gameObject;
        newBullet.Direction = -newBullet.transform.right;
        newBullet.Color = _bulletColor;
    }

    #endregion
}