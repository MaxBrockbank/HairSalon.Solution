namespace HairSalon.Models
{
    public class ClientStylist
    {
      public int ClientStylistId { get; set; }
      public int ClientId{ get; set; }
      public int StylistId{ get; set; }
      public Client Client { get; set; }
      public Stylist Stylist { get; set; }
    }
}