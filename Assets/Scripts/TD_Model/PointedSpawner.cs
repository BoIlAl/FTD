using System.Collections.Generic;
using UnityEngine;

namespace TD_Model
{
    public class PointedSpawner : MonoBehaviour, ISpawner
    {
        [SerializeField] private CreepBehavior creepBehavior;
        [SerializeField] private List<GameObject> particles;
        [SerializeField] private float particleTime;
        [SerializeField] private AudioSource instantiateSound;

        private int _creepWaveSize;
        private CreepBehavior _creep;
        private float timeInterval;
        private float timeSinceLastInstantiate;
        private bool isParticle;
        private Vector3 position;

        protected void Update()
        {
            if (_creepWaveSize > 0)
            {
                if (timeInterval < particleTime)
                {
                    return;
                }

                timeSinceLastInstantiate += Time.deltaTime;
                if (!isParticle && timeInterval - timeSinceLastInstantiate < particleTime)
                {
                    isParticle = true;
                    position = transform.position;
                    foreach (var particle in particles)
                    {
                        Instantiate(particle, position + new Vector3(0, 1, 0), transform.rotation);
                    }

                    instantiateSound.Play();
                }

                if (timeSinceLastInstantiate < timeInterval)
                {
                    return;
                }

                isParticle = false;
                var creep = Instantiate(_creep.gameObject, position, transform.rotation);
                creep.GetComponentInChildren<BillboardScript>().SetCam(Camera.main.transform);
                timeSinceLastInstantiate = 0;
                GameManager.Instance.AddNewCreep(creep);
                _creepWaveSize--;
            }
        }
        public void Spawn(int creepWaveSize, CreepBehavior creep, float waveDuration) {
            _creepWaveSize = creepWaveSize;
            _creep = creep;
            timeInterval = waveDuration / creepWaveSize;
        }
    }
}
