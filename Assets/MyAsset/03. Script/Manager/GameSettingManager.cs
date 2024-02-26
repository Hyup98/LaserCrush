using UnityEngine;
using LaserCrush.ThirdParty;

namespace LaserCrush.Manager
{
    public class GameSettingManager : MonoBehaviour
    {
        [SerializeField] private AdaptiveBanner m_AdaptiveBanner;
        [SerializeField] private int m_TargetFrameRate = 60;

        private readonly int m_SetWidth = 1080; // ����� ���� �ʺ�
        private readonly int m_SetHeight = 1920; // ����� ���� ����

        public void Init()
        {
            SetFrame();
            SetBanner();
        }

        private void SetFrame()
        {
            Application.targetFrameRate = m_TargetFrameRate;
        }

        private void SetBanner()
        {
#if UNITY_STANDALONE || DEVELOPMENT_BUILD
            SetResolution(0);
#else
            m_AdaptiveBanner.BannerOnAction += SetResolution;
            m_AdaptiveBanner.Init();
#endif
        }

        /// <summary>
        /// ���� ���� ���� �� �ػ󵵸� ��������
        /// </summary>
        private void SetResolution(float bannerHeight)
        {
            float ratio = (bannerHeight + 50) / Screen.height;

            int deviceWidth = Screen.width; // ���� ��� �ʺ�
            int deviceHeight = Screen.height; // ���� ��� ����

            float targetAspect = (float)m_SetWidth / m_SetHeight;
            float deviceAspect = (float)deviceWidth / deviceHeight;

            Rect rect;
            if (targetAspect < deviceAspect)
            {
                //Debug.Log("��� �ػ󵵰� �� ŭ");
                float newWidth = targetAspect / deviceAspect; // ���ο� �ʺ�
                rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
            }
            else
            {
                //Debug.Log("���� �ػ󵵰� �� ŭ");
                float newHeight = deviceAspect / targetAspect; // ���ο� ����
                rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
            }

            if (rect.y < ratio)
            {
                //Debug.Log("���� UI�� ħ���ع�����");
                rect.y = ratio;
            }

            Camera.main.rect = rect;
        }

        private void OnPreCull() => GL.Clear(true, true, Color.black);
    }
}