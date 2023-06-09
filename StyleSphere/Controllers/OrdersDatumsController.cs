﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using StyleSphere.Models;

namespace StyleSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDatumsController : ControllerBase
    {
        private readonly StyleSphereDbContext _context;

        public OrderDatumsController(StyleSphereDbContext context)
        {
            _context = context;
        }
        }
    public class OrderDetailController : ControllerBase
    {
        private readonly StyleSphereDbContext _context;

        public OrderDetailController(StyleSphereDbContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
            public async Task<ActionResult<OrderData>> GetOrderDatumByCustomerID(int id)
            {
                var OrdersDatum = _context.OrdersData
                    .Where(e => e.CustomerId == id)
                    .Select(c => new OrderData
                    {
                        OrderId = c.OrderId,
                        CustomerId = c.CustomerId,
                        OrderDate = c.OrderDate,
                        ShippingAddress = c.ShippingAddress,
                        BillingAddress = c.BillingAddress,
                        TrackingId = c.TrackingId,
                        NetAmount = c.NetAmount
                    }).ToList();
                if (OrdersDatum == null)
                {
                    return NotFound();
                }
                return Ok(OrdersDatum);

            }
            public async Task<ActionResult<OrderDetail>> GetOrderDataByCustomerID(int id)
            {
                var OrderDetail = _context.OrderDetails
                    .Where(e => e.OrderDetailsId == id)
                    .Select(c => new OrderDetail
                    {
                        OrderDetailsId = c.OrderDetailsId,
                        Quantity = c.Quantity,
                        Price = c.Price,
                        ProductMappingId = c.ProductMappingId,
                        OrderId = c.OrderId,
                        Total = c.Total,
                        ActiveStatus = c.ActiveStatus
                    }).ToList();
                if (OrderDetail == null)
                {
                    return NotFound();
                }
                return Ok(OrderDetail);

            }


            [Route("Checkout")]
            [HttpPost]
            public async Task<ActionResult<string>> Checkout(CheckoutMaster order)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        OrdersDatum model = new OrdersDatum();
                        model.OrderId = order.OrderId;
                        model.CustomerId = order.CustomerId;
                        model.OrderDate = order.OrderDate;
                        model.ShippingAddress = order.ShippingAddress;
                        model.BillingAddress = order.BillingAddress;
                        model.TrackingId = order.TrackingId;
                        model.NetAmount = order.NetAmount;
                        model.ActiveStatus = order.ActiveStatus;
                        _context.OrdersData.Add(model);
                        await _context.SaveChangesAsync();
                        foreach (var detail in order.OrderDetails)
                        {
                            OrderDetail detailItem = new OrderDetail();
                            detailItem.Quantity = detail.Quantity;
                            detailItem.Price = detail.Price;
                            detailItem.ProductMappingId = detail.ProductMappingId;
                            detailItem.OrderId = model.OrderId;
                            detailItem.Total = detail.Total;
                            detailItem.ActiveStatus = true;
                            _context.OrderDetails.Add(detailItem);
                            await _context.SaveChangesAsync();
                        }
                        transaction.Commit();
                        return Ok(model.OrderId.ToString());
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Ok(ex.Message);
                    }
                }
            }

        }
    }
