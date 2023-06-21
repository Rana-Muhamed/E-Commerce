using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using talabat.Core.Entities;
using talabat.Core.Entities.Order_Aggregate;

namespace talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if(! context.ProductBrands.Any())
            { //if any to check that the table is empty so seeding happens once
                var brandsData = File.ReadAllText("../talabat.Repository/Data/DataSeed/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData); //convert json to any data type
            if(brands is not null && brands.Count > 0 ) { //check that the file is not null or empty
            foreach (var brand in brands) 
                    await context.Set<ProductBrand>().AddAsync(brand);//add data in the table
            await context.SaveChangesAsync();//outside for to save changes once
            
            }
            }
            if (!context.ProductTypes.Any())
            { //if any to check that the table is empty so seeding happens once
                var TypesData = File.ReadAllText("../talabat.Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(TypesData); //convert json to any data type
                if (types is not null && types.Count > 0)
                { //check that the file is not null or empty
                    foreach (var type in types)
                        await context.Set<ProductType>().AddAsync(type);//add data in the table
                    await context.SaveChangesAsync();//outside for to save changes once

                }
            }
            if (!context.Products.Any())
            { //if any to check that the table is empty so seeding happens once
                var ProductData = File.ReadAllText("../talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductData); //convert json to any data type
                if (products is not null && products.Count > 0)
                { //check that the file is not null or empty
                    foreach (var product in products)
                        await context.Set<Product>().AddAsync(product);//add data in the table
                    await context.SaveChangesAsync();//outside for to save changes once

                }
            }

            if (!context.DeliveryMethods.Any())
            { //if any to check that the table is empty so seeding happens once
                var deliveryMethodsData = File.ReadAllText("../talabat.Repository/Data/DataSeed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData); //convert json to any data type
                if (deliveryMethods is not null && deliveryMethods.Count > 0)
                { //check that the file is not null or empty
                    foreach (var deliveryMethod in deliveryMethods)
                        await context.Set<DeliveryMethod>().AddAsync(deliveryMethod);//add data in the table
                    await context.SaveChangesAsync();//outside for to save changes once

                }
            }
        }

    }
}
