﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Marca
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public int IdProveedor {get; set;}
        public string ImagenUrl { get; set; }
        public bool Activo { get; set; }

    }
}