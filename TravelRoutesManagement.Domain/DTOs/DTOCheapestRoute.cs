namespace TravelRoutesManagement.Domain.DTOs
{
    public class DTOCheapestRoute
    {
        public List<DTOFlightConnection> Connections { get; set; } = new List<DTOFlightConnection>();
        public decimal TotalPrice 
        { 
            get { return Connections.Sum(x => x.Price); }
        }

        public DTOCheapestRoute() { }

        public void AddConnection(DTOFlightConnection connection)
        {
            Connections.Add(connection);
        }
    }
}
