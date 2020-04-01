using UnityEngine;


public class Character : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private int _health = 5;
    [SerializeField] private float _jumpForce = 10.0F;
    private bool _isGround = false;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _sprite;

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
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (_isGround) State = CharacterState.Idle;

        if(Input.GetButton("Horizontal")) Run();
        if(_isGround && Input.GetButtonDown("Jump")) Jump();
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position,
                transform.position + direction, _speed * Time.deltaTime);

        _sprite.flipX = direction.x < 0;

        if (_isGround) State = CharacterState.Run;
    }

    private void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.8F);
        _isGround = colliders.Length > 1;

        if (!_isGround) State = CharacterState.Jump;
    }
}

public enum CharacterState
{
    Idle = 0,
    Run = 1,
    Jump = 2
}