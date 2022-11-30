using UnityEngine;

namespace TD_Model
{
    public class TowerInfo : MonoBehaviour 
    {
        [SerializeField] private string towerName;
        [SerializeField] private short towerLvl;
        [SerializeField] private short damage;
        [SerializeField] private float shotPerSec;
        [SerializeField] private float upgradeCost;
        [SerializeField] private short health;
        [SerializeField] private float cost;
        [SerializeField] private Sprite towerSprite;
        public Sprite TowerSprite => towerSprite;

        public string TowerName {
            get { return towerName; }
            set { towerName = value; }
        }
        public short TowerLvl {
            get { return towerLvl; }
            set { towerLvl = value; }
        }
        public short Damage {
            get { return damage; }
            set { damage = value; }
        }
        public float ShotPerSec {
            get { return shotPerSec; }
            set { shotPerSec = value; }
        }
        public float UpgradeCost {
            get { return upgradeCost; }
            set { upgradeCost = value; }
        }
        public short Health {
            get { return health; }
            set { health = value; }
        }
        public float Cost {
            get { return cost; }
            set { cost = value; }
        }
    }
}
