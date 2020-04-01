using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeliverySave
{
    class Program
    {
        static void Main(string[] args)
        {
            bool resultGetProvince = false;
            var resultGetDistrict = false;
            var resultGetWard = false;

            var addressControll = new AddressController();
            
            resultGetProvince = addressControll.getAllProvince();
            if (!resultGetProvince) {
                Console.WriteLine("Error: Address Controller - Get ALL Provice");
                return;
            }

            resultGetDistrict = addressControll.getAllDistrict();
            if (!resultGetDistrict) {
                Console.WriteLine("Error: Address Controller - Get ALL District");
                return;
            }

            resultGetWard = addressControll.getAllWard();
            if (!resultGetWard) {
                Console.WriteLine("Error: Address Controller - Get ALL Ward");
                return;
            }

        }
    }
}
