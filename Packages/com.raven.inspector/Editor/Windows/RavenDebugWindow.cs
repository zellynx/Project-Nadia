using Core;
using Editor.Drawers;
using Editor.PropertyTree;
using Properties.Tree;
using UnityEditor;
using UnityEngine;

namespace Editor.Windows
{
    public sealed class RavenDebugWindow : EditorWindow
    {
        [MenuItem("Raven/Debug Window")]
        public static void Open()
        {
            GetWindow<RavenDebugWindow>();
        }

        private void OnGUI()
        {/*
            if (GUILayout.Button("Build Property Tree"))
            {
                var tree = RavenPropertyTreeBuilder.Build(typeof(RavenTestComponent));
                var root = new UnityEngine.UIElements.VisualElement();

                RavenDrawerPipeline.Draw(tree, root);
            }*/
        }
/*
        private void DebugNode(RavenPropertyNode node, int depth)
        {
            var indent = new string(' ', depth * 2);

            Debug.Log($"{indent}{node.Metadata.Name}");
            
            foreach (var metadata in node.Metadata.Metadata)
            {
                Debug.Log($"{indent}  Metadata: {metadata.GetType().Name}");
            }

            foreach (var child in node.Children)
            {
                DebugNode(child, depth + 1);
            }
        }*/
    }
}