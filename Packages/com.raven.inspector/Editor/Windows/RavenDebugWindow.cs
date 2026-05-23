using Core;
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
        {
            if (GUILayout.Button("Build Property Tree"))
            {
                var tree = RavenPropertyTreeBuilder.Build(typeof(RavenTestComponent));

                DebugNode(tree, 0);
            }
        }

        private void DebugNode(RavenPropertyNode node, int depth)
        {
            var indent = new string(' ', depth * 2);

            Debug.Log($"{indent}{node.Metadata.Name}");

            foreach (var child in node.Children)
            {
                DebugNode(child, depth + 1);
            }
        }
    }
}