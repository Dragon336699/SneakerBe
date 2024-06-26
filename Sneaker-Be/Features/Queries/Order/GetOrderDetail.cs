﻿using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Queries.Order
{
    public class GetOrderDetail : IRequest<InforOrderDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public GetOrderDetail(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
