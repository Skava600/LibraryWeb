using LibraryWeb.IdentityServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddIdentityServerAuth(builder.Configuration);
var app = builder.Build();

app.UseIdentityServer();
app.UseAuthorization();

SeedData.InitializeIdentityDatabase(app);

var connectionString = builder.Configuration.GetConnectionString("DataAccessPostgreSqlProvider");
SeedData.EnsureSeedData(connectionString);


app.Run();
