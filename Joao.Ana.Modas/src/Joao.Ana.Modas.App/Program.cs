using AutoMapper;
using Joao.Ana.Modas.App.Models.Clientes;
using Joao.Ana.Modas.App.Models.Cores;
using Joao.Ana.Modas.App.Models.Enderecos;
using Joao.Ana.Modas.App.Models.Fornecedores;
using Joao.Ana.Modas.App.Models.ProdutoEstoques;
using Joao.Ana.Modas.App.Models.Produtos;
using Joao.Ana.Modas.App.Models.Tamanhos;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Identity;
using Joao.Ana.Modas.Infra.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
                        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                     .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllersWithViews(config =>
{
    var policy = new AuthorizationPolicyBuilder()
           .RequireAuthenticatedUser()
           .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = new PathString("/Usuarios/AcessoNegado");
    options.LoginPath = new PathString("/Usuarios/Login");
});

builder.Services.AddRazorPages()
        .AddRazorRuntimeCompilation();

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<ClienteViewModel, Cliente>().ReverseMap();
    cfg.CreateMap<EnderecoViewModel, Endereco>().ReverseMap();
    cfg.CreateMap<FornecedorViewModel, Fornecedor>().ReverseMap();
    cfg.CreateMap<ProdutoViewModel, Produto>().ReverseMap();

    cfg.CreateMap<TamanhoViewModel, Tamanho>().ReverseMap();
    cfg.CreateMap<CorViewModel, Cor>().ReverseMap();
    cfg.CreateMap<ProdutoEstoqueViewModel, ProdutoEstoque>().ReverseMap();
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IFornecedorRepositorio, FornecedorRepositorio>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<ITamanhoRepositorio, TamanhoRepositorio>();
builder.Services.AddScoped<ICorRepositorio, CorRepositorio>();
builder.Services.AddScoped<IProdutoEstoqueRepositorio, ProdutoEstoqueRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
