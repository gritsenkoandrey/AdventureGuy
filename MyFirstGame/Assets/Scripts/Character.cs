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

    //private bool _isGround = false;
    private bool _isGround;

    private LivesBar _livesBar;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private Bullet _bullet;

    //private AudioSound _audioSound;

    private AudioSource _audio;

    [SerializeField] private AudioClip _audioClipBulletCharacter;
    [SerializeField] private AudioClip _audioClipJumpCharacter;
    [SerializeField] private AudioClip _audioClipHeart;
    [SerializeField] private AudioClip _audioClipJumpForce;
    [SerializeField] private AudioClip _audioClipCoin;

    private Vector3 _direction;
    private Vector3 _position;

    //checkground
    [SerializeField] private float _checkRadius;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;

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

    //internal int Hearth
    //{
    //    get 
    //    { 
    //        _audioSound.AudioGetHearth();
    //        return 0;
    //    }
    //    set => value = Hearth;
    //}

    internal float JumpForce
    {
        get 
        {
            //_audioSound.AudioGetPowerJump();
            return _jumpForce; 
        }
        set
        {
            if (_jumpForce < value)
                _jumpForce = value;
            Invoke(nameof(NormalJumpForce), _timeJumpForce);
        }
    }

    //internal int Coin
    //{
    //    get 
    //    {
    //        //_audioSound.AudioGetCoin();
    //        return 0;
    //    }
    //    set => value = Coin;
    //}

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
        _audio = GetComponent<AudioSource>();
        //_audioSound = GetComponent<AudioSound>();
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

        //    _audio.PlayOneShot(_audioClipJumpCharacter);
        //    //_audioSound.AudioJumpCharacter();
        //}

        if (_isGround == true)
        {
            _extraJump = _extraJumpValue;
        }

        // при каждом прыжке двойные прыжки уменьшаются
        if (Input.GetButtonDown("Jump") && _extraJump > 0)
        {
            _rigidbody.velocity = Vector2.up * _jumpForce;
            _audio.PlayOneShot(_audioClipJumpCharacter);
            _extraJump--;
        }
        // чтобы первый прыжок не считался как дополнительный прыжок
        else if (Input.GetButtonDown("Jump") && _extraJump == 0 && _isGround == true)
        {
            _rigidbody.velocity = Vector2.up * _jumpForce;
            _audio.PlayOneShot(_audioClipJumpCharacter);
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

            _audio.PlayOneShot(_audioClipBulletCharacter);
            //_audioSound.AudioBulletCharacter();
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
    //    var colliders = Physics2D.OverlapCircleAll(transform.position, 0.75f); //0.8F
    //    _isGround = colliders.Length > 1;

    //    // если мы не на земле, то проигрывается анимация Jump
    //    if (!_isGround) State = CharacterState.Jump;
    //}

    private void CheckGround()
    {
        _isGround = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);

        if (_isGround == false)
            State = CharacterState.Jump;
    }

    internal void Bounce()
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

    internal void AudioGetHearth()
    {
        _audio.PlayOneShot(_audioClipHeart);
    }

    internal void AudioGetPowerJump()
    {
        _audio.PlayOneShot(_audioClipJumpForce);
    }

    internal void AudioGetCoin()
    {
        _audio.PlayOneShot(_audioClipCoin);
    }

    #endregion
}