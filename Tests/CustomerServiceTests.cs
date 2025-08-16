using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        private TestDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new TestDbContext(options);
        }

        private CustomerService CreateService(TestDbContext context)
        {
            return new CustomerService(context);
        }

        [Fact]
        public async Task CanPurchase_InvalidCustomerId_ThrowsException()
        {
            using var context = CreateInMemoryDbContext();
            var service = CreateService(context);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.CanPurchase(0, 10, DateTime.UtcNow));
        }

        [Fact]
        public async Task CanPurchase_InvalidPurchaseValue_ThrowsException()
        {
            using var context = CreateInMemoryDbContext();
            var service = CreateService(context);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.CanPurchase(1, 0, DateTime.UtcNow));
        }

        [Fact]
        public async Task CanPurchase_CustomerNotFound_ThrowsException()
        {
            using var context = CreateInMemoryDbContext();
            var service = CreateService(context);

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.CanPurchase(999, 50, DateTime.UtcNow));
        }

        [Fact]
        public async Task CanPurchase_CustomerHasOrderInCurrentMonth_ReturnsFalse()
        {
            using var context = CreateInMemoryDbContext();
            var service = CreateService(context);

            var customer = new Customer { Id = 1, Name = "TesteName" };
            context.Customers.Add(customer);
            context.Orders.Add(new Order
            {
                CustomerId = 1,
                OrderDate = new DateTime(2025, 8, 10),
                Value = 20
            });
            await context.SaveChangesAsync();

            var currentDate = new DateTime(2025, 8, 16, 10, 0, 0); // dentro do horário comercial

            var result = await service.CanPurchase(1, 50, currentDate);
            Assert.False(result);
        }

        [Fact]
        public async Task CanPurchase_FirstPurchaseAboveLimit_ReturnsFalse()
        {
            using var context = CreateInMemoryDbContext();
            var service = CreateService(context);

            var customer = new Customer { Id = 1, Name = "TesteName" };
            context.Customers.Add(customer);
            await context.SaveChangesAsync();

            var date = new DateTime(2025, 8, 16, 10, 0, 0); // horário comercial

            var result = await service.CanPurchase(1, 150, date);
            Assert.False(result);
        }

        [Theory]
        [InlineData(2025, 8, 16, 7)]   // antes das 8h
        [InlineData(2025, 8, 16, 19)]  // depois das 18h
        [InlineData(2025, 8, 17, 10)]  // domingo
        public async Task CanPurchase_OutsideBusinessHours_ReturnsFalse(int year, int month, int day, int hour)
        {
            using var context = CreateInMemoryDbContext();
            var service = CreateService(context);
            var customer = new Customer { Id = 1, Name = "TesteName" };
            context.Customers.Add(customer);
            await context.SaveChangesAsync();

            var testDate = new DateTime(year, month, day, hour, 0, 0);

            var result = await service.CanPurchase(1, 50, testDate);
            Assert.False(result);
        }

        [Fact]
        public async Task CanPurchase_ValidConditions_ReturnsTrue()
        {
            using var context = CreateInMemoryDbContext();
            var service = CreateService(context);

            context.Customers.Add(new Customer
            {
                Id = 1,
                Orders = new List<Order>(),
                Name = "TesteName"
            });

            await context.SaveChangesAsync();

            var validDate = new DateTime(2025, 8, 16, 10, 0, 0);
            var result = await service.CanPurchase(1, 50, validDate);
            Assert.False(result); 

            var weekday = new DateTime(2025, 8, 18, 10, 0, 0); 
            var result2 = await service.CanPurchase(1, 50, weekday);
            Assert.True(result2);
        }
    }
}
