public class HotelApi : IApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/hotels", GetAll)
            .Produces<List<Note>>(StatusCodes.Status200OK)
            .WithName("GetAllHotels")
            .WithTags("Getters");
        

        app.MapGet("/hotels/{id}", GetById)
            .Produces<Note>(StatusCodes.Status200OK)
            .WithName("GetHotel")
            .WithTags("Getters");


        app.MapGet("/hotels/search/name/{query}",
           SearchByName)
            .Produces<List<Note>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("SearchHotels")
            .WithTags("Getters");


        app.MapGet("/hotels/search/location/{coordinate}", 
        SearchByCoordinates) 
            .ExcludeFromDescription();


        app.MapPost("/hotels", AddHotel)
            .Accepts<Note>("application/json")
            .Produces<Note>(StatusCodes.Status201Created)
            .WithName("CreateHotel")
            .WithTags("Creators");


        app.MapPut("/hotels", Update)
            .Accepts<Note>("application/json")
            .WithName("UpdateHotel")
            .WithTags("Updaters");


        app.MapDelete("hotels/{id}", DeleteById)
            .WithName("DeleteHotel")
            .WithTags("Deleters");
        }


        [Authorize] 
        private async Task<IResult> GetAll(INoteRepository repository) => 
            Results.Extensions.Xml(await repository.GetHotelsAsync());


        [Authorize]
        private async Task<IResult>GetById(int id, INoteRepository repository) => 
            await repository.GetHotelAsync(id) is Note hotel
                ? Results.Ok(hotel)
                : Results.NotFound();


        [Authorize]
        private async Task<IResult> SearchByName(string query, INoteRepository repository) =>
        await repository.GetHotelsAsync(query) is IEnumerable<Note> hotels
            ? Results.Ok(hotels)
            : Results.NotFound(Array.Empty<Note>());


        [Authorize] 
        private async Task<IResult> SearchByCoordinates(Coordinate coordinate, INoteRepository repository) =>
            await repository.GetHotelsAsync(coordinate) is IEnumerable<Note> hotels
                ? Results.Ok(hotels)
                : Results.NotFound(Array.Empty<Note>());

            
            
        [Authorize] 
        private async Task<IResult> AddHotel([FromBody] Note hotel, INoteRepository repository)
        {
            await repository.AddHotelAsync(hotel);
            await repository.SaveAsync();
            return Results.Created($"/hotels/{hotel.Id}", hotel);
        }


        [Authorize]
        private async Task<IResult> Update([FromBody] Note hotel, INoteRepository repository)
        {
            await repository.UpdateHotelAsync(hotel);
            await repository.SaveAsync();
            return Results.NoContent();
        }

   
         [Authorize]
         private async Task<IResult> DeleteById(int id, INoteRepository repository)
        {
            await repository.DeleteHotelAsync(id);
            await repository.SaveAsync();
            return Results.NoContent();
        }
}