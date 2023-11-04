var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var hotels = new List<Hotel>()
{
    new Hotel(123, "Robinson", 123.23455, 34.234324),
    new Hotel(512, "Kempinski", 24.56567, 546.6578)
};

app.MapGet("/hotels", () => hotels);
app.MapGet("/hotels/{id}", (int id) => hotels.FirstOrDefault(h => h.Id == id));
app.MapPost("/hotels", (Hotel hotel) => hotels.Add(hotel));
app.MapPut("/hotels", (Hotel hotel) => {
    var index = hotels.FindIndex(h => h.Id == hotel.Id);
    if (index < 0)
    {
        throw new Exception("Not found");
    }
    hotels[index] = hotel;
});

app.MapDelete("hotels/{id}", (int id) =>
{
    var index = hotels.FindIndex(h => h.Id == id);
    if (index < 0)
    {
        throw new Exception("Not found");
    }
    hotels.RemoveAt(index);
});

app.Run();


public class Hotel
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public double Latitude {get; set;}
    public double Longtitude {get; set;}

    public Hotel(int v1, string v2, double v3, double v4)
    {
        Id = v1;
        Name = v2;
        Latitude = v3;
        Longtitude = v4;
    }

}