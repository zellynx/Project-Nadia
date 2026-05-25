using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.Layout
{
    public sealed class RavenTabRegistry
    {
        private readonly
            Dictionary<string,
                VisualElement>
                    _tabs =
                        new();

        private Toolbar _toolbar;

        private VisualElement _root;

        private VisualElement _contentRoot;

        public void Initialize(
            VisualElement parent)
        {
            if (_root != null)
            {
                return;
            }

            _root =
                new VisualElement();

            _toolbar =
                new Toolbar();

            _contentRoot =
                new VisualElement();

            _root.Add(_toolbar);

            _root.Add(_contentRoot);

            parent.Add(_root);
        }

        public VisualElement GetOrCreateTab(
            string name)
        {
            if (_tabs.TryGetValue(
                    name,
                    out var existing))
            {
                return existing;
            }

            var content =
                new VisualElement();

            content.style.display =
                _tabs.Count == 0
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;

            _contentRoot.Add(content);

            var captured =
                content;

            var button =
                new ToolbarButton(() =>
                {
                    foreach (var pair
                             in _tabs)
                    {
                        pair.Value.style.display =
                            DisplayStyle.None;
                    }

                    captured.style.display =
                        DisplayStyle.Flex;
                })
                {
                    text = name
                };

            _toolbar.Add(button);

            _tabs[name] = content;

            return content;
        }
    }
}