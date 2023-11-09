public interface INoteRepository
{
    Task<List<Note>> GetAllNotesAsync();
    Task<List<Note>> GetNotesAsync(string title);
    Task<Note> GetNoteAsync(int id);
    Task AddNoteAsync(Note note);
    Task UpdateNoteAsync(Note note);
    Task DeleteNoteAsync(int id);
    Task SaveAsync();
}