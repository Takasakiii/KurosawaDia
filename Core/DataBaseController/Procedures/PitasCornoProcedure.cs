using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseController.Procedures
{
    public static class PitasCornoProcedure
    {
        public static void PitasCornoCreateProcedure(this DatabaseFacade migrationBuilder)
        {
            migrationBuilder.GetVerdades();
        }

        private static void GetVerdades(this DatabaseFacade database)
        {
            //migrationBuilder.Sql($"create procedure GetVerdade() begin select 'pitas corno' as verdadeDoUniverso; end;");
            //database.ExecuteSqlInterpolated($"create procedure GetVerdade() begin select 'pitas corno' as verdadeDoUniverso; end;");
            database.ExecuteSqlRaw($"create procedure GetVerdade() begin select 'pitas corno' as verdadeDoUniverso; end;");
        }
    }
}
