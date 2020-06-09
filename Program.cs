using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using ANNShop.Controller;
using DeliverySave.Controller;
using Service;
using System.Text;

namespace DeliverySave
{
  class Program
  {
    private static readonly ConfigurationService _configService = FactoryService.getInstance<ConfigurationService>();
    static void Main(string[] args)
    {
      Console.OutputEncoding = Encoding.UTF8;
      Console.WriteLine(String.Format("Thư mục làm việc: {0}", _configService.getWorkspaceFolder()));
      Console.WriteLine(String.Format("Môi trường đang chạy: {0}", _configService.getEnvironment()));

      if (!File.Exists(_configService.getDatabaseSQLitePath()))
      {
        Console.WriteLine(String.Format("Không tìm thấy {0}", _configService.getDatabaseSQLitePath()));
        Console.ReadLine();
        return;
      }

      #region Lấy dữ liệu Address từ API Giao Hàng tiết kiệm
      var resultGetProvince = false;
      var resultGetDistrict = false;
      var resultGetWard = false;
      var resultGetHamlet = false;

      var addressControll = new AddressController();

      resultGetProvince = addressControll.getAllProvince();
      if (!resultGetProvince)
      {
        Console.WriteLine("Error: Address Controller - Get ALL Provice");
        Console.ReadLine();
        return;
      }

      resultGetDistrict = addressControll.getAllDistrict();
      if (!resultGetDistrict)
      {
        Console.WriteLine("Error: Address Controller - Get ALL District");
        Console.ReadLine();
        return;
      }

      resultGetWard = addressControll.getAllWard();
      if (!resultGetWard)
      {
        Console.WriteLine("Error: Address Controller - Get ALL Ward");
        Console.ReadLine();
        return;
      }

      resultGetHamlet = addressControll.getAllHamlet();
      if (!resultGetHamlet)
      {
        Console.WriteLine("Error: Address Controller - Get ALL Hamlet");
        Console.ReadLine();
        return;
      }
      #endregion

      #region Thực hiện đồng bộ hoá dữ liệu address
      var addressANNController = new DeliverySaveAddressController();

      addressANNController.syncAddressSQLite();
      #endregion

      Console.ReadLine();
    }
  }
}
