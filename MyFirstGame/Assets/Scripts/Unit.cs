using UnityEngine;


public class Unit : MonoBehaviour
{
    private Rigidbody2D _rigidbody;


    #region Methods

    public virtual void ReceiveDamage()
    {
        Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    #endregion
}