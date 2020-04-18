using UnityEngine;


public class MovingPlatformX : MonoBehaviour
{
    [SerializeField] private Transform _point;

    [SerializeField] private float _speed;
    [SerializeField] private float _range;

    private bool _isMovingRight;

    private void Update()
    {
        MovingPlatform();
    }

    private void MovingPlatform()
    {
        if (transform.position.x > _point.position.x + _range)
        {
            _isMovingRight = false;
        }

        else if (transform.position.x < _point.position.x - _range)
        {
            _isMovingRight = true;
        }

        if (_isMovingRight)
        {
            transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - _speed * Time.deltaTime, transform.position.y);
        }
    }
}