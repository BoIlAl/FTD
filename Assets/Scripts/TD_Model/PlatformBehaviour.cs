using TD_Model;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    private TowerBehavior tower;

    public TowerBehavior Tower => tower;

    public void SetTower(TowerBehavior newTower)
    {
        tower = newTower;
    }

    public void ResetTower()
    {
        tower = null;
    }
}
