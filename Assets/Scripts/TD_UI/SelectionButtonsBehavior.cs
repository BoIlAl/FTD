using TD_Model;
using TD_UI;
using UnityEngine;
using UnityEngine.UI;

public class SelectionButtonsBehavior : MonoBehaviour
{
    [SerializeField] private TowerBehavior simpleTower;
    [SerializeField] private TowerBehavior magicTower;
    [SerializeField] private Text simpleTowerCost;
    [SerializeField] private Text magicTowerCost;
    [SerializeField] private float timeInterval;
    [SerializeField] private int max;
    [SerializeField] private Image simpleTowerImage;
    [SerializeField] private Image magicTowerImage;

    private float lastTime;
    protected void Start() {
        lastTime = -timeInterval;
        simpleTowerCost.text = simpleTower.Info.Cost.ToString();
        magicTowerCost.text = magicTower.Info.Cost.ToString();
        simpleTowerImage.sprite = simpleTower.Info.TowerSprite;
        magicTowerImage.sprite = magicTower.Info.TowerSprite;
    }
    public void SimpleTowerInstantiate()
    {
        TowerInstantiate(simpleTower);
    }
    public void MagicTowerInstantiate()
    {
        TowerInstantiate(magicTower);
    }

    private void TowerInstantiate(TowerBehavior curTower) {
        if (Time.realtimeSinceStartup - lastTime < timeInterval) {
            return;
        }
        if (GameManager.Instance.GetNTowers() >= max) {
            return;
        }

        if (!GameManager.Instance.Scoring.ChangeProcessing(-curTower.Info.Cost, false))
        {
            return;
        }

        if (UIRoot.Instance.LastClickedPlatform != null)
        {
            UIRoot.Instance.LastClickedPlatform.SetTower(curTower);
        }
        UIRoot.Instance.TowerSelectionObject.SetActive(false);
        UIRoot.Instance.HideUIButton.gameObject.SetActive(false);
        var tower = Instantiate(curTower, UIRoot.Instance.LastClick, Quaternion.identity);

        if (UIRoot.Instance.LastClickedPlatform != null) {
            UIRoot.Instance.LastClickedPlatform.SetTower(tower);
        }

        if (GameManager.Instance.IsClassic)
        {
            tower.GetComponentInChildren<BillboardScript>().gameObject.SetActive(false);
        }
        GameManager.Instance.AddNewTower(tower.gameObject);

        tower.GetComponentInChildren<BillboardScript>().SetCam(Camera.main.transform);
        

        lastTime = Time.realtimeSinceStartup;
    }
}
