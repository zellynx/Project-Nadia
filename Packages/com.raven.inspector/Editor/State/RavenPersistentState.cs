using UnityEditor;

namespace Editor.State
{
    public static class
        RavenPersistentState
    {
        public static bool
            GetBool(
                string key,
                bool defaultValue = true)
        {
            return EditorPrefs.GetBool(
                key,
                defaultValue);
        }

        public static void
            SetBool(
                string key,
                bool value)
        {
            EditorPrefs.SetBool(
                key,
                value);
        }

        public static int
            GetInt(
                string key,
                int defaultValue = 0)
        {
            return EditorPrefs.GetInt(
                key,
                defaultValue);
        }

        public static void
            SetInt(
                string key,
                int value)
        {
            EditorPrefs.SetInt(
                key,
                value);
        }
    }
}