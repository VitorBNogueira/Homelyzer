using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Picture
    {
        [Key]
        public int PictureId { get; set; }
        public int AdvertId { get; set; }
        public string Url { get; set; }

        public Advert Advert { get; set; }

        public bool IsActive { get; set; }
    }
}
