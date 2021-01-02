using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Client
  {
    public int ClientId { get; set; }
    public string ClientFirstName { get; set; }
    public string ClientLastName { get; set; }
  }
}