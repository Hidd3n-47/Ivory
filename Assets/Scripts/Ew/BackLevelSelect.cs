using UnityEngine;
using UnityEngine.SceneManagement;

public class BackLevelSelect : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
