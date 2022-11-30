using UnityEngine;

namespace TD_Model
{
    public class CreepInfo : MonoBehaviour
    {
        [SerializeField] private short damage;
        [SerializeField] private float speed;
        [SerializeField] private float _value;
        [SerializeField] private short health;
        public short Damage {
            get { return damage; }
            set { damage = value; }
        }
        public float Speed {
            get { return speed; }
            set { speed = value; }
        }
        public float Value {
            get { return _value; }
            set { _value = value; }
        }
        public short Health {
            get { return health; }
            set { health = value; }
        }
    }
}
