using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.dal.Models
{
    public class Grade
    {
        public int Id { get; set; }
        //[Required]
        public int SessionId { get; set; }
        public Session? Session { get; set; }
        //[Required]
        public int TraineeId { get; set; }
        public User? Trainee { get; set; }
        //[Required]
        public int Value { get; set; }
    }
}
