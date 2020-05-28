﻿using CarRentalCo.Orders.Domain.Orders;
using CarRentalCo.Orders.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalCo.Orders.Infrastructure.Domain.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersDbContext ordersContext;

        public OrderRepository(OrdersDbContext ordersContext)
        {
            this.ordersContext = ordersContext;
        }

        public async Task AddAsync(Order order)
        {
            await ordersContext.AddAsync(order);
            await ordersContext.SaveChangesAsync();
        }

        public async Task<ICollection<Order>> GetByCustomerIdAsync(CustomerId customerId)
        {
            return await ordersContext.Orders.Include(x => x.OrderCars).Where(x => x.CustomerId == customerId).ToListAsync();
            //var order =  await ordersContext.Orders.IncludePaths("OrderCars").ToListAsync();
            
        }

        public async Task<Order> GetByIdAsync(OrderId orderId)
        {
            return await ordersContext.Orders.Include(x => x.OrderCars).Where(x => x.Id == orderId)
                .FirstOrDefaultAsync();
        }
    }
}
