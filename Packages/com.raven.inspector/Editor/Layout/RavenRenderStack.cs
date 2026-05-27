using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Editor.Layout
{
    public sealed class
        RavenRenderStack
    {
        public int Count =>
            _stack.Count;
        
        private readonly
            Stack<VisualElement>
            _stack =
                new();

        public RavenRenderStack(
            VisualElement root)
        {
            _stack.Push(root);
        }

        public VisualElement
            Current =>
            _stack.Peek();

        public void
            Push(
                VisualElement container)
        {
            if (container == null)
            {
                return;
            }

            _stack.Push(container);
        }

        public void
            Pop()
        {
            if (_stack.Count <= 1)
            {
                return;
            }

            _stack.Pop();
        }
    }
}