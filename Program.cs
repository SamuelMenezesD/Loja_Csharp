using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;
using loja.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LojaDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26)))
);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/createproduto", async (LojaDbContext DbContext, Produto newProduto) =>
{
    DbContext.Produtos.Add(newProduto);
    await DbContext.SaveChangesAsync();
    return Results.Created($"/createproduto/{newProduto.Id}", newProduto);
});

//Produtos
// [
//     {
//         "id": 2,
//         "nome": "Xiaomi",
//         "preco": 1800,
//         "fornecedor": "Amazon"
//     },
//     {
//         "id": 3,
//         "nome": "IPhone13",
//         "preco": 5000,
//         "fornecedor": "IPhoneCenter"
//     }
// ]

app.MapGet("/produtos", async (LojaDbContext dbContext) =>
{
    var produtos = await dbContext.Produtos.ToListAsync();
    return Results.Ok(produtos);
});



app.MapGet("/produtos/{id}", async (int id, LojaDbContext dbContext) =>
{
    var produtos = await dbContext.Produtos.FindAsync(id);
    if (produtos == null)
    {
        return Results.NotFound($"Produto with ID {id} not found.");
    }
    return Results.Ok(produtos);
});

app.MapPut("/produtos/{id}", async (int id, LojaDbContext dbContext, Produto updatedProduto) =>
{
    var existingProduto = await dbContext.Produtos.FindAsync(id);
    if (existingProduto == null)
    {
        return Results.NotFound($"Produto with ID {id} not found.");
    }

    existingProduto.Nome = updatedProduto.Nome;
    existingProduto.Preco = updatedProduto.Preco;
    existingProduto.Fornecedor = updatedProduto.Fornecedor;

    await dbContext.SaveChangesAsync();

    return Results.Ok(existingProduto);
});

//Criar um novo cliente

app.MapPost("/createcliente", async (LojaDbContext DbContext, Cliente newCliente) =>
{
    DbContext.Clientes.Add(newCliente);
    await DbContext.SaveChangesAsync();
    return Results.Created($"/createcliente/{newCliente.Id}", newCliente);
});

// Clientes Criados
// [
//     {
//         "id": 1,
//         "nome": "joão",
//         "cpf": "4",
//         "email": "j@mail.com"
//     },
//     {
//         "id": 2,
//         "nome": "Maria",
//         "cpf": "22",
//         "email": "m@mail.com"
//     }
// ]
app.MapGet("/clientes", async (LojaDbContext dbContext) =>
{
    var clientes = await dbContext.Clientes.ToListAsync();
    return Results.Ok(clientes);
});

app.MapGet("/clientes/{id}", async (int id, LojaDbContext dbContext) =>
{
    var clientes = await dbContext.Clientes.FindAsync(id);
    if (clientes == null)
    {
        return Results.NotFound($"Cliente with ID {id} not found.");
    }
    return Results.Ok(clientes);
});
app.MapPut("/clientes/{id}", async (int id, LojaDbContext dbContext, Cliente updatedCliente) =>
{
    var existingCliente = await dbContext.Clientes.FindAsync(id);
    if (existingCliente == null)
    {
        return Results.NotFound($"Cliente with ID {id} not found.");
    }
    existingCliente.Nome = updatedCliente.Nome;
    existingCliente.Cpf = updatedCliente.Cpf;
    existingCliente.Email = updatedCliente.Email;

    await dbContext.SaveChangesAsync();

    return Results.Ok(existingCliente);
});

//Desafio

app.MapPost("/createfornecedor", async (LojaDbContext DbContext, Fornecedor newFornecedor) =>
{
    DbContext.Fornecedores.Add(newFornecedor);
    await DbContext.SaveChangesAsync();
    return Results.Created($"/createfornecedor/{newFornecedor.Id}", newFornecedor);
});

//Fornecedor 
// {
//     "Cnpj": "432",
//     "nome": "Juca",
//     "Endereco": "Rua Xico",
//     "email": "x@mail.com",
//     "telefone": "707070"
// }

app.MapGet("/fornecedores", async (LojaDbContext dbContext) =>
{
    var fornecedores = await dbContext.Fornecedores.ToListAsync();
    return Results.Ok(fornecedores);
});

app.MapGet("/fornecedores/{id}", async (int id, LojaDbContext dbContext) =>
{
    var fornecedores = await dbContext.Fornecedores.FindAsync(id);
    if (fornecedores == null)
    {
        return Results.NotFound($"Fornecedor with ID {id} not found.");
    }
    return Results.Ok(fornecedores);
});

app.MapPut("/fornecedor/{id}", async (int id, LojaDbContext dbContext, Fornecedor updatedFornecedor) =>
{
    var existingFornecedor = await dbContext.Fornecedores.FindAsync(id);
    if (existingFornecedor == null)
    {
        return Results.NotFound($"Cliente with ID {id} not found.");
    }

    existingFornecedor.Nome = updatedFornecedor.Nome;
    existingFornecedor.Cnpj = updatedFornecedor.Cnpj;
    existingFornecedor.Endereco = updatedFornecedor.Endereco;
    existingFornecedor.Email = updatedFornecedor.Email;
    existingFornecedor.Telefone = updatedFornecedor.Telefone;

    await dbContext.SaveChangesAsync();

    return Results.Ok(existingFornecedor);
});

app.Run();
