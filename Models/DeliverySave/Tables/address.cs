using System.ComponentModel.DataAnnotations;

namespace DeliverySave.Model
{
    public partial class Address
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
        public string pid { get; set; }
        public string type { get; set; }
        public string region { get; set; }  
        public string alias { get; set; }
        public string is_picked { get; set; }
        public string is_delivered { get; set; }
    }
}