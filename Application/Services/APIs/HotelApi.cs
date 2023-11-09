public class NoteApi : IApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/notes", GetAll)
            .Produces<List<Note>>(StatusCodes.Status200OK)
            .WithName("GetAllNotes")
            .WithTags("Getters");
        

        app.MapGet("/notes/{id}", GetById)
            .Produces<Note>(StatusCodes.Status200OK)
            .WithName("GetNote")
            .WithTags("Getters");


        app.MapGet("/notes/search/name/{query}",
           SearchByName)
            .Produces<List<Note>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("SearchNotes")
            .WithTags("Getters");


        app.MapPost("/notes", AddHotel)
            .Accepts<Note>("application/json")
            .Produces<Note>(StatusCodes.Status201Created)
            .WithName("CreateNote")
            .WithTags("Creators");


        app.MapPut("/notes", Update)
            .Accepts<Note>("application/json")
            .WithName("UpdateNote")
            .WithTags("Updaters");


        app.MapDelete("notes/{id}", DeleteById)
            .WithName("DeleteNote")
            .WithTags("Deleters");
        }


        [Authorize] 
        private async Task<IResult> GetAll(INoteRepository repository) => 
            Results.Extensions.Xml(await repository.GetAllNotesAsync());


        [Authorize]
        private async Task<IResult>GetById(int id, INoteRepository repository) => 
            await repository.GetNoteAsync(id) is Note hotel
                ? Results.Ok(hotel)
                : Results.NotFound();


        [Authorize]
        private async Task<IResult> SearchByName(string query, INoteRepository repository) =>
        await repository.GetNotesAsync(query) is IEnumerable<Note> hotels
            ? Results.Ok(hotels)
            : Results.NotFound(Array.Empty<Note>());

            
            
        [Authorize] 
        private async Task<IResult> AddHotel([FromBody] Note hotel, INoteRepository repository)
        {
            await repository.AddNoteAsync(hotel);
            await repository.SaveAsync();
            return Results.Created($"/hotels/{hotel.Id}", hotel);
        }


        [Authorize]
        private async Task<IResult> Update([FromBody] Note hotel, INoteRepository repository)
        {
            await repository.UpdateNoteAsync(hotel);
            await repository.SaveAsync();
            return Results.NoContent();
        }

   
         [Authorize]
         private async Task<IResult> DeleteById(int id, INoteRepository repository)
        {
            await repository.DeleteNoteAsync(id);
            await repository.SaveAsync();
            return Results.NoContent();
        }
}