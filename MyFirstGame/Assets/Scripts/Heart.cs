using UnityEngine;


public class Heart : MonoBehaviour
{
    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D healthCollider)
    {
        var character = healthCollider.GetComponent<Character>();
        if (character)
        {
            character.Health = character.Health + 1;
            Destroy(gameObject);
        }
    }

    #endregion
}