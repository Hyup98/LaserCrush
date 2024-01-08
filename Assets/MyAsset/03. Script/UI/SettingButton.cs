using UnityEngine;
using UnityEngine.EventSystems;

namespace LaserCrush.UI
{
    public class SettingButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject m_SettingCanvas;

        private bool m_IsActiveSettingCanvas;

        public void OnPointerClick(PointerEventData eventData)
        {
            m_IsActiveSettingCanvas = !m_IsActiveSettingCanvas;

            m_SettingCanvas.SetActive(m_IsActiveSettingCanvas);
        }
    }
}
