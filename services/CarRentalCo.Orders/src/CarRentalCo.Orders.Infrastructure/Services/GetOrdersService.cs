﻿using CarRentalCo.Common.Infrastructure.Mongo;
using CarRentalCo.Common.Infrastructure.Types;
using CarRentalCo.Orders.Application.Orders.Dtos;
using CarRentalCo.Orders.Application.Orders.Features.GetOrders;
using CarRentalCo.Orders.Infrastructure.Mappings;
using CarRentalCo.Orders.Infrastructure.Mongo.Orders;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalCo.Orders.Infrastructure.Services
{
    public class GetOrdersService : IGetOrdersService
    {
        private readonly IMongoRepository<OrderDocument> mongoRepository;

        public GetOrdersService(IMongoRepository<OrderDocument> mongoRepository)
        {
            this.mongoRepository = mongoRepository;
        }

        public async Task<PagedResult<OrderDto>> GetAsync(GetOrdersServiceQuery query)
        {
            var result = await mongoRepository.BrowseAsync(x => true, query);
            var res = PagedResult<OrderDto>.From(result, result.Items.Select(x => x.ToDto()));

            return res;
        }
    }
}
