using UnityEngine;
using System.Linq;


public class MoveableMonster : Monster
{
    #region Fields
    [SerializeField] private float _speed = 2.0F;

    private SpriteRenderer _sprite;
    private Vector3 _direction;
    #endregion


    #region UnityMethods
    protected override void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
    protected override void Start()
    {
        _direction = transform.right;
    }

    protected override void Update()
    {
        Move();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        // этого монстра можно убить только прыгнув на него сверху
        // Mathf.Abs - модуль числа, если мы заходим слева при Х = -число, то модуль Х = +число
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit as Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.5F)
                ReceiveDamage();
            else unit.ReceiveDamage();
        }
    }
    #endregion


    #region Methods
    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            transform.position + transform.up * 0.1F + transform.right * _direction.x * 0.55F, 0.1F);
        // если в массиве нет элемента, который принадлежит персонажу, то меняем направление движения
        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<Character>())) _direction *= -1.0F;

        transform.position = Vector3.MoveTowards(transform.position, 
                transform.position + _direction, _speed * Time.deltaTime);
    }
    #endregion
}