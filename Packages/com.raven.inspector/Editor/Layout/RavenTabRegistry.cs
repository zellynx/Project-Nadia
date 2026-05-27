using System.Collections.Generic;
using Editor.State;
using UnityEngine.UIElements;

namespace Editor.Layout
{
    public sealed class
        RavenTabRegistry
    {
        private sealed class
            TabGroup
        {
            public VisualElement
                Root;

            public VisualElement
                ButtonRow;

            public List<VisualElement>
                Containers =
                    new();

            public List<Button>
                Buttons =
                    new();

            public Dictionary<string,
                    VisualElement>
                Tabs =
                    new();

            public int
                SelectedIndex;
        }

        private readonly
            Dictionary<string,
                TabGroup>
            _groups =
                new();

        public VisualElement
            GetOrCreateTab(
                string groupName,
                string tabName,
                VisualElement root) {
            if (!_groups.TryGetValue(
                    groupName,
                    out var group)) {
                string stateKey =
                    $"RavenTab.{groupName}";

                group =
                    new TabGroup {
                        Root =
                            new VisualElement(),

                        ButtonRow =
                            new VisualElement(),

                        SelectedIndex =
                            RavenPersistentState
                                .GetInt(
                                    stateKey,
                                    0)
                    };

                group.ButtonRow.style.flexDirection =
                    FlexDirection.Row;

                group.Root.Add(
                    group.ButtonRow);

                root.Add(group.Root);

                _groups[groupName] =
                    group;
            }

            if (group.Tabs.TryGetValue(
                    tabName,
                    out var existing)) {
                return existing;
            }

            int tabIndex =
                group.Containers.Count;

            var container =
                new VisualElement();

            container.style.display =
                tabIndex ==
                group.SelectedIndex
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;

            var button =
                new Button(() => {
                    group.SelectedIndex =
                        tabIndex;

                    string stateKey =
                        $"RavenTab.{groupName}";

                    RavenPersistentState
                        .SetInt(
                            stateKey,
                            tabIndex);

                    for (int i = 0;
                         i < group.Containers.Count;
                         i++) {
                        group.Containers[i]
                                .style.display =
                            i == tabIndex
                                ? DisplayStyle.Flex
                                : DisplayStyle.None;
                    }
                });

            button.text = tabName;

            group.ButtonRow.Add(button);

            group.Root.Add(container);

            group.Buttons.Add(button);

            group.Containers.Add(container);

            group.Tabs[tabName] =
                container;

            return container;
        }
    }
}