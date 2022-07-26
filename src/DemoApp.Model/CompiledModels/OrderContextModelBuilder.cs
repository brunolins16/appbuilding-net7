﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable 219, 612, 618
#nullable enable

namespace DemoApp.Model.CompiledModels
{
    public partial class OrderContextModel
    {
        partial void Initialize()
        {
            var category = CategoryEntityType.Create(this);
            var person = PersonEntityType.Create(this);
            var product = ProductEntityType.Create(this);
            var purchaseOrder = PurchaseOrderEntityType.Create(this);
            var purchaseOrderLine = PurchaseOrderLineEntityType.Create(this);
            var customer = CustomerEntityType.Create(this, person);
            var employee = EmployeeEntityType.Create(this, person);

            ProductEntityType.CreateForeignKey1(product, category);
            PurchaseOrderEntityType.CreateForeignKey1(purchaseOrder, customer);
            PurchaseOrderLineEntityType.CreateForeignKey1(purchaseOrderLine, purchaseOrder);
            PurchaseOrderLineEntityType.CreateForeignKey2(purchaseOrderLine, product);

            CategoryEntityType.CreateAnnotations(category);
            PersonEntityType.CreateAnnotations(person);
            ProductEntityType.CreateAnnotations(product);
            PurchaseOrderEntityType.CreateAnnotations(purchaseOrder);
            PurchaseOrderLineEntityType.CreateAnnotations(purchaseOrderLine);
            CustomerEntityType.CreateAnnotations(customer);
            EmployeeEntityType.CreateAnnotations(employee);

            AddAnnotation("ProductVersion", "7.0.0-preview.7.22369.1");
            AddAnnotation("Relational:MaxIdentifierLength", 128);
            AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
