using UnityEngine;


public enum CharacterState
{
    Idle = 0,
    Run = 1,
    Jump = 2
}
public class Character : Unit
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _jumpForce = 10;
    [SerializeField] private int _health = 5;

    private bool _isGround = false;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private Bullet _bullet;

    private CharacterState State
    {
        get { return (CharacterState)_animator.GetInteger("State"); }
        set { _animator.SetInteger("State", (int)value);}
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _bullet = Resources.Load<Bullet>("Bullet");
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
            Bullet newBullet = Instantiate(_bullet, position, _bullet.transform.rotation) as Bullet;
            // при стрельбе мы являемся родителем пули и она не уничтожается
            newBullet.Parent = gameObject;
            // задаем направление движения созданной пули
            newBullet.Direction = newBullet.transform.right * (_sprite.flipX ? -1 : 1);
        }
    }

    public override void ReceiveDamage()
    {
        _health--;
        _rigidbody.velocity = Vector3.zero; // обнуляет силу притяжения при подении, чтобы на ловушке подбросило
        _rigidbody.AddForce(transform.up * 4, ForceMode2D.Impulse);

        // перекрашивает персонажа обратно в начальный цвет, через 0,5 сек
        _sprite.color = Color.red;
        Invoke(nameof(ColorWhite), 0.5F);

        //Debug.Log(_health);
        if (_health <= 0)
            Die();
    }

    private void ColorWhite()
    {
        _sprite.color = Color.white;
    }

    private void CheckGround()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.8F);
        _isGround = colliders.Length > 1;

        // если мы не на земле, то проигрывается анимация Jump
        if (!_isGround) State = CharacterState.Jump;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.gameObject.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
        {
            ReceiveDamage();
        }
    }
}