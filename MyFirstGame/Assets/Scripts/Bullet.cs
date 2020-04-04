using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10;

    private Vector3 _direction;
    private SpriteRenderer _sprite;
    private GameObject _parent;

    public Vector3 Direction
    {
        set { _direction = value; }
    }

    public GameObject Parent
    {
        get { return _parent; }
        set { _parent = value; }
    }

    // свойство для окрашивания пули
    public Color Color
    {
        set { _sprite.color = value; }
    }

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, 1.5F);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
                transform.position + _direction, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit.gameObject != _parent)
        {
            // если пуля не принадлежит созданному его объекту, то она наносит урон
            Destroy(gameObject);
        }
    }
}