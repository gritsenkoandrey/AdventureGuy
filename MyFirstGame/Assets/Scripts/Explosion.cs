using UnityEngine;


public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionLifeTime = 1.0f;

    private void Start()
    {
        Destroy(gameObject, _explosionLifeTime);
    }
}