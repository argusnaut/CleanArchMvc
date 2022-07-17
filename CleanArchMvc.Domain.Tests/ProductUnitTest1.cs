using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "Product image");
            action.Should().NotThrow<Validation.DomainExceptionValidation>();

        }

        [Fact]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "Product image");
            action.Should().Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid Id value");

        }

        [Fact]
        public void CreateProduct_WithShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "Product image");
            action.Should().Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid name. Too short, minimum 3 characters");

        }

        [Fact]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam varius tortor tellus, vitae pellentesque felis egestas in. Curabitur neque ex, fermentum vel elementum vitae, posuere ut lorem. Donec in quam eget elit sodales aliquam. Donec dui mi, fringilla vehicula metus a, semper blandit ante. Pellentesque lorem turpis, sodales eu tortor at, gravida feugiat urna. Nullam a risus nec elit tempus pellentesque id et eros. Integer mattis, eros sit amet commodo ultricies, augue ipsum volutpat elit, id hendrerit ante turpis eget diam. Praesent quis nulla nec ante aliquam suscipit. Duis sit amet nulla nulla. Maecenas lacus ipsum, semper ut viverra eget, viverra ut tellus. Aenean interdum erat est, nec tristique nibh efficitur vitae. Donec mattis eu magna viverra consequat. Vivamus a metus sit amet nibh interdum vulputate vitae in metus. Integer vulputate et quam in dapibus.");
            action.Should().Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid image name. Too long, maximum 250 characters");

        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
            action.Should().NotThrow<Validation.DomainExceptionValidation>();

        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoNullReferenceException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
            action.Should().NotThrow<NullReferenceException>();

        }

        [Fact]
        public void CreateProduct_WithEmptyImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
            action.Should().NotThrow<Validation.DomainExceptionValidation>();

        }

        [Fact]
        public void CreateProduct_WithInvalidPriceValue_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", -9.99m, 99, "Product image");
            action.Should().Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid price value");

        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_WithInvalidStockValue_DomainExceptionNegativeValue(int value)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, value, "Product image");
            action.Should().Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid stock value");

        }
    }
}
