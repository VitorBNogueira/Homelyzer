using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SortDTO
    {
        public string OrderBy { get; set; }
        public EDirection Direction { get; set; }
    }

    public enum EDirection
    {
        Asc = 1,
        Desc = 2
    }
}
