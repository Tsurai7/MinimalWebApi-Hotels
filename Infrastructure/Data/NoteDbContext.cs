public class NoteDb : DbContext
{
    public NoteDb(DbContextOptions<NoteDb> options) 
        : base(options) 
    {
        Database.EnsureCreated();
    }
    public DbSet<Note> Notes => Set<Note>();
}
