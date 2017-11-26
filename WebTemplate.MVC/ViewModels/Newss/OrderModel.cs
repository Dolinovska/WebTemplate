namespace WebTemplate.MVC.ViewModels.Newss
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using WebTemplate.Database.Models;

    using News = WebTemplate.Database.Models.News;

    public sealed class OrderModel
    {
        public int Id { get; set; }
        [Display(Name = "ПІП")]
        public string FullName { get; set; }
        [Display(Name = "Адреса")]
        public string Text { get; set; }
        [Display(Name = "Номер")]
        public string Number { get; set; }
       


        public OrderModel()
        {
        }

        public OrderModel(Order order)
        {
            this.Id = order.Id;
            this.Text = order.Text;
            this.FullName = order.FullName;
            this.Number = order.Number;

        }
    }
}