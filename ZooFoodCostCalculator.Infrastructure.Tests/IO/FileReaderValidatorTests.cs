using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Common.Options;
using ZooFoodCostCalculator.Infrastructure.IO;
using ZooFoodCostCalculator.Infrastructure.IO.Validators;

namespace ZooFoodCostCalculator.Infrastructure.Tests.IO
{
    public class FileReaderValidatorTests
    {
        FileReaderValidator validator;
        IOptions<FilesOptions> fileOptions;

        [SetUp]
        public void Setup()
        {
            var fileReaderMock = new Mock<IFileReader>();
            fileOptions = Options.Create(new FilesOptions
            {
                AnimalFoodFileName = "animals.csv",
                FilesPath = "Files",
                PricesFileName = "prices.txt",
                ZooFileName = "zoo.xml"
            });

            fileReaderMock.Setup(c => c.ReadFileAsLines(It.IsAny<string>())).Returns(() => new string[] { "asd", "zxc" });
            validator = new FileReaderValidator(fileReaderMock.Object, fileOptions);
        }

        [Test]
        public void ReadAsLines_Should_Succeed()
        {
            var lines = validator.ReadFileAsLines(fileOptions.Value.AnimalFoodFileName);
            Assert.That(lines.Length, Is.EqualTo(2));
        }

        [Test]
        public void ReadAsLines_Should_ThrowArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => validator.ReadFileAsLines(string.Empty));
        }

        [Test]
        public void ReadAsLines_Should_ThrowFileNotFoundException()
        {
            Assert.Throws(typeof(FileNotFoundException), () => validator.ReadFileAsLines("file.tss"));
        }

        [Test]
        public void ReadAsXml_Should_Succeed()
        {
            var lines = validator.ReadFileAsLines(fileOptions.Value.ZooFileName);
            Assert.That(lines.Length, Is.EqualTo(2));
        }

        [Test]
        public void ReadAsXml_Should_ThrowArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => validator.ReadXmlFile(string.Empty));
        }

        [Test]
        public void ReadAsXml_Should_ThrowFileNotFoundException()
        {
            Assert.Throws(typeof(FileNotFoundException), () => validator.ReadXmlFile("file.xml"));
        }

        [Test]
        public void ReadAsXml_Should_ArgumentException()
        {
            Assert.Throws(typeof(ArgumentException), () => validator.ReadXmlFile("file.tls"));
        }
    }
}
