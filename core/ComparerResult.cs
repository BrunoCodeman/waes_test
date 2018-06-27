

namespace Waes.Core
{
    /// <summary>
    /// The result of an operation that compare two strings
    /// </summary>
    public class ComparerResult
    {
        /// <summary>
        /// The Status of a comparation
        /// </summary>
        /// <returns>
        /// 0): String Left and Right are Equal
        /// 1): The size of strings Left and Right are different
        /// 2): The strings have same size, but differs in content
        /// </returns>
        public ComparisonStatusCode ReturnCode { get; set; }
        /// <summary>
        /// The Message equivalent to the comparation Status
        /// </summary>
        /// <returns>
        /// The comparison result in a user-friendly message. 
        /// If the string differs just in content but not in size,
        /// the information about the lenght of this difference
        /// and the positions of this differences can also be
        /// found in this message.
        /// </returns>
        public string Message { get; set; }

        public override string ToString()
        {
            return this.Message;
        }
    }
    public enum ComparisonStatusCode {AreEqual = 0, DifferentSize = 1, DifferentContent = 2}
}