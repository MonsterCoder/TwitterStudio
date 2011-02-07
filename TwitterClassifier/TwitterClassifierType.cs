using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace TwitterClassifier
{
    internal static class TwitterClassifierClassificationDefinition
    {
        /// <summary>
        /// Defines the "TwitterClassifier" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("TwitterClassifier")]
        internal static ClassificationTypeDefinition TwitterClassifierType = null;
    }
}
