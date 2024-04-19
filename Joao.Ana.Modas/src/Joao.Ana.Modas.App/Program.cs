using AutoMapper;
using Joao.Ana.Modas.App.Models.Clientes;
using Joao.Ana.Modas.App.Models.Cores;
using Joao.Ana.Modas.App.Models.Enderecos;
using Joao.Ana.Modas.App.Models.Fornecedores;
using Joao.Ana.Modas.App.Models.LogistaAssociado;
using Joao.Ana.Modas.App.Models.Pedidos;
using Joao.Ana.Modas.App.Models.ProdutoEstoques;
using Joao.Ana.Modas.App.Models.Produtos;
using Joao.Ana.Modas.App.Models.Tamanhos;
using Joao.Ana.Modas.App.Models.TipoPagamento;
using Joao.Ana.Modas.App.Models.Vendedores;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Identity;
using Joao.Ana.Modas.Infra.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();

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
}).AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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
    cfg.CreateMap<ItemCheckViewModel, ProdutoEstoque>().ReverseMap();
    
    cfg.CreateMap<TipoPagamentoViewModel, TipoPagamento>().ReverseMap();
    cfg.CreateMap<LogistaAssociadoViewModel, LogistaAssociado>().ReverseMap();
    cfg.CreateMap<PedidoViewModel, Pedido>().ReverseMap();  
    cfg.CreateMap<ProdutoPedidoViewModel, ProdutoPedido>().ReverseMap();
    cfg.CreateMap<VendedorViewModel, Vendedor>().ReverseMap();
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IFornecedorRepositorio, FornecedorRepositorio>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<ITamanhoRepositorio, TamanhoRepositorio>();
builder.Services.AddScoped<ICorRepositorio, CorRepositorio>();
builder.Services.AddScoped<IProdutoEstoqueRepositorio, ProdutoEstoqueRepositorio>();
builder.Services.AddScoped<ITipoPagamentoRepositorio, TipoPagamentoRepositorio>();
builder.Services.AddScoped<ILogistaAssociadoRepositorio, LogistaAssociadoRepositorio>();
builder.Services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
builder.Services.AddScoped<IProdutoPedidoRepositorio, ProdutoPedidoRepositorio>();
builder.Services.AddScoped<IVendedorRepositorio, VendedorRepositorio>();

var app = builder.Build();

var defaultDateCulture = "pt-BR";

// Formatter number
var ci = new CultureInfo(defaultDateCulture);
ci.NumberFormat.NumberDecimalSeparator = ",";
ci.NumberFormat.CurrencyDecimalSeparator = ",";

// Configure the Localization middleware
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ci),
    SupportedCultures = new List<CultureInfo>
        {
             ci,
        },
    SupportedUICultures = new List<CultureInfo>
        {
             ci,
        }
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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
