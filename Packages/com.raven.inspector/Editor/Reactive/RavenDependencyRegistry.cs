using System;
using System.Collections.Generic;

namespace Editor.Reactive
{
    public sealed class
        RavenDependencyRegistry
    {
        private readonly Dictionary<string,
            List<RavenDependencySubscription>> _listeners = new();
        
        public RavenDependencySubscription Register(string propertyPath, Action callback)
        {
            if (!_listeners.TryGetValue(
                    propertyPath,
                    out var list))
            {
                list =
                    new List<
                        RavenDependencySubscription>();

                _listeners[propertyPath] =
                    list;
            }

            var subscription =
                new RavenDependencySubscription
                {
                    PropertyPath =
                        propertyPath,

                    Callback =
                        callback
                };

            list.Add(subscription);

            return subscription;
        }
        
        public void Unregister(
            RavenDependencySubscription
                subscription)
        {
            if (subscription == null)
            {
                return;
            }

            if (!_listeners.TryGetValue(
                    subscription.PropertyPath,
                    out var list))
            {
                return;
            }

            list.Remove(subscription);
        }

        public void Notify(
            string propertyPath)
        {
            if (!_listeners.TryGetValue(
                    propertyPath,
                    out var list))
            {
                return;
            }

            foreach (var listener
                     in list)
            {
                listener?.Callback?.Invoke();
            }
        }
    }
}