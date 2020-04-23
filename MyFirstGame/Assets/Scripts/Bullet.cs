using UnityEngine;


public class Bullet : MonoBehaviour
{
    #region Fields

    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _lifeTime = 0.8f;

    private Vector3 _direction;

    private SpriteRenderer _sprite;
    private GameObject _parent;

    #endregion


    #region Properities

    public Vector3 Direction
    {
        get { return _direction; }
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

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
                transform.position + _direction, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var unit = collider.GetComponent<Unit>();

        if (unit && unit.gameObject != _parent)
        {
            // если пуля не принадлежит объекту, который ее создал, то она наносит урон:
            Destroy(gameObject);
        }
    }

    #endregion
}