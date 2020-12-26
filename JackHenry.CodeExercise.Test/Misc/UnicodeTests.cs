using System.Collections.Generic;
using System.Text;

namespace JackHenry.CodeExercise.Test.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using JackHenry.CodeExercise.App.Services;
    using JackHenry.CodeExercise.App.Services.Interfaces;

    using Microsoft.Extensions.Logging;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class UnicodeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task UnicodeTests_Match_Flag()
        {
            string input = "❤️";

            var list = new List<string>();

            for (var i = 0; i < input.Length; i += char.IsSurrogatePair(input, i) ? 2 : 1)
            {
                var codepoint = char.ConvertToUtf32(input, i);

                list.Add($"{codepoint:X4}");
            }

            Console.WriteLine(string.Join("-", list));
        }
    }
}