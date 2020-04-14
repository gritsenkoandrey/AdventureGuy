using UnityEngine;
using UnityEngine.SceneManagement;


public class NextScene : MonoBehaviour
{
    #region Fields

    [SerializeField] private int _loadingScene;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D nextLevelCollider)
    {
        var character = nextLevelCollider.GetComponent<Character>();
        if(character)
        {
            character.AudioGetCoin();




            Invoke(nameof(LoadScene), 0.5f);
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(_loadingScene);
    }

    #endregion
}