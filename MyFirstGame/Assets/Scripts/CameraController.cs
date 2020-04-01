using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0F;
    [SerializeField] private Transform _target;

    private void Awake()
    {
        // если цель не выбрана, то находит тип персонажа и следит за ним
        if (!_target) _target = FindObjectOfType<Character>().transform;
    }

    private void Update()
    {
        // перемещает камеру за персонажем
        Vector3 position = _target.position;
        position.z = -10.0F;
        transform.position = Vector3.Lerp(transform.position, position, _speed * Time.deltaTime);
    }
}