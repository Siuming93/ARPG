using System;
using System.Collections.Generic;
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

        private static List<Skill> _skills;

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
            var path = Path + "/" + skill.Id + "/" + "Skill" + ".asset";

            newItem.DataContext = new ItemData()
            {
                SelectedType = SelectedType.Skill,
                Path = path,
                Name = skill.Id.ToString(),
                Data = skill
            };
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

            newItem.DataContext = new ItemData()
            {
                SelectedType = SelectedType.Action,
                Path = path + "/Actions/" + action.GetType().Name + parentItem.ChildItemCount + ".asset"
            };
            AddEvents(newItem);

            CreateFolder(path, "Actions");
            AssetDatabase.CreateAsset(action,
                path + "/Actions/" + action.GetType().Name + parentItem.ChildItemCount + ".asset");
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
            var data = (ItemData) (sender as TreeViewItem).DataContext;
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

        private static void AddEvents(TreeViewItem item)
        {
            item.Selected = SelectedHanlder;
        }

        public static void InitTree(TreeViewControl item)
        {
            item.Width = 600;
            item.Height = 500;
            item.m_roomItem = null;
            item.Header = "Skill List";
            AddEvents(item.RootItem);
            item.DataContext = new ItemData() {SelectedType = SelectedType.Root};

            InitSkillList();

            foreach (var skill in _skills)
            {
                var skillItem = item.RootItem.AddItem(skill.Id.ToString());
                skillItem.DataContext = new ItemData()
                {
                    SelectedType = SelectedType.Skill,
                    Path = Path + "/" + skill.Id + "/" + "Skill" + ".asset",
                    Name = skill.Id.ToString(),
                    Data = skill
                };

                AddEvents(skillItem);

                foreach (var action in skill.Actions)
                {
                    var newItem = skillItem.AddItem(action.GetType().Name + (skillItem.ChildItemCount + 1));
                    var data = skillItem.DataContext as ItemData;
                    var path = Path + "/" + data.Name;

                    newItem.DataContext = new ItemData()
                    {
                        SelectedType = SelectedType.Action,
                        Path = path + "/Actions/" + action.GetType().Name + skillItem.ChildItemCount + ".asset"
                    };

                    AddEvents(newItem);
                }
            }
        }

        private static void InitSkillList()
        {
            _skills = new List<Skill>();
            var path = "Assets/Resources/SkillData/";
            var parentFloader = new DirectoryInfo(path);
            foreach (var directory in parentFloader.GetDirectories())
            {
                _skills.Add(Resources.Load<Skill>("SkillData/" + directory.Name + "/Skill"));
            }
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