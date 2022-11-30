using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public void PlayClassic() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayAlternative() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
