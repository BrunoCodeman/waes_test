
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using System;

namespace Waes.Core
{
    public class Comparer
    {
        /// <summary>
        /// Compares two strings
        /// </summary>
        /// <param name="left">String to be compared</param>
        /// <param name="right">Another string to be compared</param>
        /// <returns>A ComparerResult object with a return code and a message that represents the return code</returns>
        public static ComparerResult Compare(string left, string right)
        {
            return left == right ? 
                new ComparerResult() {   ReturnCode = ComparisonStatusCode.AreEqual, 
                                        Message = "Left and Right are exactly the same." }
            :left.Length != right.Length ?
                new ComparerResult() {   ReturnCode = ComparisonStatusCode.DifferentSize, 
                                        Message = "Left and Right has different sizes" }
            : CompareDiffs(left, right);
        }

        /// <summary>
        /// Check differences between two strings and count how many different chars there is between them
        /// </summary>
        /// <param name="left">One string</param>
        /// <param name="right">Another String</param>
        /// <returns>A ComparerResult object with a return code 2 and a message saying how many differences are and where are they.</returns>
        private static ComparerResult CompareDiffs(string left, string right)
        {   
            Func<char,int,int> filter =(c,i) => { return c != right[i] ? i : -1;};
            var diff = left.Select(filter)
                            .Where(i => i > -1)
                            .ToArray();

            return new ComparerResult(){    ReturnCode = ComparisonStatusCode.DifferentContent, 
                                            Message = $"{diff.Count()} differences found on positions {String.Join(',', diff)}" 
                                            };
        }
    }
}