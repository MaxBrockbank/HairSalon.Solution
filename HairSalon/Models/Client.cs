using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Client
  {
    public Client()
    {
      this.Stylists = new HashSet<ClientStylist>();
    }
    public int ClientId { get; set; }
    public string ClientFirstName { get; set; }
    public string ClientLastName { get; set; }
    public ICollection<ClientStylist> Stylists {get; }
  }


}