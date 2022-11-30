using TD_UI;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace TD_Model
{
    public class GameOverTrigger : MonoBehaviour
    {
        
        [SerializeField] private float gameOverTime;
        [SerializeField] private PlayableDirector playableDirector;
        [SerializeField] private GameObject deathScreen;

        private float endTime;
        private bool endGameFlag;
        private bool wavesEndFlag = false;

        protected void Start()
        {
            endGameFlag = false;
            endTime = gameOverTime;
            
        }
        protected void Update()
        {
            if (wavesEndFlag && GameManager.Instance.GetNCreeps() == 0)
            {
                SceneManager.LoadScene(3);
                return;
            }

            if (endTime < 0)
            {
                SceneManager.LoadScene(3);
                return;
            }

            if (endGameFlag)
            {
                endTime -= Time.deltaTime;
            }

            if (GameManager.Instance.MainTower == null)
            {
                GameManager.Instance.SellAll();
                UIRoot.Instance.GetComponent<HideButtonBehaviour>().Hide();
                endGameFlag = true;
                deathScreen.SetActive(true);
                playableDirector.Play();
                endTime -= Time.deltaTime;
            }
        }

        public void SetWavesEndTrigger()
        {
            wavesEndFlag = true;
        }
    }
}
