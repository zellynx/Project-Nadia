using System;

namespace Editor.Reactive
{
    public sealed class
        RavenDependencySubscription
    {
        public string PropertyPath;

        public Action Callback;
    }
}