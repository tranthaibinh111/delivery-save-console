using System;
using System.Linq;

using ANNShop.Model;
using ANNShop.Service;

using DeliverySave.Model;
using DeliverySave.Service;

using Service;

namespace ANNShop.Controller
{
  public class DeliverySaveAddressController
  {
    private DeliverySaveAddressService _addressService;

    public DeliverySaveAddressController()
    {
      _addressService = FactoryService.getInstance<DeliverySaveAddressService>();
    }

    public void syncAddressSQLite()
    {
      Console.WriteLine("Bat dau lay thong tin Address tu data SQLite");
      var addressSQLite = FactoryService.getInstance<AddressService>();

      var addresses = addressSQLite.getAll()
        .Select(x => new DeliverySaveAddress()
        {
          id = x.id,
          name = x.name,
          pid = x.pid,
          type = x.type,
          region = x.region,
          alias = x.alias,
          is_picked = x.is_picked,
          is_delivered = x.is_delivered
        })
        .ToList();
      Console.WriteLine("Bat dau lay thong tin Address tu data SQLite");

      try
      {
        Console.WriteLine("Bat dau dong bo ho du lieu table DeliverySaveAdress");
        
        foreach (var item in addresses)
        {
          _addressService.insert(item);
        }

        Console.WriteLine("Bat ket thuc dong bo ho du lieu table DeliverySaveAdress");
      }
      catch (Exception e)
      {
        Console.WriteLine("Error - Delivery Save Address Controller");
        Console.WriteLine(e);
      }
    }
  }
}