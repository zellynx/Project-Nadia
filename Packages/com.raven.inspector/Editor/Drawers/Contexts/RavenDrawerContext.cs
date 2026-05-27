using Editor.Layout;
using Editor.Reactive;
using Editor.UI.Elements;
using Properties.Tree;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.Drawers.Contexts
{
    public sealed class RavenDrawerContext
    {
        public RavenPropertyNode Node;

        public VisualElement Root;

        public VisualElement CurrentElement;

        public VisualElement RenderParent;

        public SerializedObject SerializedObject;

        public RavenGroupContainerRegistry Groups;
        
        public RavenTabRegistry Tabs;
        
        public RavenHorizontalGroupRegistry HorizontalGroups;
        
        public RavenFoldoutRegistry Foldouts;
        
        public RavenDependencyRegistry Dependencies;

        public object Target;
        
        public RavenRenderStack RenderStack;
        
        public SerializedProperty Property => SerializedObject.FindProperty(
                Node.Metadata.PropertyPath);
    }
}