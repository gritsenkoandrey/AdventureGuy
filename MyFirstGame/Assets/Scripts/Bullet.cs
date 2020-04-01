using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10;

    private Vector3 _direction;
    public Vector3 Direction { set { _direction = value; } }
    private SpriteRenderer _sprite;

    private GameObject _parent;
    public GameObject Parent { set { _parent = value; } }

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
            Destroy(gameObject);
        }
    }
}