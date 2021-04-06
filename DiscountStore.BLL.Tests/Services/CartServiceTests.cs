// Copyright (c) PavelJedlicka. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using DiscountStore.BLL.DataAccess;
using DiscountStore.BLL.Models;
using DiscountStore.BLL.Services;
using Moq;
using Xunit;

namespace DiscountStore.BLL.Tests.Services
{
    public class CartServiceTests
    {
        [Fact]
        public void Add_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var cartItem = new CartItem();

                var repositoryMock = new Mock<IRepository<CartItem>>();
                repositoryMock.Setup(tab => tab.Add(cartItem)).Verifiable();

                var unitOfWorkMock = new Mock<IUnitOfWork>();
                unitOfWorkMock.Setup(tab => tab.CartItems).Returns(repositoryMock.Object).Verifiable();
                unitOfWorkMock.Setup(tab => tab.SaveChanges()).Verifiable();

                ICartService service = new CartService(unitOfWorkMock.Object);

                // Act
                service.Add(cartItem);

                // Assert
                repositoryMock.Verify(tab => tab.Add(cartItem), Times.Exactly(1));
                unitOfWorkMock.Verify(tab => tab.SaveChanges(), Times.Exactly(1));
            }
        }

        [Fact]
        public void Remove_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var removeId = 1;

                var repositoryMock = new Mock<IRepository<CartItem>>();
                repositoryMock.Setup(tab => tab.RemoveById(removeId)).Verifiable();

                var unitOfWorkMock = new Mock<IUnitOfWork>();
                unitOfWorkMock.Setup(tab => tab.CartItems).Returns(repositoryMock.Object).Verifiable();
                unitOfWorkMock.Setup(tab => tab.SaveChanges()).Verifiable();

                ICartService service = new CartService(unitOfWorkMock.Object);

                // Act
                service.Remove(removeId);

                // Assert
                repositoryMock.Verify(tab => tab.RemoveById(removeId), Times.Exactly(1));
                unitOfWorkMock.Verify(tab => tab.SaveChanges(), Times.Exactly(1));
            }
        }

        [Fact]
        public void GetCartItems_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var repositoryMock = new Mock<IRepository<CartItem>>();
                repositoryMock.Setup(tab => tab.GetAll(tab => tab.Item.Discounts)).Returns(this.GetCartItemsWithDiscounts()).Verifiable();

                var unitOfWorkMock = new Mock<IUnitOfWork>();
                unitOfWorkMock.Setup(tab => tab.CartItems).Returns(repositoryMock.Object);

                ICartService service = new CartService(unitOfWorkMock.Object);

                // Act
                var actual = service.GetCartItems();

                // Assert
                repositoryMock.Verify(tab => tab.GetAll(tab => tab.Item.Discounts), Times.Exactly(1));
            }
        }

        [Fact]
        public void GetTotal_WithDiscount_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var expected = 6.1M;

                var repositoryMock = new Mock<IRepository<CartItem>>();
                repositoryMock.Setup(tab => tab.GetAll(tab => tab.Item.Discounts)).Returns(this.GetCartItemsWithDiscounts()).Verifiable();

                var unitOfWorkMock = new Mock<IUnitOfWork>();
                unitOfWorkMock.Setup(tab => tab.CartItems).Returns(repositoryMock.Object);

                ICartService service = new CartService(unitOfWorkMock.Object);

                // Act
                var actual = service.GetTotal();

                // Assert
                repositoryMock.Verify(tab => tab.GetAll(tab => tab.Item.Discounts), Times.Exactly(1));
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void GetTotal_WithoutDiscount_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var expected = 3M;

                var repositoryMock = new Mock<IRepository<CartItem>>();
                repositoryMock.Setup(tab => tab.GetAll(tab => tab.Item.Discounts)).Returns(this.GetCartItemsWithoutDiscounts()).Verifiable();

                var unitOfWorkMock = new Mock<IUnitOfWork>();
                unitOfWorkMock.Setup(tab => tab.CartItems).Returns(repositoryMock.Object);

                ICartService service = new CartService(unitOfWorkMock.Object);

                // Act
                var actual = service.GetTotal();

                // Assert
                repositoryMock.Verify(tab => tab.GetAll(tab => tab.Item.Discounts), Times.Exactly(1));
                Assert.Equal(expected, actual);
            }
        }

        private IQueryable<CartItem> GetCartItemsWithDiscounts()
        {
            var vase = new Item
            {
                Id = 1,
                Price = 1.2M,

                Discounts = new List<Discount>(),
            };

            var bigMug = new Item
            {
                Id = 2,
                Price = 1,

                Discounts = new List<Discount>()
                {
                    new Discount { Count = 2, SpecialPrice = 1.5M },
                },
            };

            var napkinsPack = new Item
            {
                Id = 3,
                Price = 0.45M,

                Discounts = new List<Discount>()
                {
                    new Discount { Count = 3, SpecialPrice = 0.9M },
                },
            };

            var cartItems = new List<CartItem>()
            {
                new CartItem { Item = vase },
                new CartItem { Item = bigMug },
                new CartItem { Item = bigMug },
                new CartItem { Item = bigMug },
                new CartItem { Item = bigMug },
                new CartItem { Item = bigMug },
                new CartItem { Item = napkinsPack },
                new CartItem { Item = napkinsPack },
                new CartItem { Item = napkinsPack },
            };

            return cartItems.AsQueryable();
        }

        private IQueryable<CartItem> GetCartItemsWithoutDiscounts()
        {
            var item = new Item
            {
                Id = 1,
                Name = "Test",
                Price = 1.5M,

                Discounts = new List<Discount>(),
            };

            var cartItems = new List<CartItem>()
            {
                new CartItem { Item = item },
                new CartItem { Item = item },
            };

            return cartItems.AsQueryable();
        }
    }
}
