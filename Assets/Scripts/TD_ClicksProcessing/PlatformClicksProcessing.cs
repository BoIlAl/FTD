using TD_UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatformClicksProcessing : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject towerPlace;
    [SerializeField] private PlatformBehaviour platformBehaviour;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (platformBehaviour.Tower != null)
        {
            UIRoot.Instance.TowerInfoBehavior.Display(platformBehaviour.Tower);
            return;
        }

        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            UIRoot.Instance.LastClickedPlatform = platformBehaviour;
            UIRoot.Instance.LastClick = towerPlace.transform.position;
            UIRoot.Instance.HideUIButton.gameObject.SetActive(true);
            UIRoot.Instance.TowerSelectionObject.SetActive(true);
            UIRoot.Instance.TowerSelectionImage.transform.position =
                Camera.main.WorldToScreenPoint(towerPlace.transform.position);
        }
    }
}
