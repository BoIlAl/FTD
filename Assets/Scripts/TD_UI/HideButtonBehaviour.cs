using UnityEngine;

namespace TD_UI {
    public class HideButtonBehaviour : MonoBehaviour {
        public void Hide() {
            UIRoot.Instance.TowerInfoBehavior.gameObject.SetActive(false);
            UIRoot.Instance.TowerSelectionObject.gameObject.SetActive(false);
            UIRoot.Instance.HideUIButton.gameObject.SetActive(false);
        }
    }
}
