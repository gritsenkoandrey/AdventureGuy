using UnityEngine;


public class Heart : MonoBehaviour
{
    #region Fields

    private int _healthPlus = 1;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D healthCollider)
    {
        var character = healthCollider.GetComponent<Character>();
        if (character)
        {
            character.Health = character.Health + _healthPlus;
            character.AudioGetHearth();
            Destroy(gameObject);
        }
    }

    #endregion
}