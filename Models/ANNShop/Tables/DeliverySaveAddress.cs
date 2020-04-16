namespace ANNShop.Model
{
  public partial class DeliverySaveAddress
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public int? PID { get; set; }
    public int Type { get; set; }
    public int? Region { get; set; }
    public string Alias { get; set; }
    public bool IsPicked { get; set; }
    public bool IsDelivered { get; set; }
  }
}