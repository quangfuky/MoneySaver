using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.NetworkOperators;

namespace Entity
{
    public class GiaoDich
    {
        public int ID { get; set; }
        public string Ten { get; set; }
        public int SoTien { get; set; }
        public DateTime Ngay { get; set; }
        public string GhiChu { get; set; }
        public int LoaiGD { get; set; }
    }   
}
