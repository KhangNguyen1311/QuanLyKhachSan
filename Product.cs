using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_QuanLiKhachSan
{
    public class Product
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string tinhTrang{ get; set; }

        public Product(string name, double value, string tinhtrang)
        {
            Name = name;
            Value = value;
            tinhTrang = tinhtrang;
        }
    }
}
