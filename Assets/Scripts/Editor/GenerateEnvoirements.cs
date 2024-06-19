using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerateEnvironment : EditorWindow
{
    string[] files = Directory.GetFiles(Application.dataPath, "*.prefab", SearchOption.TopDirectoryOnly);

    public void InstantiatePrefab(int envoirementAmount)
    {
        Transform envParent = GameObject.Find("EnvSorter").transform;
        GameObject env = Resources.Load<GameObject>("Env");


        for (int i = 0; i < envoirementAmount; i++)
        {
            PrefabUtility.InstantiatePrefab(env, envParent);
        }
    }

    [MenuItem("Window/AI/EnvironmentMenu")]
    public static void ShowExample()
    {
        GenerateEnvironment wnd = GetWindow<GenerateEnvironment>();
        wnd.titleContent = new GUIContent("Environment Manager");
    }

    public void CreateGUI()
    {
        var label = new Label("How many environment do you want?");
        rootVisualElement.Add(label);

        var input = new TextField();
        rootVisualElement.Add(input);

        var button = new Button();
        button.text = "Instantiate";
        button.clicked += () => InstantiatePrefab(int.Parse(input.text));
        rootVisualElement.Add(button);

        var button1 = new Button();
        button1.text = "Close Window";
        button1.clicked += () => this.Close();
        rootVisualElement.Add(button1);
    }
}
