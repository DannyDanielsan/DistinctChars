/**In the programming language of your choice, write a program that parses a sentence 
 * and replaces each word with the following: first letter, number of distinct characters between 
 * first and last character, and last letter.  For example, Smooth would become S3h. 
 * Words are separated by spaces or non-alphabetic characters and these separators should 
 * be maintained in their original form and location in the answer.**/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DistinctCharsApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DistinctCharsApp.Controllers
{
    public class SentenceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ParsedSentence(SentenceModel model)
        {
            try
            {
                string parsedSentence = string.Empty;
                string word = string.Empty;

                //If user didn't input string, simply return "0" for no distinct characters and no start/end letters"
                if (model.OriginalSentence == null || model.OriginalSentence?.Length == 0)
                {
                    model.ParsedSentence = "0";
                    return View(model);
                }

                //Iterate through the sentence, if the character is a space or non-alphabetic character, then this is a separator.
                //Separators are simply appended to the result to maintain their original form and location. 
                //For each letter, we start a new word, until we reach another separator, then we process that word and append that word to the result. 
                for (int i = 0; i < model.OriginalSentence.Length; i++)
                {
                    //If character is alphabetic 
                    if (char.IsLetter(model.OriginalSentence[i]))
                    {
                        //Add the letter to the current word.
                        word += model.OriginalSentence[i];
                        //If this is the last character in the sentence and we still have a word to parse
                        if (i == model.OriginalSentence.Length - 1 && word != "")
                        {
                            //Append the final parsed word to the result. 
                            parsedSentence += ParsedWord(word);
                        }
                    }
                    //This is a separator (space or non-alphabetic character), but have to append the current word to the result.
                    else if (word != "")
                    {
                        //Add the parsed word and the next separator. 
                        parsedSentence += ParsedWord(word) + model.OriginalSentence[i];
                        word = "";
                    }
                    //This is a separator (space or non-alphabetic character), append it to the result.
                    else
                    {
                        parsedSentence += model.OriginalSentence[i];
                        word = "";
                    }
                }
                model.ParsedSentence = parsedSentence;
                
                return View(model);

            }
            catch (Exception)
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

        }

        private string ParsedWord(string unparsedWord)
        {
            //Get the number of distinct characters between the first and last letters
            var distinctCharSum = unparsedWord.Length > 2 ? unparsedWord.ToCharArray(1, unparsedWord.Length - 2).Distinct().Count().ToString() : "0";
            
            //Result is the first letter, number of distinct characters between first and last character, and last letter.
            return unparsedWord.First() + distinctCharSum + unparsedWord.ElementAt(unparsedWord.Length - 1);
          
        }
    }
}