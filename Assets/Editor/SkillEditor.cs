using UnityEditor;
using UnityEngine;

public class SkillEditor : EditorWindow
{
    [MenuItem("Tools/SkillEditor")]
    public static void ShowSkillEditor()
    {

    }

    public static SkillEditor _instance;

    public static SkillEditor GetPanel()
    {
        if (_instance == null)
        {
            _instance = EditorWindow.GetWindow<SkillEditor>(false, "SkillEditor", false);
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
        _treeViewControl=TreeViewInspector.AddTreeView();
        _treeViewControl.DisplayInInspector = false;
        _treeViewControl.DisplayOnScene = false;

        Example.PopulateExampleData(_treeViewControl);
    }

    void OnEnable()
    {
        wantsMouseMove = true;
    }

    void OnGUI()
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
    }
}
