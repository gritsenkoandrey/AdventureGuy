using UnityEngine;


public class MovingPlatformAngle : MonoBehaviour
{
    #region Fields

    // точка вокруг которой будет двигаться платформа
    [SerializeField] private Transform _centerPointPlatform;

    // на каком расстоянии будет двигаться платформа
    [SerializeField] private float _radiusPlatform;

    // скорость платформы
    [SerializeField] private float _speedPlatform;

    private float _positionX = 0f;
    private float _positionY = 0f;
    private float _angle = 0f;

    #endregion


    #region UnityMethod

    void Update()
    {
        _angle += Time.deltaTime * _speedPlatform;
        if (_angle >= 360f)
        {
            _angle = 0f;
        }

        _positionX = _centerPointPlatform.position.x + Mathf.Cos(_angle) * _radiusPlatform;
        _positionY = _centerPointPlatform.position.y + Mathf.Sin(_angle) * _radiusPlatform;

        transform.position = new Vector2(_positionX, _positionY);
    }

    #endregion
}