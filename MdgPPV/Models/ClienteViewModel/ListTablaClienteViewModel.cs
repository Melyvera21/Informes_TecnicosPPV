﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MdgPPV.Models.ClienteViewModel
{
    public class ListTablaClienteViewModel
    {
        public int id { get; set; }
        public string RazonSocial { get; set; }
        public string NombreFantasia { get; set; }
        public string Direccion { get; set; }
        public string Cuit { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Responsable { get; set; }
    }
}