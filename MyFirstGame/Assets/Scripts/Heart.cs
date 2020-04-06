using UnityEngine;


public class Heart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D healthCollider)
    {
        var character = healthCollider.GetComponent<Character>();
        if (character)
        {
            character.Health = character.Health + 1;
            Destroy(gameObject);
        }
    }
}