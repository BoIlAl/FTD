using TD_UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TD_ClicksProcessing
{
    public class TerrainClicksProcessing : MonoBehaviour, IPointerClickHandler
    {
        private RaycastHit hit;

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit);
                UIRoot.Instance.LastClick = hit.point;
                UIRoot.Instance.HideUIButton.gameObject.SetActive(true);
                UIRoot.Instance.TowerSelectionObject.SetActive(true);
                UIRoot.Instance.TowerSelectionImage.transform.position = Input.mousePosition;
            }
        }
    }
}
