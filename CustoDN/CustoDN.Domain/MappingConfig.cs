using System.Data.Entity;
using Highway.Data;

namespace CustoDN.Domain
{
    public class MappingConfig : IMappingConfiguration
    {
        public void ConfigureModelBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(); //.HasMany(c => c.Many).WithRequired(m => m.Customer);
        }
    } 
}