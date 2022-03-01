namespace HomeApi.Contracts.Models.Rooms
{
    public class EditRoomRequest
    {
        public string Name { get; set; }
        public int NewArea { get; set; }
        public bool NewGasStatus { get; set; }
        public int NewVoltage { get; set; }
    }
}