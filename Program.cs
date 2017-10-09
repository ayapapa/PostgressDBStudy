using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgressExe
{
    // スキーマはMySchema
    // テーブルEncodeInfo
    [Table("EncodingInfo", Schema = "MySchema")]
    class EncodingInfo
    {
        [Key] //主キー列の宣言
        [Column("id")] //列名
        public int encode_info_id { get; set; }

        [Index(IsUnique = true)]
        [Column("code_page")]
        public int CodePage { get; set; }

        [StringLength(80)]
        [Column("name")]
        public string Name { get; set; }

        [StringLength(80)]
        [Column("display_name")]
        public string DisplayName { get; set; }
    }

    // テーブルEncodeInfo
    [Table("AppEncoding", Schema = "MySchema")]
    class AppEncoding
    {
        [Key] //主キー列の宣言
        [Column("app_encoding_id")] //列名
        public int Id { get; set; }

        [Index(IsUnique = true)]
        public int CodePage { get; set; }

        [StringLength(80)]
        [Column("app_name")]
        public string AppName { get; set; }
    }


    class MyContext : DbContext
    {
        public static string connString = @"Server=localhost;User id=test;Password=testtest;Database=testDb";
        public MyContext(DbConnection conn)
        //: base(@"Server=localhost;Port=5432;User Id=test; Password=testtest;Database=testDb")
        //: base("name=Postgres")
        //: base(@"Server=localhost; Port=5432;User Id = test; Password=testtest;Database=testDb; providerName=Npgsql")
        : base(conn, true)
        {
        }

        public DbSet<EncodingInfo> EncodingInfos { get; set; }
        public DbSet<AppEncoding> AppEncoding { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<MyContext>(null);
            //modelBuilder.HasDefaultSchema("MySchema");
        }

    }

    public class Program
    {
        static void Main(string[] args)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("Npgsql");
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = MyContext.connString;
            conn.Open();
            using (var context = new MyContext(conn))
            {
                try
                {
                    context.EncodingInfos.Add(new EncodingInfo()
                    {
                        CodePage = 932,
                        Name = "shift_jis",
                        DisplayName = "Japanese (Shift-JIS)"
                    });
                    //context.EncodingInfos.Add(new EncodingInfo()
                    //{
                    //    CodePage = 20932,
                    //    Name = "EUC-JP",
                    //    DisplayName = "Japanese (JIS 0208-1990 and 0212-1990)"
                    //});
                    //context.EncodingInfos.Add(new EncodingInfo()
                    //{
                    //    CodePage = 65001,
                    //    Name = "utf-8",
                    //    DisplayName = "Unicode (UTF-8)"
                    //});

                    //context.AppEncoding.Add(new AppEncoding()
                    //{
                    //    CodePage = 932,
                    //    AppName = "App-sjis"
                    //});
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

            }
        }
    }
}

