using UnityEngine;
using UnityEngine.SceneManagement;


public class NextScene : MonoBehaviour
{
    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D nextLevelCollider)
    {
        var character = nextLevelCollider.GetComponent<Character>();
        if(character)
        {
            SceneManager.LoadScene(1);
        }
    }

    #endregion
}