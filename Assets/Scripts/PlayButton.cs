using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("Gameplay");
    }
}