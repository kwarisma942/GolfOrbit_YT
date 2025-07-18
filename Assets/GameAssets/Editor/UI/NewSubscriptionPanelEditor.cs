using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Pinpin.Scene.MainScene.UI;

namespace Pinpin.UI.Editor
{
    [CustomEditor(typeof(NewSubscriptionPanel))]
    public class NewSubscriptionPanelEditor : SubscriptionPanelEditor
	{

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}