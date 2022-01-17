using FM.Application.Common.Interfaces;
using FM.Domain.Common;
using FM.Domain.Entities;
using FM.Domain.Entities.Facebook;
using FM.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FM.Infrastructure.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<FbPostGroup> FbPostGroups { get; set; }

        public DbSet<FbPostGroupAction> FbPostGroupActions { get; set; }

        public DbSet<FbPostGroupComment> FbPostGroupComments { get; set; }

        public DbSet<FbPostGroupPrivacy> FbPostGroupPrivacy { get; set; }

        public DbSet<FbToken> FbTokens { get; set; }

        public DbSet<FbAccount> FbAccounts { get; set; }

        public DbSet<HealthHistory> HealthHistories { get; set; }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //SeedAccountDefault.SeedData(builder);
            SeedFbToken();
        }

        private void SeedFbToken()
        {

        }

        private static T ReadFromExcel<T>(string path, bool hasHeader = true)
        {
            using (var excelPack = new ExcelPackage())
            {
                //Load excel stream
                using (var stream = File.OpenRead(path))
                {
                    excelPack.Load(stream);
                }

                //Lets Deal with first worksheet.(You may iterate here if dealing with multiple sheets)
                var ws = excelPack.Workbook.Worksheets[0];

                //Get all details as DataTable -because Datatable make life easy :)
                DataTable excelasTable = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    //Get colummn details
                    if (!string.IsNullOrEmpty(firstRowCell.Text))
                    {
                        string firstColumn = string.Format("Column {0}", firstRowCell.Start.Column);
                        excelasTable.Columns.Add(hasHeader ? firstRowCell.Text : firstColumn);
                    }
                }
                var startRow = hasHeader ? 2 : 1;
                //Get row details
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, excelasTable.Columns.Count];
                    DataRow row = excelasTable.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                //Get everything as generics and let end user decides on casting to required type
                var generatedType = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(excelasTable));
                return (T)Convert.ChangeType(generatedType, typeof(T));
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in ChangeTracker.Entries<AudiTableEntity<Guid>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.Uid;
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUserService.Uid;
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}