using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
  public  class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string DateCreated { get; set; } = DateTime.Now.ToShortDateString();
    }
}
