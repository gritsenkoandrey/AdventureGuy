using UnityEngine;


public class Checkpoints : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            //Character._playerPosition = collision.transform.position;
            //Destroy(gameObject);
        }
    }
}