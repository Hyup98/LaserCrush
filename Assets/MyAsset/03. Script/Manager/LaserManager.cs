using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using Laser.Entity;

namespace Laser.Manager
{
    /// <summary>
    /// ������ �������� �������� �ϳ��ϳ� ������ �������� �ʰ� Ȱ��ȭ ��Ȱ��ȭ(������� �ܰ�)�� �����ؼ� �Ѱ��ش�.
    /// </summary>
    public class LaserManager : MonoBehaviour
    {
        #region Property
        //lazer�����ϴ� �ڷᱸ��
        private Laser.Entity.Laser m_InitLazer;
        private static List<Laser.Entity.Laser> m_Lasers = new List<Laser.Entity.Laser>();
        private static List<Laser.Entity.Laser> m_LaserAddBuffer = new List<Laser.Entity.Laser>();
        private static List<Laser.Entity.Laser> m_LaserRemoveBuffer = new List<Laser.Entity.Laser>();
        //����� ������ ���������� �ڷᱸ��
        private List<Laser.Entity.Laser> m_RootLazer = new List<Laser.Entity.Laser>();
        private static bool m_Initialized = false;

        //tem
        [SerializeField] private LineRenderer m_SubLine;
        #endregion

        /// <summary>
        /// �������� �ı����ϴ� �̺�Ʈ �߻� �� ȣ��
        /// �ı����� lazer�� �Ű������� �޾� �ش� ��ü�� �ڽ�lazer�� 
        /// ���ο� rootlazer�� �߰�
        /// </summary>
        /// <param name="lazer"></param>
        void DestroyLazer(Laser.Entity.Laser lazer)
        {
            m_RootLazer.Remove(lazer);

            if (lazer.HasChild())
            {
                List<Laser.Entity.Laser> child = lazer.GetChildLazer();
                foreach (var now in child)
                {
                    m_RootLazer.Add(now);
                }
            }
        }

        public void EraseLazer()
        {
            foreach (var lazer in m_RootLazer)
            {
                lazer.Erase();
            }
        }

        public void Activate()
        {
            /*
             * �� ����
             * 1. ��Ʈ �迭 ����
             * 2. ���� ������ ���� �� ��Ʈ�迭�� ������ �迭�� �߰�
             * 3. �ʱ�ȭ���� ����
             */
            if (!m_Initialized)//�� ����
            {
                m_RootLazer.Clear();
                m_InitLazer.Init(Energy.GetPosion(), Launcher.GetPosion() - Energy.GetPosion());
                m_RootLazer.Add(m_InitLazer);
                m_Lasers.Add(m_InitLazer);
                m_Initialized = true;
            }
            /*
             * 1. ������ ��ȸ�ϸ� ��� ������ ���� 
             */
            else
            {
                ActivateBufferFlush();
                for (int i = 0; i < m_Lasers.Count; i++)
                {
                    if(Energy.CheckEnergy()) // ������ ������ ȣ���� �ǹ̰� ���� -> ����ȭ?
                    {
                        m_Lasers[i].Activate();
                    }
                }
            }
        }

        /// <summary>
        /// ��� ������ ������ true ��ȯ
        /// true ��ȯ �� �ٽ� ������ �߽� �� initalized�� �����ϱ����� m_Initialized�� false�� ���� 
        /// </summary>
        public bool DeActivate()
        {
            if (m_Lasers.Count == 0)
            {
                m_Initialized = false;
                return true;
            }
            /*
             * 1 ��Ʈ�迭���� �ϳ��� ������
             * 2 ���� �������� �������� �������� �̵���Ų��.
             * 3 ������ �������� ������ �������� ��Ʈ�迭���� �����ϰ� �ڽķ������� �迭�� �߰�
             * 4 �� ������ ������ �迭�� �������� �����Ѵ�.
             */
            for (int i = 0; i < m_RootLazer.Count; i++)
            {
                if (m_RootLazer[i].Erase())
                {
                    m_LaserRemoveBuffer.Add(m_Lasers[i]);
                    m_Lasers.Remove(m_Lasers[i]);

                    foreach(var child in m_Lasers[i].GetChildLazer())
                    {
                        m_LaserAddBuffer.Add(child);
                    }
                }
            }
            DeActivateBufferFlush();
            return false;
        }

        public void SetFirstPos(Vector3 pos)
        {
            m_SubLine.SetPosition(0, pos);
        }

        public void SetSecondPos(Vector3 pos)
        {
            m_SubLine.SetPosition(1, pos);
        }

        public void ResetPos()
        {
            m_SubLine.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
        }

        public static void AddLaser(Laser.Entity.Laser laser)
        {
            m_LaserAddBuffer.Add(laser);
        }

        public void ActivateBufferFlush()
        {
            for(int i = 0; i < m_LaserAddBuffer.Count; i++) 
            {
                m_Lasers.Add(m_LaserAddBuffer[i]);
            }
            m_LaserAddBuffer.Clear();
        }

        public void DeActivateBufferFlush()
        {
            for (int i = 0; i < m_LaserRemoveBuffer.Count; i++)
            {
                m_RootLazer.Remove(m_LaserRemoveBuffer[i]);
            }
            for (int i = 0; i < m_LaserAddBuffer.Count; i++)
            {
                m_RootLazer.Add(m_LaserAddBuffer[i]);
            }

            m_LaserAddBuffer.Clear();
            m_LaserRemoveBuffer.Clear();
        }
    }
}