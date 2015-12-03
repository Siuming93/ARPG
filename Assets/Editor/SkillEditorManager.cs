using System;
using Assets.Scripts.View.Skill;
using Assets.Scripts.View.Skill.Action;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class SkillEditorManager
    {
        public static TreeViewItem SelectedItem;
        public static string Path = "Assets/Resources/SkillData";

        public static void Handler(object sender, System.EventArgs args)
        {
            Debug.Log(string.Format("{0} detected: {1}", args.GetType().Name, (sender as TreeViewItem).Header));
        }

        public static void CreateSkill(int id)
        {
            var skill = ScriptableObject.CreateInstance<Skill>();
            skill.Id = id;

            var newItem = SelectedItem.AddItem(id.ToString());
            var path = Path + "/" + skill.Id + "/" + "Skill" + skill.Id + ".asset";

            newItem.DataContext = new ItemData()
            {
                SelectedType = SelectedType.Skill,
                Path = path,
                Name = skill.Id.ToString(),
                Data = skill
            };
            newItem.IsSelected = true;
            AddEvents(newItem);

            AssetDatabase.CreateFolder(Path, skill.Id.ToString());
            AssetDatabase.CreateAsset(skill, path);
        }

        public static void CreateActionAsset(TreeViewItem parentItem, ActionBase action)
        {
            var newItem = parentItem.AddItem(action.GetType().ToString());
            var data = parentItem.DataContext as ItemData;
            var path = Path + "/" + data.Name;

            newItem.DataContext = new ItemData() {SelectedType = SelectedType.Skill, Path = path};
            newItem.IsSelected = true;
            AddEvents(newItem);

            AssetDatabase.CreateFolder(path, "Actions");
            AssetDatabase.CreateAsset(action, path + "/Actions/" + action.GetType() + ".asset");
        }

        public static void CreateAction(Type actionType)
        {
            var action = ScriptableObject.CreateInstance<AnimationAction>();
            CreateActionAsset(SelectedItem, action);
        }

        public static void SelectedHanlder(object sender, System.EventArgs args)
        {
            var data = (ItemData) (sender as TreeViewItem).DataContext;
            SelectedItem = sender as TreeViewItem;

            switch (data.SelectedType)
            {
                case SelectedType.Root:
                    break;
                case SelectedType.Skill:
                    //选择操作
                    break;
                case SelectedType.Action:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void AddHandlerEvent(out System.EventHandler handler)
        {
            handler = Handler;
        }

        private static void AddEvents(TreeViewItem item)
        {
            AddHandlerEvent(out item.Click);
            AddHandlerEvent(out item.Checked);
            AddHandlerEvent(out item.Unchecked);
            AddHandlerEvent(out item.Unselected);
            item.Selected = SelectedHanlder;
        }

        public static void InitTree(TreeViewControl item)
        {
            item.Width = 600;
            item.Height = 500;
            item.Header = "Skill List";
            AddEvents(item.RootItem);
            var item1 = item.RootItem.AddItem("skill1");

            AddEvents(item1);
            item.DataContext = new ItemData() {SelectedType = SelectedType.Root};
            item1.DataContext = new ItemData() {SelectedType = SelectedType.Skill};
        }
    }

    internal enum SelectedType : byte
    {
        Root,
        Skill,
        Action
    }

    internal class ItemData
    {
        public SelectedType SelectedType;
        public string Path;
        public string Name;
        public object Data;

        public Type Type
        {
            get { return Data.GetType(); }
        }
    }
}