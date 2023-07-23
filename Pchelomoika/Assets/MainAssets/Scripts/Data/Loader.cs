using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private void Start()
    {
        //Time.timeScale = 1.0f;
    }
    public void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
