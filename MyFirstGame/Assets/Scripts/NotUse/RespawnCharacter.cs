using UnityEngine;
using System.Collections;


public class RespawnCharacter : MonoBehaviour
{
    // координаты с последней контрольной точки
    internal static Vector3 _playerPosition;

    // игрок мертв или нет
    private static bool _isPlayerDead;

    // координаты персонажа
    [SerializeField] private Transform _character;
    // вектор смещения, чтобы персонаж не провалился в текстуры
    [SerializeField] private Vector3 _offset;

    private void Awake()
    {
        _isPlayerDead = false;
    }

    private void Update()
    {
        if (_isPlayerDead)
        {
            _isPlayerDead = false;
            Spawn();
        }
    }

    public void Spawn()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        Instantiate(_character, _playerPosition + _offset, Quaternion.identity);
    }
}