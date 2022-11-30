using TD_Model;
using TMPro;
using UnityEngine;

public class GameOverScoreGetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    void Start()
    {
        scoreText.text = Scoring.MaxSessionScore.ToString("G5");
    }
}
