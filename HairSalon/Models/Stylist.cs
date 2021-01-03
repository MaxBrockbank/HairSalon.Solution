using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Stylist
  {
    public Stylist()
    {
      this.Clients = new HashSet<Client>();
    }
    public int StylistId { get; set; }
    public string StylistFirstName { get; set; }
    public string StylistLastName { get; set; }
    public void virtual ICollection<Client> Clients { get; set; }
  }
}