using UnityEngine;
using UnityEngine.SceneManagement;


public class Character : Unit
{
    #region Fields

    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _jumpForce = 6.5f;

    private float _plusJumpForce = 2.0f;
    private float _timeJumpForce = 3.0f;

    private int _currentHealth = 5;
    private int _maxHealth = 5;

    private bool _isGround = false;

    private LivesBar _livesBar;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private Bullet _bullet;

    private AudioSource _audio;
    [SerializeField] private AudioClip _audioClipBulletShot;
    [SerializeField] private AudioClip _audioClipJumpCharacter;
    [SerializeField] private AudioClip _audioClipHeart;
    [SerializeField] private AudioClip _audioClipJumpForce;
    [SerializeField] private AudioClip _audioClipCoin;

    private Vector3 _direction;

    #endregion


    #region Properities

    // свойство которое должно изменять количество жизней если оно изменяется
    // метод Refresh() при изменении жизней просит обновить UI
    internal int Health
    {
        get { return _currentHealth; }
        set
        {
            if(value <= _maxHealth)
                _currentHealth = value;
            _livesBar.Refresh();
        }
    }

    internal float JumpForce
    {
        get { return _jumpForce; }
        set
        {
            if (_jumpForce < value) _jumpForce = value;
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
        _audio = GetComponent<AudioSource>();
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
        if (_isGround && Input.GetButtonDown("Jump"))
        {
            _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);

            _audio.PlayOneShot(_audioClipJumpCharacter);
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _direction = transform.right * (_sprite.flipX ? -1 : 1);
            var position = transform.position;

            if (_direction.x > 0)
            {
                position.x += 1;
            }
            else
            {
                position.x -= 1;
            }

            var newBullet = Instantiate(_bullet, position, _bullet.transform.rotation);

            // при стрельбе мы являемся родителем пули и она нас не бьет
            //newBullet.Parent = gameObject;

            //задаем направление движения созданной пули
            newBullet.Direction = newBullet.transform.right * (_sprite.flipX ? -1 : 1);

            _audio.PlayOneShot(_audioClipBulletShot);
        }
    }

    public override void ReceiveDamage()
    {
        State = CharacterState.Hit;

        Health--;
        _rigidbody.velocity = Vector3.zero; // обнуляет силу притяжения при подении, чтобы на ловушке подбросило
        _rigidbody.AddForce(transform.up * 4, ForceMode2D.Impulse); // при получении урона отбрасывает вверх

        // перекрашивает персонажа обратно в начальный цвет, через 0,5 сек
        _sprite.color = Color.red;
        Invoke(nameof(ColorWhite), 1F);
        if (_currentHealth <= 0)
        {
            Die();
            SceneManager.LoadScene(3);
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