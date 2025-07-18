using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Pinpin.Scene.MainScene.UI;

namespace Pinpin.UI.Editor
{
    [CustomEditor(typeof(SubscriptionPanel))]
    public class SubscriptionPanelEditor : AUIPanelEditor
    {


        public override void OnInspectorGUI()
        {
            SubscriptionPanel subscriptionPanel = target as SubscriptionPanel;
            if (GUILayout.Button("1"))
            {
                subscriptionPanel.Setup(0, 0, 0, true);
            }
            if (GUILayout.Button("2"))
            {
                subscriptionPanel.Setup(1, 0, 0, true);
            }
            if (GUILayout.Button("3"))
            {
                subscriptionPanel.Setup(1, 0, 1, true);
            }
            if (GUILayout.Button("4"))
            {
                subscriptionPanel.Setup(1, 1, 0, true);
            }
            if (GUILayout.Button("5"))
            {
                subscriptionPanel.Setup(1, 1, 1, true);
            }
            if (GUILayout.Button("6"))
            {
                subscriptionPanel.Setup(1, 1, 2, true);
            }
            if (GUILayout.Button("Clear"))
            {
                subscriptionPanel.Setup(-1, 0, 0, true);
            }

            base.OnInspectorGUI();

        }
    }
}