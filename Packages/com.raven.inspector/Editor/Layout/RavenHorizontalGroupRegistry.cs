using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Editor.Layout
{
    public sealed class
        RavenHorizontalGroupRegistry
    {
        private readonly
            Dictionary<string,
                VisualElement>
            _groups =
                new();

        public VisualElement
            GetOrCreate(
                string name,
                VisualElement root)
        {
            if (_groups.TryGetValue(
                    name,
                    out var existing))
            {
                return existing;
            }

            var row =
                new VisualElement();

            row.AddToClassList(
                "raven-horizontal-group");

            root.Add(row);

            _groups[name] = row;

            return row;
        }
    }
}