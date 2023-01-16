using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_iRenta.Data.Interfaces;
using Test_iRenta.Data.Models.EntityModel;
using Test_iRenta.Data.Models.Enums;
using Test_iRenta.Data.Models.ViewModel;
using WorldMafia.Infrastructure;

namespace Test_iRenta.Data.Service
{
    public class OrderService : IOrders
    {
        private readonly ApplicationDbContext context;

        public OrderService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<string> CreateOrder(Order model)
        {
            var valid = ModelValid(model);
            if (string.IsNullOrEmpty(valid) == false)
                return valid;

            Order order = new Order();
            order.StateOrder = StateOrder.Registered;
            order.Created = DateTime.UtcNow;
            order.ClientName = model.ClientName;

            foreach (var item in model.OrderProducts)
                order.OrderProducts.Add(item);

            await context.AddAsync(order);
            await context.SaveChangesAsync();
            return order.ToString();
        }

        public async Task<string> EdtiOrder(Order model)
        {
            var order = await context.Orders.SingleAsync(o => o.Id == model.Id);
            var valid = ModelValid(model, order);
            
            if (string.IsNullOrEmpty(valid) == false)
                return valid;

            string changeOrder = await ChangeOrder(order, model);
            return changeOrder;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await context.Orders.SingleAsync(o => o.Id == id);
            if (order.StateOrder != StateOrder.Registered)
                return false;
            context.Orders.Remove(order);
            return true;
        }

        public async Task<Order> GetOrder(int id)
        {
            return await context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Order>> GetOrders()
        {
            return await context.Orders
                .AsNoTracking()
                .Where(o => o.StateOrder == StateOrder.Registered)
                .ToListAsync();
        }

        public async Task<List<Order>> GetOrders(DateTime date)
        {
            return await context.Orders
                .AsNoTracking()
                .Where(o => o.StateOrder == StateOrder.Registered && o.Created == date)
                .ToListAsync();
        }

        private string ModelValid(Order model, Order? order = null)
        {
            if (model.OrderProducts.Sum(x => x.Product.Price) >= 15000)
                return "Ошибка! Сумма заказа привышает 15000";
            if (model.OrderProducts.Count() > 10)
                return "Ошибка! Количество единц товаров более 10";
            if (order != null && order.StateOrder != StateOrder.Registered)
                return "Ошибка! Заказ нельзя изменять!";
            return "";
        }

        private async Task<string> ChangeOrder(Order order, Order model)
        {
            string change = "";

            if (order.ClientName != model.ClientName)
            {
                order.ClientName = model.ClientName;
                change += $"Изменения поля клиент с {order.ClientName} на {model.ClientName} \n";
            }

            if (order.StateOrder != model.StateOrder)
            {
                order.StateOrder = model.StateOrder;
                change += $"Изменения поля статус заказа с {order.StateOrder} на {model.StateOrder} \n";
            }

            change = DeleteProduct(order, model, change);
            change = await EditProduct(order, model, change);

            return change;
        }

        private async Task<string> EditProduct(Order order, Order model, string change)
        {
            foreach (OrderProduct modelItem in model.OrderProducts)
            {
                OrderProduct orderItem = order.OrderProducts.FirstOrDefault(op => op.ProductId == modelItem.ProductId);

                if (orderItem is null)
                {
                    await context.OrderProducts.AddAsync(modelItem);
                    change += $"Добавление продукта в заказ \n";
                    /*
                     * В данном случаеи изменение реализовал через создание строк с изменениями 
                     * Как наиболее быстрый вариант, в релиях приложение возможно создать модель *изменений
                     * и туда уже вписывать список свойств и объектов что измененны, так же можно реализовать
                     * "репрозитоиря" куда будут отправляться изменение сущности и записывать логи и так же формировать
                     * Json ответ изменений, еще через систему логов где записываются изменение сущности и оттуда уже 
                     * доставать по ключу закака, но в реалиях тестового приложения сделано через простую строку.
                     */
                }
                else
                {
                    if (orderItem.ProductId != modelItem.ProductId)
                    {
                        orderItem.ProductId = modelItem.ProductId;
                        change += $"Изменения поля номер продуткта с {orderItem.ProductId} на {modelItem.ProductId} \n";
                    }

                    if (orderItem.Count != modelItem.Count)
                    {
                        orderItem.Count = modelItem.Count;
                        change += $"Изменения поля количество заказа с {orderItem.Count} на {modelItem.Count} \n";
                    }
                }
            }

            return change;
        }

        private string DeleteProduct(Order order, Order model, string change)
        {
            foreach (OrderProduct itemOrder in from product in order.OrderProducts
                                               let modelItem = model.OrderProducts.FirstOrDefault(op => op.ProductId == product.ProductId)
                                               where modelItem is null
                                               select product)
            {
                context.OrderProducts.Remove(itemOrder);
                change += $"Удален товар в заказе {itemOrder.ProductId} \n";
            }
            return change;
        }
    }
}
