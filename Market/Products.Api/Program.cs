using Common;
using Common.Entities;
using Products.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("ConnectionStrings");
builder.Services.Configure<DatabaseOptions>(connectionString);
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products", async (IProductsRepository _productsRepository) =>
{
    var products = await _productsRepository.GetAllAsync();

    return Results.Ok(products);
})
.Produces<Product[]>();

app.MapGet("/products/{id:int}", async (int id, IProductsRepository _productRepository) =>
{
    var product = await _productRepository.GetAsync(id);

    if (product == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(product);
})
.Produces<Product>()
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/products", async (Product model, IProductsRepository _productsRepository) =>
{
    await _productsRepository.AddAsync(model);

    return Results.Ok();
})
.Produces(StatusCodes.Status200OK);

app.MapPut("/products/{id:int}", async (int id, Product model, IProductsRepository _productRepository) =>
{
    var product = await _productRepository.GetAsync(id);

    if (product == null)
    {
        return Results.NotFound();
    }

    await _productRepository.UpdateAsync(model);

    return Results.Ok();
})
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapDelete("/products/{id:int}", async (int id, IProductsRepository _productRepository) =>
{
    var product = await _productRepository.GetAsync(id);

    if (product == null)
    {
        return Results.NotFound();
    }

    await _productRepository.DeleteAsync(id);

    return Results.Ok();
})
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.Run();