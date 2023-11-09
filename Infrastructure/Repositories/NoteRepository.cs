public class NoteRepository : INoteRepository, IDisposable
{
    private readonly NoteDb _context;
    private bool _disposed = false;

    public NoteRepository(NoteDb context)
    {
        _context = context;
    }
    public async Task<List<Note>> GetAllNotesAsync() =>
        await _context.Notes.ToListAsync();

    public async Task<List<Note>> GetNotesAsync(string title) =>
        await _context.Notes.Where(h => h.Title.Contains(title)).ToListAsync();

    public async Task<Note> GetNoteAsync(int id) =>
        await _context.Notes.FindAsync(new object[] {id});

    public async Task AddNoteAsync(Note note)
    {
        note.CreatedAt = DateTime.Now;
        await _context.Notes.AddAsync(note);
    }

    public async Task UpdateNoteAsync(Note note)
    {
        var noteFromDb = await _context.Notes.FindAsync(new object[] { note.Id});

        if (noteFromDb == null) 
            return;

        noteFromDb.Title = note.Title;
        noteFromDb.Content = note.Content;
        noteFromDb.UpdatedAt = DateTime.Now;
    }

    public async Task DeleteNoteAsync(int hotelId)
    {
        var noteFromDb = await _context.Notes.FindAsync(new object[] {hotelId});

        if (noteFromDb == null)
            return;

        _context.Remove(noteFromDb);
    }

    public async Task SaveAsync() => 
        await _context.SaveChangesAsync();


    protected virtual void Dispose(bool disposing)
    {
        if(!_disposed)
        {
            if(disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose() 
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
