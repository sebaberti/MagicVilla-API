﻿using MagicVilla_API.Models.DTO;

namespace MagicVilla_API.Data
{
    public static class VillaStore
    {
        //esta clase que simula la informacion que nos devuelve  una base de datos.
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto{Id=1, Name="Vista a la piscina" },
            new VillaDto{Id=2,Name="Vista a la playa"}

        };
    }
}
