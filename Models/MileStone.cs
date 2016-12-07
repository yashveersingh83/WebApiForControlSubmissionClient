using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{

    public class MileStone
    {
        public int Id { get; set; }
       
        public string Name { get; set; }

        public System.DateTime? StartDate { set; get; }
    }

    public class Recepient
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { set; get; }
        public string Division { set; get; }
    }

    public class InformationRequest
    {
        public int Id { get; set; }
        public string InformationRequired { get; set; }
        public int RecepientId { get; set; }
        public int MileStoneId { get; set; }

        public virtual Recepient Recepient { get; set; }
        public virtual MileStone MileStone {get;set;}
    }
}