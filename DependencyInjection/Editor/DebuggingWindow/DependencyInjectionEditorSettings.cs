using UnityEditor;

namespace DependencyInjection.Editor.DebuggingWindow
{
    public static class DependencyInjectionEditorSettings
    {
        public static bool ShowInternalBindings
        {
            get => EditorPrefs.GetBool("ShowInternalBindings", defaultValue: true);
            set => EditorPrefs.SetBool("ShowInternalBindings", value);
        }

        public static bool ShowInheritedBindings
        {
            get => EditorPrefs.GetBool("ShowInheritedBindings", defaultValue: true);
            set => EditorPrefs.SetBool("ShowInheritedBindings", value);
        }
    }
}