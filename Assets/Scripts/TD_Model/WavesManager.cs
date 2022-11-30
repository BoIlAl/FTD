using System.Collections.Generic;
using TD_Model;
using TD_UI;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private int creepsInWavesAmount;
    [SerializeField] private int wavesAmount;
    [SerializeField] private int wavesDuration;
    [SerializeField] private CreepBehavior creep;
    [SerializeField] private float breakTime;
    [SerializeField] private float addValue;
    [SerializeField] private float addSpeed;
    [SerializeField] private short addDamage;
    [SerializeField] private short addHealth;
    [SerializeField] private float maxCreepSpeed;
    [SerializeField] private List<GameObject> spawners;
    [SerializeField] private bool isPause;

    private float timeSinceLastWave;
    private bool isWave;

    protected void Start()
    {
        UIRoot.Instance.CurrentWaveInfoBehavior.UpdateInfo(creep.Info);
    }
    protected void Update()
    {
        if (isPause)
        {
            return;
        }

        if (wavesAmount < 0)
        {
            GameManager.Instance.GameOverTrigger.SetWavesEndTrigger();
        }

        if (isWave && timeSinceLastWave <= breakTime)
        {
            isWave = false;
            if (creep.Info.Speed + addSpeed > maxCreepSpeed) {
                creep.Upgrade(addValue, 0, addDamage, addHealth);
            }
            else {
                creep.Upgrade(addValue, addSpeed, addDamage, addHealth);
            }
            UIRoot.Instance.CurrentWaveInfoBehavior.UpdateInfo(creep.Info);
        }

        if (timeSinceLastWave > 0)
        {
            timeSinceLastWave -= Time.deltaTime;
            return;
        }


        isWave = true; 
        wavesAmount -= 1;

        foreach (var spawner in spawners)
        {
            spawner.GetComponent<ISpawner>().Spawn(creepsInWavesAmount, creep, wavesDuration);
        }

        timeSinceLastWave = breakTime + wavesDuration;

    }

    public CreepInfo GetCreepInfo()
    {
        return creep.Info;
    }
}
