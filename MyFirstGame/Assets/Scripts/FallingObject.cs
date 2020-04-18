using UnityEngine;


public class FallingObject : MonoBehaviour
{
    #region Fields

    private Rigidbody2D _rigidbody;
    private Character _character;

    [SerializeField] private float _lifeTimeObject = 1.0f;
    [SerializeField] private float _lifeTimeObjectAfterCollision = 0.2f;

    #endregion


    #region UnityMethod

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // при пересечении линии, Kinematic меняется на Dynamic, и на объект начинает действовать гравитация
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _character = collision.GetComponent<Character>();
        if (_character)
        {
            _rigidbody.isKinematic = false;
            Destroy(gameObject, _lifeTimeObject);
        }

        //if (collision.gameObject.tag.Equals("Player"))
        //{
        //    _rigidbody.isKinematic = false;
        //}
    }

    // поведение объекта после падения
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _character = collision.collider.GetComponent<Character>();
        if (_character)
        {
            _character.ReceiveDamage();
            Destroy(gameObject, _lifeTimeObjectAfterCollision);
        }
    }

    #endregion
}