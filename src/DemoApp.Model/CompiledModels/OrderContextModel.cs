﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable 219, 612, 618
#nullable enable

namespace DemoApp.Model.CompiledModels
{
    [DbContext(typeof(OrderContext))]
    public partial class OrderContextModel : RuntimeModel
    {
        static OrderContextModel()
        {
            var model = new OrderContextModel();
            model.Initialize();
            model.Customize();
            _instance = model;
        }

        private static OrderContextModel _instance;
        public static IModel Instance => _instance;

        partial void Initialize();

        partial void Customize();
    }
}