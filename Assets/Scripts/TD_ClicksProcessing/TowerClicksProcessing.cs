using TD_Model;
using TD_UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TD_ClicksProcessing
{
    public class TowerClicksProcessing : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TowerBehavior towerBehavior;

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                UIRoot.Instance.TowerInfoBehavior.Display(towerBehavior);
            }
        }
    }
}
