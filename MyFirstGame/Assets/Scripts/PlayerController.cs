using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jump;

    // run
    private float _moveInput;
    private bool _isFacingRight = true;
    private Rigidbody2D _rigidbody;

    // check ground
    private bool _isGrounded;
    [SerializeField] private float _checkRadius;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;

    // jump
    private int _extraJump;
    // количество двойных прыжков
    [SerializeField] private int _extraJumpValue;

    // for anim is check ground
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _extraJump = _extraJumpValue;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        Run();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("MovingPlatform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    // при спрыгивании с платформы все условия обнуляются
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("MovingPlatform"))
        {
            this.transform.parent = null;
        }
    }

    private void Run()
    {
        // _moveInput = направление движения объекта, если стрелка вправо то +1, влево -1
        _moveInput = Input.GetAxis("Horizontal");

        _rigidbody.velocity = new Vector2(_moveInput * _speed, _rigidbody.velocity.y);

        if (_isFacingRight == false && _moveInput > 0)
        {
            Flip();
        }
        else if (_isFacingRight == true && _moveInput < 0)
        {
            Flip();
        }
    }

    private void Jump()
    {
        // если мы приземляемся на землю, то двойные прыжки возобновляются
        if (_isGrounded == true)
        {
            _extraJump = _extraJumpValue;
        }
        // при каждом прыжке двойные прыжки уменьшаются
        if (Input.GetButtonDown("Jump") && _extraJump > 0)
        {
            _rigidbody.velocity = Vector2.up * _jump;
            _extraJump--;
        }
        // чтобы первый прыжок не считался как дополнительный прыжок
        else if (Input.GetButtonDown("Jump") && _extraJump == 0 && _isGrounded == true)
        {
            _rigidbody.velocity = Vector2.up * _jump;
        }
        AnimIsJumping();
    }

    private void Flip()
    {
        // проверка на поворот
        _isFacingRight = !_isFacingRight;
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
    }

    private void AnimIsJumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _animator.SetBool("isJumping", true);
        }
        else
        {
            if (_isGrounded == true)
            {
                _animator.SetBool("isJumping", false);
            }
        }
    }
}