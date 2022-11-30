using TD_Model;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWaveInfoBehavior : MonoBehaviour
{
    [SerializeField] private Text damage;
    [SerializeField] private Text speed;
    [SerializeField] private Text health;
    [SerializeField] private Text value;
    void Start()
    {
        var info = GameManager.Instance.WavesManager.GetCreepInfo();
        UpdateInfo(info);
    }

    public void UpdateInfo(CreepInfo info)
    {
        damage.text = info.Damage.ToString();
        speed.text = info.Speed.ToString("G4");
        health.text = info.Health.ToString();
        value.text = info.Value.ToString("G4");
    }
}
