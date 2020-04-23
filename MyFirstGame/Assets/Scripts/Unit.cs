using UnityEngine;


public class Unit : MonoBehaviour
{
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