using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace TD_Model
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        [SerializeField] private Scoring scoring;
        [SerializeField] private GameOverTrigger gameOverTrigger;
        [SerializeField] private WavesManager wavesManager;

        [SerializeField] private List<TowerInfo> towerInfos;
        [SerializeField] private float sellingFactor;
        [SerializeField] private short maxTowerLvl;

        [SerializeField] private List<PlatformBehaviour> platformBehaviours;
        [SerializeField] private TowerBehavior mainTower;

        [SerializeField] private bool isClassic;

        private List<GameObject> towers;
        private List<GameObject> creeps;
        private SortedDictionary<GameObject, GameObject> platformTowers;

        public static GameManager Instance => instance;
        public Scoring Scoring => scoring;
        public GameOverTrigger GameOverTrigger => gameOverTrigger;
        public WavesManager WavesManager => wavesManager;

        public TowerBehavior MainTower => mainTower;
        public float SellingFactor => sellingFactor;
        public short MaxTowerLvl => maxTowerLvl;

        public bool IsClassic => isClassic;

        protected void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            towers = new List<GameObject>();
            creeps = new List<GameObject>();
        }

        protected void Start()
        {
            MainTower.GetComponentInChildren<BillboardScript>().SetCam(Camera.main.transform);
            towers.Add(MainTower.gameObject);
        }
        public void AddNewTower(GameObject tower)
        {
            towers.Add(tower);
        }

        public void AddNewCreep(GameObject creep)
        {
            creeps.Add(creep);
        }

        public int GetNTowers()
        {
            return towers.Count;
        }

        public int GetNCreeps()
        {
            return creeps.Count;
        }

        public GameObject GetClosestCreep(GameObject gameObject)
        {
            return GetClosestInList(creeps, gameObject);
        }

        public GameObject GetClosestTower(GameObject gameObject)
        {
            return GetClosestInList(towers, gameObject);
        }

        public void DeleteCreep(GameObject creep) {
            creeps.Remove(creep);
        }

        public void DeleteTower(GameObject tower) {
            towers.Remove(tower);
        }

        public void SellAll() {
            foreach (var tower in towers) {
                tower.GetComponent<TowerBehavior>().Sell();
            }
        }


        public PlatformBehaviour GetPlatform(TowerBehavior tower)
        {
            foreach (var platformBehaviour in platformBehaviours)
            {
                if (platformBehaviour.Tower == tower)
                {
                    return platformBehaviour;
                }
            }
            return null;
        }
        private GameObject GetClosestInList(List<GameObject> list, GameObject curGameObject)
        {
            GameObject closest = null;
            double distance = Mathf.Infinity;
            var position = curGameObject.transform.position;
            foreach (var gameObject in list)
            {
                var diff = gameObject.transform.position - position;
                double curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = gameObject;
                    distance = curDistance;
                }
            }

            return closest;
        }
    }
}

