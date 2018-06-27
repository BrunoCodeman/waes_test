
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Waes.Core;

namespace Waes.Tests.project
{
    [TestFixture]
    public class ComparerTest
    {
        private Dictionary<string, string> ConfigurationFile;
        private static string GetBase64StringFromImage(string imgPath)  => 
        System.Convert.ToBase64String(System.IO.File.ReadAllBytes(imgPath));        
        
        [SetUp]
        public void SetUp() => this.ConfigurationFile = JsonConvert
        .DeserializeObject<Dictionary<string, string>>(new StreamReader("config.json").ReadToEnd());
        
        [Test]
        public void MustWarnIfEquals()
        {
            var leftAndRight = GetBase64StringFromImage(this.ConfigurationFile["leftImage"]);
            var result = Comparer.Compare(leftAndRight, leftAndRight);
            
            Assert.IsTrue(result.ReturnCode == ComparisonStatusCode.AreEqual);
            Assert.AreEqual(this.ConfigurationFile["status0"], result.Message); 
        }

        [Test]
        public void MustWarnIfSizeDiffers()
        {
            var b64Left = GetBase64StringFromImage(this.ConfigurationFile["leftImage"]);
            var b64Right = GetBase64StringFromImage(this.ConfigurationFile["rightImage"]);
            var result = Comparer.Compare(b64Left, b64Right);
            
            Assert.IsTrue(result.ReturnCode == ComparisonStatusCode.DifferentSize);
            Assert.AreEqual(this.ConfigurationFile["status1"], result.Message);
        }

        [Test]
        public void MustTellDiffLengthAndOffsets()
        {
            var b64Left = GetBase64StringFromImage(this.ConfigurationFile["leftImage"]);
            var b64Right = String.Concat( b64Left.Select( (c, index) => { return index % 2 == 0 ? c : 'a'; } ) );
            var result = Comparer.Compare(b64Left, b64Right);
            Assert.IsTrue(result.ReturnCode == ComparisonStatusCode.DifferentContent);
            Assert.IsTrue(result.Message.Contains(this.ConfigurationFile["status2"]));
        }



    }
}