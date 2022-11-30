using System.Globalization;
using TD_Model;
using UnityEngine;
using UnityEngine.UI;

namespace TD_UI
{
    public class TowerInfoBehavior : MonoBehaviour
    {
        [SerializeField] private GameObject towerInfoPanel;
        [SerializeField] private Text towerName;
        [SerializeField] private Text damage;
        [SerializeField] private Text shotPerSecond;
        [SerializeField] private Text health;
        [SerializeField] private Text cost;
        [SerializeField] private Text upgradeCost;
        [SerializeField] private Image towerImage;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button sellButton;

        private string specifier;
        private TowerBehavior lastTowerBehavior;
        public TowerBehavior LastTowerBehavior => lastTowerBehavior;
        protected void Start()
        {
            specifier = "G4";
        }

        private void ChangeInfo()
        {
            towerName.text = lastTowerBehavior.Info.TowerName + " " + lastTowerBehavior.Info.TowerLvl.ToString(specifier);
            damage.text = lastTowerBehavior.Info.Damage.ToString();
            shotPerSecond.text = lastTowerBehavior.Info.ShotPerSec.ToString(specifier);
            health.text = lastTowerBehavior.Info.Health.ToString();
            cost.text = lastTowerBehavior.Info.Cost.ToString(specifier);
            towerImage.sprite = lastTowerBehavior.Info.TowerSprite;
            upgradeCost.text = lastTowerBehavior.Info.UpgradeCost.ToString(specifier);

            if (lastTowerBehavior.Info.TowerLvl == GameManager.Instance.MaxTowerLvl) {
                upgradeButton.gameObject.SetActive(false);
            }
            else {
                upgradeButton.gameObject.SetActive(true);
            }
        }

        public void Display(TowerBehavior towerBehavior)
        {

            if (towerBehavior == GameManager.Instance.MainTower)
            {
                sellButton.gameObject.SetActive(false);
            }
            else
            {
                sellButton.gameObject.SetActive(true);
            }

            lastTowerBehavior = towerBehavior;
            ChangeInfo();

            UIRoot.Instance.HideUIButton.gameObject.SetActive(true);
            towerInfoPanel.SetActive(true);
        }

        public void SellTower()
        {
            var platform = GameManager.Instance.GetPlatform(lastTowerBehavior);
            if (platform != null)
            {
                platform.ResetTower();
            }
            lastTowerBehavior.Sell();
            UIRoot.Instance.HideUIButton.gameObject.SetActive(false);
            towerInfoPanel.SetActive(false);
        }
        public void UpgradeTower() {
            if (!GameManager.Instance.Scoring.ChangeProcessing(-lastTowerBehavior.Info.UpgradeCost, false))
            {
                return;
            }
            lastTowerBehavior.Upgrade();
            ChangeInfo();
        }
    }
}
