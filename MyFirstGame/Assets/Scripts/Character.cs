using UnityEngine;
using UnityEngine.SceneManagement;


public class Character : Unit
{
    #region Fields

    [SerializeField] private float _speed = 3;
    [SerializeField] private float _jumpForce = 10;
    [SerializeField] private int _health = 5;

    private bool _isGround = false;

    private LivesBar _livesBar;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private Bullet _bullet;

    #endregion


    #region Properities

    // свойство которое должно изменять количество жизней если оно изменяется
    // метод Refresh() при изменении жизней просит обновить UI
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            if(value <= 5) _health = value;
            _livesBar.Refresh();
        }
    }

    private CHARACTER_STATE State
    {
        get { return (CHARACTER_STATE)_animator.GetInteger("State"); }
        set { _animator.SetInteger("State", (int)value); }
    }

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _bullet = Resources.Load<Bullet>("Bullet");
        _livesBar = FindObjectOfType<LivesBar>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        // когда стоим на твердой поверхности проигрывается анимация Idle
        if (_isGround)
            State = CHARACTER_STATE.Idle;

        Fire();
        Run();
        Jump();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.gameObject.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
        {
            ReceiveDamage();
        }
    }

    #endregion


    #region Methods

    private void Run()
    {
        if (Input.GetButton("Horizontal"))
        {
            Vector3 direction = transform.right * Input.GetAxis("Horizontal");
            transform.position = Vector3.MoveTowards(transform.position,
                transform.position + direction, _speed * Time.deltaTime);

            _sprite.flipX = direction.x < 0;

            if (_isGround)
                State = CHARACTER_STATE.Run;
        }
    }

    private void Jump()
    {
        if (_isGround && Input.GetButtonDown("Jump"))
        {
            _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 position = transform.position;
            position.y += 0;
            Bullet newBullet = Instantiate(_bullet, position, _bullet.transform.rotation) as Bullet;
            // при стрельбе мы являемся родителем пули и она не уничтожается
            newBullet.Parent = gameObject;
            // задаем направление движения созданной пули
            newBullet.Direction = newBullet.transform.right * (_sprite.flipX ? -1 : 1);
        }
    }

    public override void ReceiveDamage()
    {
        //State = CHARACTER_STATE.Hit;
        // уменьшаем свойство иначе UI работать не будет
        Health--;
        _rigidbody.velocity = Vector3.zero; // обнуляет силу притяжения при подении, чтобы на ловушке подбросило
        _rigidbody.AddForce(transform.up * 4, ForceMode2D.Impulse); // при получении урона отбрасывает вверх

        //// перекрашивает персонажа обратно в начальный цвет, через 0,5 сек
        _sprite.color = Color.red;
        Invoke(nameof(ColorWhite), 1F);
        if (_health <= 0)
        {
            Die();
            SceneManager.LoadScene("GameOver");
        }
    }

    private void ColorWhite()
    {
        _sprite.color = Color.white;
    }

    private void CheckGround()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.75F); //0.8F
        _isGround = colliders.Length > 1;

        // если мы не на земле, то проигрывается анимация Jump
        if (!_isGround) State = CHARACTER_STATE.Jump;
    }

    #endregion
}