﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuBeefrri.Core.Dtos
{
    public class ProductoOrdenesDTO
    {
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
    }
}
