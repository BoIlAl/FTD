using UnityEngine;
using UnityEngine.AI;

namespace TD_Model {
    enum CreepStatus
    {
        CsAlive,
        CsDies,
        CsAttack,
    };

    public class CreepBehavior : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private HealthBarScript healthBar;
        [SerializeField] private GameObject healthBarCanvas;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private CreepInfo info;

        private GameObject attackingTower;
        private Animator animator;

        private CreepStatus creepStatus;
        private float timer;
        private Vector3 destination;
        private const float AttackAnimationTime = 1;
        private const float FallingForwardAnimationTime = 1.167f;
        private const float FallingBackAnimationTime = 1.4f;

        public CreepInfo Info => info;

        protected void Start()
        {
            healthBar.SetMaxHealth(info.Health);
            animator = gameObject.GetComponent<Animator>();
            animator.SetFloat("Speed", info.Speed);
            navMeshAgent.speed = info.Speed;
            creepStatus = CreepStatus.CsAlive;
        }

        protected void Update()
        {
            if (GameManager.Instance.GetNTowers() == 0) {
                navMeshAgent.SetDestination(gameObject.transform.position);
            }

            if (creepStatus == CreepStatus.CsAttack)
            {
                Destroy(healthBarCanvas);
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    return;
                }

                creepStatus = CreepStatus.CsDies;
                timer = FallingForwardAnimationTime;
            }

            if (creepStatus == CreepStatus.CsDies)
            {
                Destroy(healthBarCanvas);
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    return;
                }

                Destroy(gameObject);
                return;
            }

            if (GameManager.Instance.IsClassic && attackingTower == null) {
                destination = GameManager.Instance.MainTower.transform.position;
                attackingTower = GameManager.Instance.MainTower.gameObject;
                navMeshAgent.SetDestination(destination);
            }

            if (!GameManager.Instance.IsClassic) {
                attackingTower = GameManager.Instance.GetClosestTower(gameObject);
                destination = attackingTower.transform.position;
                navMeshAgent.SetDestination(destination);
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (creepStatus != CreepStatus.CsAlive)
            {
                return;
            }
            if (collision.gameObject != attackingTower)
            {
                return;
            }

            GameManager.Instance.DeleteCreep(gameObject);
            creepStatus = CreepStatus.CsAttack;
            animator.SetTrigger("Attack");
            navMeshAgent.speed = 0;
            timer = AttackAnimationTime;
            attackingTower.GetComponent<TowerBehavior>().ReceiveDamage(info.Damage);
        }

        public void ReceiveDamage(short damage)
        {
            info.Health -= damage;

            if (info.Health <= 0)
            {
                navMeshAgent.speed = 0;
                GameManager.Instance.DeleteCreep(gameObject);
                creepStatus = CreepStatus.CsDies;
                animator.SetTrigger("FallingBack");
                GameManager.Instance.Scoring.ChangeProcessing(info.Value, true);
                timer = FallingBackAnimationTime;
            }

            healthBar.SetHealth(info.Health);
        }

        public void Upgrade(float addValue, float addSpeed, short addDamage, short addHealth)
        {
            info.Value += addValue;
            info.Speed += addSpeed;
            info.Damage += addDamage;
            info.Health += addHealth;
        }
    }
}
