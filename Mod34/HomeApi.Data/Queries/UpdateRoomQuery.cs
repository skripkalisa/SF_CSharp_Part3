namespace HomeApi.Data.Queries
{
    public class UpdateRoomQuery
    {
        internal string NewName { get; set; }
        internal int NewArea{ get; set;}

        public UpdateRoomQuery(string newName = null, int newArea = default)
        {
            NewName = newName;
            NewArea = newArea;
        }
    }
}