using UnityEngine;


public class MovingPlatformY : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _point;

    [SerializeField] private float _speed;
    [SerializeField] private float _range;

    private bool _isMovingUp;

    #endregion


    #region Propereties

    internal float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

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
        if (transform.position.y > _point.position.y + _range)
        {
            _isMovingUp = false;
        }

        else if (transform.position.y < _point.position.y - _range)
        {
            _isMovingUp = true;
        }

        if (_isMovingUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + _speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - _speed * Time.deltaTime);
        }
    }

    #endregion
}