using UnityEngine;
using UnityEngine.SceneManagement;


public class NextScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D nextLevelCollider)
    {
        var character = nextLevelCollider.GetComponent<Character>();
        if(character)
        {
            SceneManager.LoadScene("YouWin");
        }
    }
}