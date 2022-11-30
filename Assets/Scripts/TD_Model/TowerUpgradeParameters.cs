using UnityEngine;

namespace TD_Model
{
    public class TowerUpgradeParameters : MonoBehaviour
    {
        [SerializeField] private short addDamage;
        [SerializeField] private float addShotPerSec;
        [SerializeField] private float upgradeCostFactor;
        [SerializeField] private float sizeChangeFactor;

        public short AddDamage => addDamage;
        public float AddShotPerSec => addShotPerSec;
        public float UpgradeCostFactor => upgradeCostFactor;
        public float SizeChangeFactor => sizeChangeFactor;
    }
}
