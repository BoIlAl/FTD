using UnityEngine;

namespace TD_Model
{
    public class Scoring : MonoBehaviour
    {
        [SerializeField] private float maxScore;
        [SerializeField] private float startScore;
        public static float MaxSessionScore { get; set; } = 0;

        private float score;
        public float Score => score;

        public void Start()
        {
            score = startScore;
            MaxSessionScore = 0;
        }
        public bool ChangeProcessing(float change, bool isKill)
        {
            if (isKill)
            {
                MaxSessionScore += change;
            }
            if (score == maxScore)

            {
                return true;
            }

            if (score + change > maxScore)
            {
                score = maxScore;
                return true;
            }

            if (score + change < 0)
            {
                return false;
            }

            score += change;
            return true;
        }
    }
}