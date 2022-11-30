using TD_Model;
using UnityEngine;
using UnityEngine.UI;

namespace TD_UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private Text scoreText;

        protected void Update()
        {
            scoreText.text = GameManager.Instance.Scoring.Score.ToString("G4");
        }
    }
}
