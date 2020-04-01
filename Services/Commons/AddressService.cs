using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeliverySave
{
    public class AddressService: IService
    {
        # region Lấy thông tin
        public async Task<List<Address>> getAllProvince() {
            using (var con = new DeliverySaveContext())
            {
                var province = new List<Address>();
                
                await con.Addresses.ForEachAsync(x => {
                    if (x.pid == null) province.Add(x);
                });

                return province;
            }
        }

        public async Task<List<Address>> getAllDistrict(string provinceID) {
            using (var con = new DeliverySaveContext())
            {
                var district = new List<Address>();
                
                await con.Addresses.ForEachAsync(x => {
                    if (x.pid == provinceID) district.Add(x);
                });

                return district;
            }
        }
        #endregion

        #region Khởi tạo
        public async Task<Address> insert(Address data)
        {
            using (var con = new DeliverySaveContext())
            {
                var address = await con.Addresses.FirstOrDefaultAsync(x => x.id == data.id);

                if (address != null) {
                    address.name = data.name;
                    address.pid = data.pid;
                    address.type = data.type;
                    address.region = data.region;
                    address.alias = data.alias;
                    address.is_picked = data.is_picked;
                    address.is_delivered = data.is_delivered;
                    con.SaveChanges();

                    return address;
                }
                else {
                    con.Addresses.Add(data);
                    con.SaveChanges();

                    return data;
                }
            }
        }

        public async Task<List<Address>> insert(List<Address> data)
        {
            using (var con = new DeliverySaveContext())
            {
                var addressNew = new List<Address>();

                foreach(var item in data) {
                    var address = await con.Addresses.FirstOrDefaultAsync(x => x.id == item.id);

                    if (address != null) {
                        address.name = item.name;
                        address.pid = item.pid;
                        address.type = item.type;
                        address.region = item.region;
                        address.alias = item.alias;
                        address.is_picked = item.is_picked;
                        address.is_delivered = item.is_delivered;
                        con.SaveChanges();
                    }
                    else {
                        addressNew.Add(item);
                    }
                }

                con.Addresses.AddRange(addressNew);
                con.SaveChanges();

                return data;
            }
        }
        #endregion
    }
}