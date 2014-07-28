using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using CustoDN.Domain;
using Highway.Data;
using Microsoft.Ajax.Utilities;

namespace CustoDN.Web
{
    public class A : Domain.A
    {
        public static DataContext NewDbDataContext()
        {
            var foo = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            return new DataContext(
                foo.ConnectionString,
                new MappingConfig());
        }
    }

    public class An : Domain.An
    {
        public static Nexus EmptyDbNexus()
        {
            return new Nexus(new Repository(A.NewDbDataContext()));
        }
    }
}