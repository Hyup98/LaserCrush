using UnityEngine;

namespace LaserCrush.Manager
{
    [System.Serializable]
    public class GameSettingManager
    {
        [SerializeField] private int m_TargetFrameRate = 60;

        public void Init()
        {
            SetFrame();
            SetResolution();
        }

        public void SetFrame()
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = m_TargetFrameRate;
        }

        /// <summary>
        /// ���� ���� ���� �� �ػ󵵸� ��������
        /// </summary>
        private void SetResolution()
        {
            int setWidth = 1080; // ����� ���� �ʺ�
            int setHeight = 1920; // ����� ���� ����

            int deviceWidth = Screen.width; // ���� ��� �ʺ�
            int deviceHeight = Screen.height; // ���� ��� ����

            Screen.SetResolution(setWidth, (int)((float)deviceHeight / deviceWidth * setWidth), true);

            Rect rect;
            if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
            {
                float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
                rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
            }
            else // ������ �ػ� �� �� ū ���
            {
                float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
                rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
            }
            Camera.main.rect = rect;
        }
    }
}