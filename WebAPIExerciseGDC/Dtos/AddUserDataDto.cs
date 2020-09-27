using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIExerciseGDC.Dtos
{
    public class AddUserDataDto
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string FirstName { get; set; }
        [System.ComponentModel.DataAnnotations.Required]

        public string LastName { get; set; }
        [System.ComponentModel.DataAnnotations.Required]

        public string Address { get; set; }
    }
}
