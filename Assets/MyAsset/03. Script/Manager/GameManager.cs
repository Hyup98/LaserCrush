using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    #region Property
    private LazerManager lazerManager;
    private List<ICollisionable> m_CollisionableDbjects = new List<ICollisionable>();
    private bool m_OnDeploying;
    #endregion

    public void Update()
    {
        //�ùķ��̼�
        if(m_OnDeploying)
        {
            lazerManager.Activate();
        }
        //��ġ��
        else
        {
            //������� �ؾ����� ���� �ȿ´�
            //�Ϸ��ư(������ ������ ������) ������ ��ġ�Ϸắ�� true�� �ٲٱ�

        }
    }
}
