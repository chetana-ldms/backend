namespace LDP.Common.Model
{
    public class AlertNoteModel
    {
        public int AlertsNotesId { get; set; }
        public int AlertId { get; set; }
        public string? ActionType { get; set; }
        public string? ActionName { get; set; }
        public int ActionId { get; set; }
        public string? Notes { get; set; }
        public DateTime? NotesDate { get; set; }
        public int NotesCreatedUserid { get; set; }
        public int NotesToUserid { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }

    }
}
