using TD_Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TD_UI
{
    public class GameOverButtonsBehaviour : MonoBehaviour
    {
        public void QuitToMenu() {
            SceneManager.LoadScene(0);
        }
    }
}
