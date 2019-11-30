using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelDeBotel.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            IsDeleted = false;
        }

        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}