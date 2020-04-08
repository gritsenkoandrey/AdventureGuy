using UnityEngine;
using UnityEngine.SceneManagement;


public class Character : Unit
{
    #region Fields

    [SerializeField] private float _speed = 4F;
    [SerializeField] private float _jumpForce = 6.5F;

    private float _plusJumpForce = 2F;

    private int _currentHealth = 5;
    private int _maxHealth = 5;

    private bool _isGround = false;
    private bool _isPaused = false;

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
        get { return _currentHealth; }
        set
        {
            if(value <= _maxHealth)
                _currentHealth = value;
            _livesBar.Refresh();
        }
    }

    public float JumpForce
    {
        get { return _jumpForce; }
        set
        {
            if (_jumpForce < value) _jumpForce = value;
            Invoke(nameof(NormalJumpForce), 3.0F);
        }
    }

    private CharacterState State
    {
        get { return (CharacterState)_animator.GetInteger("State"); }
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
            State = CharacterState.Idle;

        Fire();
        Run();
        Jump();
        GamePause();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        var bullet = collider.gameObject.GetComponent<Bullet>();
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
                State = CharacterState.Run;
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
            var newBullet = Instantiate(_bullet, position, _bullet.transform.rotation);
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
        if (_currentHealth <= 0)
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
        if (!_isGround) State = CharacterState.Jump;
    }

    private void GamePause()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!_isPaused)
            {
                Time.timeScale = 0F;
                _isPaused = true;
            }
            else
            {
                Time.timeScale = 1F;
                _isPaused = false;
            }
        }
    }

    private void NormalJumpForce()
    {
        _jumpForce -= _plusJumpForce;
        Debug.Log("Jump is normal");
    }

    #endregion
}