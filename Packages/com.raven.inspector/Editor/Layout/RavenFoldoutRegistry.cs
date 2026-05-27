using System.Collections.Generic;
using Editor.State;
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

            string stateKey =
                $"RavenFoldout.{name}";

            var foldout =
                new Foldout
                {
                    text = name,
                    value =
                        RavenPersistentState
                            .GetBool(
                                stateKey,
                                true)
                };
            
            foldout.RegisterValueChangedCallback(
                evt =>
                {
                    RavenPersistentState.SetBool(
                        stateKey,
                        evt.newValue);
                });

            root.Add(foldout);

            _foldouts[name] = foldout;

            return foldout;
        }
    }
}