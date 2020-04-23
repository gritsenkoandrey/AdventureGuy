using UnityEngine;


public class Monster : Unit
{
    #region UnityMethods

    [SerializeField] private Explosion _explosion;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        var bullet = collider.GetComponent<Bullet>();

        if (bullet)
        {
            ReceiveDamage();
            Explosion();
        }

        var unit = collider.GetComponent<Unit>();

        if (unit && unit as Character)
        {
            unit.ReceiveDamage();
        }
    }

    protected virtual void Explosion()
    {
        Instantiate(_explosion, transform.position, transform.rotation);
    }

    #endregion
}