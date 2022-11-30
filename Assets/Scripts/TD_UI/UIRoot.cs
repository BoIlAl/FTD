using TD_Model;
using UnityEngine;
using UnityEngine.UI;

namespace TD_UI 
{
    public class UIRoot : MonoBehaviour
    {
        private static UIRoot instance;

        [SerializeField] private TowerInfoBehavior towerInfoBehavior;
        [SerializeField] private CurrentWaveInfoBehavior currentWaveInfoBehavior;
        [SerializeField] private GameObject towerSelectionObject;
        [SerializeField] private Image towerSelectionImage;
        [SerializeField] private GameObject hideUIButton;
        [SerializeField] private HealthBarScript healthBar;

        public TowerInfoBehavior TowerInfoBehavior => towerInfoBehavior;
        public CurrentWaveInfoBehavior CurrentWaveInfoBehavior => currentWaveInfoBehavior;
        public static UIRoot Instance => instance;
        public Vector3 LastClick { set; get; }
        public PlatformBehaviour LastClickedPlatform { set; get; }
        public GameObject TowerSelectionObject => towerSelectionObject;
        public Image TowerSelectionImage => towerSelectionImage;
        public GameObject HideUIButton => hideUIButton;

        protected void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
            GameManager.Instance.MainTower.HealthBar.gameObject.SetActive(false);
            GameManager.Instance.MainTower.HealthBar = healthBar;
        }
    }
}
