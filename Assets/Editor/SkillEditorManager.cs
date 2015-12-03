using System;
using System.IO;
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

        public static void CreateSkill()
        {
            var id = SelectedItem.ChildItemCount;
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

            CreateFolder(Path, skill.Id.ToString());
            AssetDatabase.CreateAsset(skill, path);
        }

        public static void CreateActionAsset(TreeViewItem parentItem, ActionBase action)
        {
            var newItem = parentItem.AddItem(action.GetType().Name + (parentItem.ChildItemCount + 1));
            var data = parentItem.DataContext as ItemData;
            var path = Path + "/" + data.Name;
            var Skill = data.Data as Skill;

            Skill.Actions.Add(action);

            newItem.DataContext = new ItemData() { SelectedType = SelectedType.Action, Path = path + "/Actions/" + action.GetType().Name + parentItem.ChildItemCount + ".asset" };
            newItem.IsSelected = true;
            AddEvents(newItem);

            CreateFolder(path, "Actions");
            AssetDatabase.CreateAsset(action, path + "/Actions/" + action.GetType().Name + parentItem.ChildItemCount + ".asset");
        }

        /// <summary>
        /// 想要创建的文件夹存在时,则不创建
        /// </summary>
        /// <param name="parentPath"></param>
        /// <param name="name"></param>
        /// <param name="allwaysCreateNew"></param>
        private static void CreateFolder(string parentPath, string name, bool allwaysCreateNew = false)
        {
            if (Directory.Exists(parentPath + "/" + name) && !allwaysCreateNew)
                return;

            AssetDatabase.CreateFolder(parentPath, name);
        }

        public static void CreateAction(Type actionType)
        {
            var action = ScriptableObject.CreateInstance(actionType) as ActionBase;
            CreateActionAsset(SelectedItem, action);
        }

        public static void SelectedHanlder(object sender, System.EventArgs args)
        {
            var data = (ItemData)(sender as TreeViewItem).DataContext;
            SelectedItem = sender as TreeViewItem;

            switch (data.SelectedType)
            {
                case SelectedType.Root:
                    break;
                case SelectedType.Skill:
                    Selection.activeObject = Resources.Load(data.Path.Substring(17, data.Path.Length - 23));
                    break;
                case SelectedType.Action:
                    Selection.activeObject = Resources.Load(data.Path.Substring(17, data.Path.Length - 23));
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
            item.DataContext = new ItemData() { SelectedType = SelectedType.Root };
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