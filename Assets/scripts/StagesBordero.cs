using UnityEngine;
using UnityEngine.SceneManagement;

public class StagesBordero : MonoBehaviour
{
    public string SceneName;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}