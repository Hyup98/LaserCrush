using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;


public class LazerManager : MonoBehaviour
{
    #region Property
    //lazer�����ϴ� �ڷᱸ��
    private Lazer m_InitLazer;
    private List<Lazer> m_Lazers = new List<Lazer> ();
    //����� ������ ���������� �ڷᱸ��
    private List<Lazer> m_RootLazer = new List<Lazer> ();
    private bool m_Initialized = false;
    #endregion
    /// <summary>
    /// �������� �ı����ϴ� �̺�Ʈ �߻� �� ȣ��
    /// �ı����� lazer�� �Ű������� �޾� �ش� ��ü�� �ڽ�lazer�� 
    /// ���ο� rootlazer�� �߰�
    /// </summary>
    /// <param name="lazer"></param>
    void DestroyLazer(Lazer lazer)
    {
        m_RootLazer.Remove(lazer);

        if(lazer.HasChild())
        {
            List<Lazer> child = lazer.GetChildLazer();
            foreach(var now in child)
            {
                m_RootLazer.Add(now);
            }
        }
    }

    public void EraseLazer()
    {
        foreach(var lazer in m_RootLazer)
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
        if(!m_Initialized)//�� ����
        {
            m_RootLazer.Clear();
            m_InitLazer.Init(Energy.GetPosion(), Launcher.GetPosion(), Launcher.GetPosion() - Energy.GetPosion());
            m_RootLazer.Add(m_InitLazer);
            m_Lazers.Add(m_InitLazer);
            m_Initialized = true;
        }
        /*
         * 1. ������ ��ȸ�ϸ� ��� ������ ���� 
         */
        else
        {
            for(int i = 0; i < m_Lazers.Count; i++) 
            {
                if (!m_Lazers[i].IsComplite())//���� �Ұ��� üũ
                {
                    m_Lazers[i].Shoot();
                }
            }
        }

    }
}
