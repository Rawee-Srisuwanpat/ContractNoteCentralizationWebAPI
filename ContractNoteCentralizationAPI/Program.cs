using ContractNoteCentralizationAPI.Controllers;
using ContractNoteCentralizationAPI.DataAccess.Implement;
using ContractNoteCentralizationAPI.DataAccess.Interface;
using ContractNoteCentralizationAPI.DataAccessADO.Utills;
using ContractNoteCentralizationAPI.Helper.AD;
using ContractNoteCentralizationAPI.Helper.ContextDb;
using ContractNoteCentralizationAPI.Helper.DB;
using ContractNoteCentralizationAPI.Services.Implement;
using ContractNoteCentralizationAPI.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ContractNoteCentralizationAPI.DataAccessADO.Implement;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.

builder.Services.AddTransient<ILogLoginService, LogLoginService>();
builder.Services.AddTransient<IUsersAuthenticationService, UsersAuthenticationService>();
builder.Services.AddTransient<IMasterSystemService, MasterSystemService>();
builder.Services.AddTransient<IManageUserService, ManageUserService>();
builder.Services.AddTransient<IManageRoleService, ManageRoleService>();
builder.Services.AddTransient<IMasterService, MasterService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IContactNoteService, ContactNoteService>();
builder.Services.AddTransient<ILogService, LogService>();
builder.Services.AddTransient<ADO_ContactNoteRepository>();







builder.Services.AddTransient<IContactNoteService, ContactNoteService>();


// Add Repository to the container.
builder.Services.AddTransient<IRegisterRepository, RegisterRepository>();

// Add Context to the container.
builder.Services.AddDbContext<ContractNoteCentralizationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContractNoteCentralizationConnection")); 
    //options.UseSqlServer(builder.Configuration.GetConnectionString("ChatBotConnection"), options => options.CommandTimeout(1200));
});




builder.Services.AddDbContext<ChatBotDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChatBotConnection"));
});


builder.Services.AddScoped<DBHelpers>();
builder.Services.AddSingleton(builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStringsModel>());

builder.Services.AddSingleton(builder.Configuration.GetSection("principalContext").Get<PrincipalModel>());


builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    // options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
                    // ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
                    // ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]))
                };
            });

builder.Services.Configure<ApiBehaviorOptions>(options
    => options.SuppressModelStateInvalidFilter = true);

//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));



var app = builder.Build();

// Configure the HTTP request pipeline.
// app.UseMiddleware<BadRequestMiddleware>();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseCors("corsapp");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
