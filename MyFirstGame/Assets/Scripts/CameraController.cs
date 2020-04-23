using UnityEngine;


public class CameraController : MonoBehaviour
{
    #region Fields

    [SerializeField] private float _speed = 2.0f;

    [SerializeField] private Transform _target;
    // смещение камеры по оси Z
    private float _transformPositionZ = -10.0f;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        // если цель не выбрана, то находит тип персонажа и следит за ним
        if (!_target)
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            //_target = FindObjectOfType<Character>().transform;
        }
    }

    private void Update()
    {
        // перемещает камеру за персонажем
        Vector3 position = _target.position;
        position.z = _transformPositionZ;
        transform.position = Vector3.Lerp(transform.position, position, _speed * Time.deltaTime);
    }

    #endregion
}