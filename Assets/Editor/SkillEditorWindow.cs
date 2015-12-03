using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.View.Skill.Action;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class SkillEditorWindow : EditorWindow
    {
        [MenuItem("Tools/SkillEditorWindow")]
        public static void ShowSkillEditor()
        {
            CreateTreeView();
            RefreashPanel();
        }

        public static SkillEditorWindow _instance;

        public static SkillEditorWindow GetPanel()
        {
            if (_instance == null)
            {
                _instance = EditorWindow.GetWindow<SkillEditorWindow>(false, "SkillEditorWindow", false);
            }

            return _instance;
        }

        public static void RefreashPanel()
        {
            var panel = GetPanel();
            panel.Repaint();
        }

        private static TreeViewControl _treeViewControl;

        public static void CreateTreeView()
        {
            _treeViewControl = TreeViewInspector.AddTreeView();
            _treeViewControl.DisplayInInspector = false;
            _treeViewControl.DisplayOnScene = false;

            SkillEditorManager.InitTree(_treeViewControl);
        }

        private void OnEnable()
        {
            wantsMouseMove = true;
        }

        private int _index = 0;
        private void OnGUI()
        {
            if (_treeViewControl == null)
            {
                return;
            }

            wantsMouseMove = true;
            if (Event.current != null && Event.current.type == EventType.MouseMove)
            {
                Repaint();
            }
            _treeViewControl.DisplayTreeView(TreeViewControl.DisplayTypes.USE_SCROLL_VIEW);

            if (SkillEditorManager.SelectedItem == null)
                return;

            //根据选择的不同标签,显示不同的button
            var data = SkillEditorManager.SelectedItem.DataContext as ItemData;
            switch (data.SelectedType)
            {
                case SelectedType.Root:
                    if (GUILayout.Button("AddSkill"))
                    {
                        SkillEditorManager.CreateSkill();
                    }
                    break;
                case SelectedType.Skill:

                    GUILayout.BeginHorizontal();
                    //1.获得当前的种类

                    var list = GetActionTypes();
                    _index = EditorGUILayout.Popup("Choice Action", _index, list.ToArray());
                    GUILayout.EndHorizontal();
                    if (GUILayout.Button("AddAction"))
                    {
                        var actionType = (from type in Assembly.GetAssembly(typeof (ActionBase)).GetTypes()
                            where type.Name == list[_index]
                            select type).ToList()[0];

                        SkillEditorManager.CreateAction(actionType);
                    }
                    break;
                case SelectedType.Action:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        List<string> GetActionTypes()
        {
            var types = Assembly.GetAssembly(typeof(ActionBase)).GetTypes();

            return (from type in types where type.BaseType == typeof(ActionBase) select type.Name).ToList();
        }
    }
}