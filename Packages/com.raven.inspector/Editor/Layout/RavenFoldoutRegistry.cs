using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Editor.Layout
{
    public sealed class
        RavenFoldoutRegistry
    {
        private readonly
            Dictionary<string,
                Foldout>
            _foldouts =
                new();

        public Foldout
            GetOrCreate(
                string name,
                VisualElement root)
        {
            if (_foldouts.TryGetValue(
                    name,
                    out var existing))
            {
                return existing;
            }

            var foldout =
                new Foldout
                {
                    text = name,
                    value = true
                };

            root.Add(foldout);

            _foldouts[name] = foldout;

            return foldout;
        }
    }
}