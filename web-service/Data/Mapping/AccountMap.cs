using dto;
using System.Data.Entity.ModelConfiguration;

namespace web_service.Data.Mapping
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            this.ToTable("Account");
            this.HasKey(a => a.Id);
        }
    }
}