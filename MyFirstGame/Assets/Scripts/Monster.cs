using UnityEngine;


public class Monster : Unit
{
    #region UnityMethods
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }
    #endregion


    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        var bullet = collider.GetComponent<Bullet>();
        if (bullet)
            ReceiveDamage();

        var unit = collider.GetComponent<Unit>();
        if (unit && unit as Character)
        {
            unit.ReceiveDamage();
        }
    }
}