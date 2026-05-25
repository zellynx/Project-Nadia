using Editor.Drawers;
using Editor.PropertyTree;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Inspectors
{
    [CustomEditor(typeof(UnityEngine.MonoBehaviour), true)]
    public sealed class RavenInspector : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            serializedObject.Update();

            var root = new VisualElement();
            
            root.AddToClassList(
                "raven-root");

            var styleSheet =
                Resources.Load<StyleSheet>(
                    "Styles/RavenInspector");

            if (styleSheet != null)
            {
                root.styleSheets.Add(
                    styleSheet);
            }

            var tree =
                RavenPropertyTreeBuilder.Build(
                    target.GetType());

            RavenDrawerPipeline.Draw(
                tree,
                root,
                serializedObject,
                target);

            return root;
        }
    }
}