using System.Collections.Generic;
using TD_UI;
using UnityEngine;

namespace TD_Model 
{
    public class TowerBehavior : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Material startMaterial;
        [SerializeField] private Material actionMaterial;
        [SerializeField] private HealthBarScript healthBar;

        [SerializeField] private TowerInfo info;
        [SerializeField] private TowerUpgradeParameters upgradeParameters;
        [SerializeField] private AudioSource shootSound;
        [SerializeField] private GameObject particlesEmpty;
        [SerializeField] private List<GameObject> particles;
        public TowerInfo Info => info;

        private Transform rotatingPart;
        private GameObject closestCreep;
        private Material curMaterial;
        private float timeSinceLastShot;
        private float animationTime;

        public HealthBarScript HealthBar {
            get { return healthBar; }
            set { healthBar = value; }
        }

        protected void Start()
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "RotatingPart")
                {
                    rotatingPart = child;
                    break;
                }
            }
            healthBar.SetMaxHealth(info.Health);
            curMaterial = startMaterial;
            animationTime = 1 / ((float)info.ShotPerSec * 4);
            timeSinceLastShot = 1 / (float)info.ShotPerSec + 0.1f;
        }

        protected void Update()
        {
            timeSinceLastShot += Time.deltaTime;
            if (GameManager.Instance.GetNCreeps() == 0)
            {
                ChangeMaterial(gameObject, startMaterial);
                curMaterial = startMaterial;
                return;
            }

            if (timeSinceLastShot <= animationTime)
            {
                ChangeMaterial(gameObject, actionMaterial);
                curMaterial = actionMaterial;
                return;
            }

            ChangeMaterial(gameObject, startMaterial);
            curMaterial = startMaterial;

            closestCreep = GameManager.Instance.GetClosestCreep(gameObject);

            var direction = (closestCreep.transform.position - rotatingPart.transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            if (lookRotation != rotatingPart.rotation)
            {
                rotatingPart.rotation =
                    Quaternion.RotateTowards(rotatingPart.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }
            else if (timeSinceLastShot >= 1 / (float) info.ShotPerSec)
            {
                timeSinceLastShot = 0;
                closestCreep.GetComponent<CreepBehavior>().ReceiveDamage(info.Damage);
                ChangeMaterial(gameObject, actionMaterial);
                curMaterial = actionMaterial;
                shootSound.pitch = Random.Range(0.9f, 1.1f);
                shootSound.Play();
                foreach (var particle in particles)
                {
                    Instantiate(particle, particlesEmpty.transform);
                }
            }

        }

        private void ChangeMaterial(GameObject currentObject, Material material)
        {
            if (curMaterial == material)
            {
                return;
            }

            if (currentObject.transform.tag == "ActivePart")
            {
                currentObject.GetComponent<Renderer>().sharedMaterial = material;
                return;
            }

            if (currentObject.transform.childCount != 0)
            {
                for (var i = 0; i < currentObject.transform.childCount; ++i)
                {
                    ChangeMaterial(currentObject.transform.GetChild(i).gameObject, material);
                }
            }
        }

        public void ReceiveDamage(short damage)
        {
            info.Health -= damage;

            if (info.Health <= 0)
            {
                if (UIRoot.Instance.TowerInfoBehavior.gameObject.activeInHierarchy &&
                    UIRoot.Instance.TowerInfoBehavior.LastTowerBehavior == this)
                {
                    UIRoot.Instance.GetComponent<HideButtonBehaviour>().Hide();
                }
                healthBar.SetHealth(info.Health);
                GameManager.Instance.DeleteTower(gameObject);
                Destroy(gameObject);
                return;
            }

            if (UIRoot.Instance.TowerInfoBehavior.gameObject.activeInHierarchy &&
                UIRoot.Instance.TowerInfoBehavior.LastTowerBehavior == this) {
                UIRoot.Instance.TowerInfoBehavior.Display(this);
            }

            healthBar.SetHealth(info.Health);
        }

        public void Sell() {
            GameManager.Instance.DeleteTower(gameObject);
            GameManager.Instance.Scoring.ChangeProcessing(info.Cost * GameManager.Instance.SellingFactor, false);
            Destroy(gameObject);
        }
        public void Upgrade()
        {
            info.TowerLvl += 1;
            info.Cost += info.UpgradeCost;
            info.UpgradeCost *= upgradeParameters.UpgradeCostFactor;
            info.Damage += upgradeParameters.AddDamage;
            info.ShotPerSec += upgradeParameters.AddShotPerSec;
            transform.localScale *= upgradeParameters.SizeChangeFactor;
            animationTime = 1 / ((float)info.ShotPerSec * 4);
            timeSinceLastShot = 1 / (float)info.ShotPerSec + 0.1f;
        }
    }
}