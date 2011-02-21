using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Company.TwitterStudio.Classifier
{
    #region Format definition
    /// <summary>
    /// Defines an editor format for the TwitterClassifier type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "TwitterClassifier")]
    [Name("TwitterClassifier")]
    [UserVisible(true)] //this should be visible to the end user
    [Order(Before = Priority.Default)] //set the priority to be after the default classifiers
    internal sealed class TwitterClassifierFormat : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "TwitterClassifier" classification type
        /// </summary>
        public TwitterClassifierFormat()
        {
            this.DisplayName = "TwitterClassifier"; //human readable version of the name
            this.BackgroundColor = Colors.Goldenrod;
            this.TextDecorations = System.Windows.TextDecorations.Underline;
        }
    }
    #endregion //Format definition
}
