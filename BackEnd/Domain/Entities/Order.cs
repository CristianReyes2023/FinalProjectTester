using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : BaseEntityInt
    {
        public DateOnly OrderDate { get; set; }
        public DateOnly DeadlineDate { get; set; }
        public DateOnly ExpectedDate { get; set; }
        [Required]
        public string Comments { get; set; }

        [Required]
        public int IdClientFk { get; set; }
        public Client Clients { get; set; }
        [Required]
        public int IdStateOrderFk { get; set; }
        public StateOrder StatesOrders { get; set; }

        public ICollection<OrderDetail> OrdersDetails { get; set; }
    }
}