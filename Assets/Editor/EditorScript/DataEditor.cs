using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using LaserCrush.Manager;
using Debug = UnityEngine.Debug;

namespace LaserCrush.Editor
{
    public class DataEditor : MonoBehaviour
    {
        [MenuItem("Data/Reset Data")]
        private static void ResetData()
        {
            DataManager.InitDataSetting();
            DataManager.SaveGameData();
            DataManager.SaveSettingData();
            Debug.Log("������ ���� ����");
        }

        [MenuItem("Data/Delete Data")]
        private static void DeleteFile()
        {
            string gameDataPath = Path.Combine(DataManager.s_DefaultPath, DataManager.s_GameDataFileName);
            string settingDataPath = Path.Combine(DataManager.s_DefaultPath, DataManager.s_SettingDataFileName);
            try
            {
                if (File.Exists(gameDataPath))
                {
                    File.Delete(gameDataPath);
                    Debug.Log(DataManager.s_GameDataFileName + "���� ����");
                }

                if (File.Exists(settingDataPath))
                {
                    File.Delete(settingDataPath);
                    Debug.Log(DataManager.s_SettingDataFileName + "���� ����");
                }
            }
            catch (Exception e)
            { Debug.LogError(e.Message); }
        }

        [MenuItem("Data/Open Explorer")]
        private static void OpenExplorer()
        {
            try
            { Process.Start(DataManager.s_DefaultPath); }
            catch (Exception e)
            { Debug.LogError(e.Message); }
        }
    }
}
