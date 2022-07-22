using Books.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Books.Tests
{
    public class BookTests
    {
        [Test]
        public void Book_NumberOfCopies_CanBeNull()
        {
            var testBook = new Book { Title = "Some title" };
            Assert.That(testBook.NumberOfCopies, Is.Null);
        }

        [Test]
        public void Book_Title_IsRequired()
        {
            var aBookWithoutTitle = new Book();
            
            var validationContext = new ValidationContext(aBookWithoutTitle);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(aBookWithoutTitle, validationContext, validationResults, true);
            
            Assert.That(isValid, Is.False);
            Assert.That(validationResults.Count, Is.EqualTo(1));
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The Title field is required."));
        }
    }
}
