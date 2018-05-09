using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Folding;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SAS.Spark.Runner.ViewModels
{
    public class TextEditorProps
    {

        private static Dictionary<TextEditor, FoldingManager> _editorToFoldingLookup = new Dictionary<TextEditor, FoldingManager>();

        public static string GetJsonText(DependencyObject obj)
        {
            return (string)obj.GetValue(JsonTextProperty);
        }
        public static void SetJsonText(DependencyObject obj, string value)
        {
            obj.SetValue(JsonTextProperty, value);
        }
        public static readonly DependencyProperty JsonTextProperty =
            DependencyProperty.RegisterAttached("JsonText", typeof(string), 
                typeof(TextEditorProps), new PropertyMetadata(string.Empty, JsonTextChanged));

        private static void JsonTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextEditor textEditor)
            {
                if (!_editorToFoldingLookup.ContainsKey(textEditor))
                {
                    _editorToFoldingLookup.Add(textEditor, FoldingManager.Install(textEditor.TextArea));
                }

                textEditor.Text = (string)e.NewValue;
                var foldingStrategy = new BraceFoldingStrategy();
                foldingStrategy.UpdateFoldings(_editorToFoldingLookup[textEditor], textEditor.Document);
            }
        }
    }
}
