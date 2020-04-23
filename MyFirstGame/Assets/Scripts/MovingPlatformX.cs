using UnityEngine;


public class MovingPlatformX : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _point;

    [SerializeField] private float _speed;
    [SerializeField] private float _range;

    private bool _isMovingRight;

    #endregion


    #region UnityMethod

    private void Update()
    {
        MovingPlatform();
    }

    #endregion


    #region Method

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

    #endregion
}