using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    private Vector3 _direction;
    public Vector3 Direction { set { _direction = value; } }
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
                transform.position + _direction, _speed * Time.deltaTime);
    }
}