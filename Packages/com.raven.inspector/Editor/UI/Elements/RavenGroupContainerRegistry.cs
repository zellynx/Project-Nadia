using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Editor.UI.Elements
{
    public sealed class RavenGroupContainerRegistry
    {
        private readonly Dictionary<string, VisualElement>
            _groups = new();

        public bool TryGetGroup(
            string groupName,
            out VisualElement container)
        {
            return _groups.TryGetValue(
                groupName,
                out container);
        }

        public void RegisterGroup(
            string groupName,
            VisualElement container)
        {
            _groups[groupName] = container;
        }
    }
}