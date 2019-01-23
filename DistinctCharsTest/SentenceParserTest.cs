using DistinctCharsApp.Controllers;
using DistinctCharsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DistinctCharsTest
{
    [TestClass]
    public class SentenceParserTest
    {
        [TestMethod]
        public void ParsingTest_Smooth()
        {
            var controller = new SentenceController();
            var model = new SentenceModel
            {
                OriginalSentence = "Smooth"
            };
            var result = controller.ParsedSentence(model) as ViewResult;
            Assert.AreEqual("S3h", model.ParsedSentence);
        }

        [TestMethod]
        public void ParsingTest_ABCDE()
        {
            var controller = new SentenceController();
            var model = new SentenceModel
            {
                OriginalSentence = "Aa$BBb_CCCc@DdDDd^eEeEEE"
            };
            var result = controller.ParsedSentence(model) as ViewResult;
            Assert.AreEqual("A0a$B1b_C1c@D2d^e2E", model.ParsedSentence);
        }

        [TestMethod]
        public void ParsingTest_1234()
        {
            var controller = new SentenceController();
            var model = new SentenceModel
            {
                OriginalSentence = "1a1_2bc2_3def3_4ghji4"
            };
            var result = controller.ParsedSentence(model) as ViewResult;
            Assert.AreEqual("1a0a1_2b0c2_3d1f3_4g2i4", model.ParsedSentence);
            
        }
        
        [TestMethod]
        public void ParsingTest_RemoveNonAlphabetic()
        {
            var controller = new SentenceController();
            var model = new SentenceModel
            {
                OriginalSentence = "1~@!$%1This1()*{}[]1is1~`&#&*&^%1a1:;?><9TEST!!!!"
            };
            var result = controller.ParsedSentence(model) as ViewResult;
            Assert.AreEqual("1~@!$%1T2s1()*{}[]1i0s1~`&#&*&^%1a0a1:;?><9T2T!!!!", model.ParsedSentence);
            
        }

        [TestMethod]
        public void ParsingTest_Empty()
        {
            var controller = new SentenceController();
            var model = new SentenceModel
            {
                OriginalSentence = string.Empty
            };
            var result = controller.ParsedSentence(model) as ViewResult;
            Assert.AreEqual("0", model.ParsedSentence);
            
        }
    }
}
