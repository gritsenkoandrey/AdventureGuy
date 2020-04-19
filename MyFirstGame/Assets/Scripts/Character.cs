using UnityEngine;
using UnityEngine.SceneManagement;


public class Character : Unit
{
    #region Fields

    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _jumpForce = 6.5f;

    private float _extraJump;
    [SerializeField] private float _extraJumpValue;

    private float _plusJumpForce = 2.0f;
    private float _timeJumpForce = 3.0f;

    private int _currentHealth = 5;
    private int _maxHealth = 5;

    private LivesBar _livesBar;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private Bullet _bullet;

    private Vector3 _direction;
    private Vector3 _position;

    //checkground
    private bool _isGround;
    [SerializeField] private float _checkRadius;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;

    //platform
    private int _playerObject;
    private int _colliderObject;
    [SerializeField] private LayerMask _whatIsPlatform;

    #endregion


    #region Properities

    // свойство которое должно изменять количество жизней если оно изменяется
    // метод Refresh() при изменении жизней просит обновить UI
    internal int Health
    {
        get 
        {
            return _currentHealth;
        }
        set
        {
            if(value <= _maxHealth)
                _currentHealth = value;
            _livesBar.Refresh();
        }
    }

    internal float JumpForce
    {
        get 
        {
            return _jumpForce; 
        }
        set
        {
            if (_jumpForce < value)
                _jumpForce = value;
            Invoke(nameof(NormalJumpForce), _timeJumpForce);
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

    private void Start()
    {
        // определяем слои
        _playerObject = LayerMask.NameToLayer("Player");
        _colliderObject = LayerMask.NameToLayer("Platform");
    }

    private void FixedUpdate()
    {
        IgnoreLayerPlatform();
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
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var bullet = collider.gameObject.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
        {
            ReceiveDamage();
        }
    }

    // при попадании на  MovingPlatform, платформа является родителем персонажа
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var platformX = collision.collider.GetComponent<MovingPlatformX>();
        var platformY = collision.collider.GetComponent<MovingPlatformY>();
        var platformAngle = collision.collider.GetComponent<MovingPlatformAngle>();

        if (platformX || platformY || platformAngle)
        {
            transform.parent = collision.transform;
        }
        //if (collision.gameObject.tag.Equals("Platform"))
        //{
        //    transform.parent = collision.transform;
        //}
    }

    // при спрыгивании с платформы все условия обнуляются
    private void OnCollisionExit2D(Collision2D collision)
    {
        var platformX = collision.collider.GetComponent<MovingPlatformX>();
        var platformY = collision.collider.GetComponent<MovingPlatformY>();
        var platformAngle = collision.collider.GetComponent<MovingPlatformAngle>();

        if (platformX || platformY || platformAngle)
        {
            transform.parent = null;
        }
    }

    #endregion


    #region Methods

    private void Run()
    {
        if (Input.GetButton("Horizontal"))
        {
            var direction = transform.right * Input.GetAxis("Horizontal");
            var speed = _speed * Time.deltaTime;
            var position = transform.position;

            transform.position = Vector3.MoveTowards(position, transform.position + direction, speed);
            _sprite.flipX = direction.x < 0;
            if (_isGround)
                State = CharacterState.Run;
        }
    }

    private void Jump()
    {
        //if (_isGround && Input.GetButtonDown("Jump"))
        //{
        //    _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        //}

        if (_isGround == true)
        {
            _extraJump = _extraJumpValue;
        }

        // при каждом прыжке двойные прыжки уменьшаются
        if (Input.GetButtonDown("Jump") && _extraJump > 0)
        {
            _rigidbody.velocity = Vector2.up * _jumpForce;
            AudioSound._audioSound.AudioJumpCharacter();
            _extraJump--;
        }

        // чтобы первый прыжок не считался как дополнительный прыжок
        else if (Input.GetButtonDown("Jump") && _extraJump == 0 && _isGround == true)
        {
            _rigidbody.velocity = Vector2.up * _jumpForce;
            AudioSound._audioSound.AudioJumpCharacter();
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _direction = transform.right * (_sprite.flipX ? -1 : 1);
            _position = transform.position;

            if (_direction.x > 0)
            {
                _position.x += 1;
            }
            else
            {
                _position.x -= 1;
            }

            var newBullet = Instantiate(_bullet, _position, _bullet.transform.rotation);

            // при стрельбе мы являемся родителем пули и она нас не бьет
            //newBullet.Parent = gameObject;

            //задаем направление движения созданной пули
            newBullet.Direction = newBullet.transform.right * (_sprite.flipX ? -1 : 1);

            AudioSound._audioSound.AudioBulletCharacter();
        }
    }

    public override void ReceiveDamage()
    {
        //State = CharacterState.Hit;

        Health--;
        Bounce();

        // перекрашивает персонажа обратно в начальный цвет, через 0,5 сек
        _sprite.color = Color.red;
        Invoke(nameof(ColorWhite), 1f);
        if (_currentHealth <= 0)
        {
            Die();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }
    }

    private void ColorWhite()
    {
        _sprite.color = Color.white;
    }

    //private void CheckGround()
    //{
    //    var colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
    //    _isGround = colliders.Length > 1;

    //    if (_isGround == false)
    //        State = CharacterState.Jump;
    //}

    private void CheckGround()
    {
        _isGround = (Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround))
            || (Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsPlatform));

        if (_isGround == false)
            State = CharacterState.Jump;
    }

    // игнорируем слои для того чтобы снизу запрыгивать на платформу
    private void IgnoreLayerPlatform()
    {
        if(_rigidbody.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(_playerObject, _colliderObject, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(_playerObject, _colliderObject, false);
        }
    }

    // персонажа подбрасывает вверх
    private void Bounce()
    {
        // обнуляет силу притяжения при подении, чтобы на ловушке подбросило
        _rigidbody.velocity = Vector3.zero;
        // при получении урона отбрасывает вверх
        _rigidbody.AddForce(transform.up * 4, ForceMode2D.Impulse);
    }

    private void NormalJumpForce()
    {
        _jumpForce -= _plusJumpForce;
    }

    #endregion
}