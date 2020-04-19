using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql;

namespace PostgressExe
{
    //// スキーマはMySchema
    //// テーブルEncodeInfo
    //[Table("EncodingInfo")]
    //class EncodingInfo
    //{
    //    [Key] //主キー列の宣言
    //    [Column("id")] //列名
    //    public int encode_info_id { get; set; }

    //    [Index(IsUnique = true)]
    //    [Column("code_page")]
    //    public int CodePage { get; set; }

    //    [StringLength(80)]
    //    [Column("name")]
    //    public string Name { get; set; }

    //    [StringLength(80)]
    //    [Column("display_name")]
    //    public string DisplayName { get; set; }
    //}

    //// テーブルAppEncoding
    //[Table("AppEncoding")]
    //class AppEncoding
    //{
    //    [Key] //主キー列の宣言
    //    [Column("app_encoding_id")] //列名
    //    public int Id { get; set; }

    //    [Index(IsUnique = true)]
    //    [Column("code_page")] //列名
    //    public int CodePage { get; set; }

    //    [StringLength(80)]
    //    [Column("app_name")]
    //    public string AppName { get; set; }
    //}

    class MyContext : DbContext
    {
        public static string connString = @"Server=localhost;User id=test;Password=testtest;Database=testDb";

        public MyContext(DbConnection conn) : base(conn, false/*コンテキスト破棄時にコネクションは破棄しない*/)
        {
        }

        /// <summary>
        /// テーブルEncodingInfo。
        /// </summary>
        public DbSet<EncodingInfo> EncodingInfos { get; set; }

        /// <summary>
        /// テーブルEncodingInfo。
        /// </summary>
        public DbSet<AppEncoding> AppEncodings { get; set; }

        /// <summary>
        /// コンテキスト（本クラス）が初期化されたときの呼ばれる関数。
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 初回DBアクセス時に呼び出される初期化用クラス（インターフェイス）を設定（というか、初期化無しにした例）
            //Database.SetInitializer<MyContext>(null);
            var mb = modelBuilder.HasDefaultSchema("public");

            //var entity = modelBuilder.Entity<EncodingInfo>();
            //entity.ToTable("EncodingInfo", "MySchema");
            //entity.HasKey(e => e.encode_info_id);
            //entity.Property(e => e.encode_info_id)
            //    .HasColumnName("id");
            //entity.Property(e => e.CodePage)
            //    .HasColumnName("code_page");
            //entity.Property(e => e.DisplayName)
            //    .HasColumnName("display_name");
            //entity.Property(e => e.Name)
            //    .HasColumnName("name");

            //var entityA = modelBuilder.Entity<AppEncoding>();
            //entityA.ToTable("AppEncoding", "MySchema");
            //entityA.HasKey(e => e.Id);
            //entityA.Property(e => e.Id)
            //    .HasColumnName("app_encoding_id");
            //entityA.Property(e => e.CodePage)
            //    .HasColumnName("code_page");
            //entityA.Property(e => e.AppName)
            //    .HasColumnName("app_name");

            //base.OnModelCreating(modelBuilder);
        }
    }

    /// <summary>
    /// コンテキストクラス。
    /// データベースとテーブル/スキーマクラス定義（ここでは、EncodingInfoとAppEncoding）を橋渡しするためのクラス。
    /// 本クラスを使い、DBへのクエリ―を実行する。
    /// </summary>
    class MyContextE : MyContext
    {
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public MyContextE(DbConnection conn) : base(conn)
        {
        }

        /// <summary>
        /// テーブルEncodingInfo。
        /// </summary>
        public DbSet<EncodingInfo> EncodingInfos { get; set; }
    }

    /// <summary>
    /// コンテキストクラス。
    /// データベースとテーブル/スキーマクラス定義（ここでは、EncodingInfoとAppEncoding）を橋渡しするためのクラス。
    /// 本クラスを使い、DBへのクエリ―を実行する。
    /// </summary>
    class MyContextA : MyContext
    {
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public MyContextA(DbConnection conn) : base(conn)
        {
        }

        /// <summary>
        /// テーブルEncodingInfo。
        /// </summary>
        public DbSet<AppEncoding> AppEncodings { get; set; }

    }

    /// <summary>
    /// サマリーだぞ！！
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var context = new TestModel())
                {
                    //context.Database.Delete();
                    context.Database.CreateIfNotExists();
                    context.MyEntities.Add(new MyEntity()
                    {
                        //Id = 123,
                        Name = "Name0000001"
                    });
                    context.SaveChanges();
                }
                //using (var conn = new NpgsqlConnection(MyContext.connString))
                //{
                //    conn.Open();
                //    //// AppInfoテーブル編集
                //    ////using (var context = new MyContextA(conn))
                //    //using (var context = new MyContext(conn))
                //    //{
                //    //    context.Database.CreateIfNotExists();
                //    //    var table = context.Set<AppEncoding>();
                //    //    table.Add(new AppEncoding()
                //    //    {
                //    //        CodePage = 932,
                //    //        AppName = "App-sjis"
                //    //    });
                //    //    context.SaveChanges();
                //    //}
                //    //// EncodingInfoテーブル編集
                //    using (var context = new MyContext(conn))
                //    //using (var context = new MyContextE(conn))
                //    {
                //        context.EncodingInfos.Add(new EncodingInfo()
                //        {
                //            CodePage = 932,
                //            Name = "shift_jis",
                //            DisplayName = "Japanese (Shift-JIS)"
                //        });
                //        context.EncodingInfos.Add(new EncodingInfo()
                //        {
                //            CodePage = 20932,
                //            Name = "EUC-JP",
                //            DisplayName = "Japanese (JIS 0208-1990 and 0212-1990)"
                //        });
                //        context.EncodingInfos.Add(new EncodingInfo()
                //        {
                //            CodePage = 65001,
                //            Name = "utf-8",
                //            DisplayName = "Unicode (UTF-8)"
                //        });
                //        context.SaveChanges();
                //    }
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
            // 最後に閉じる
        }
    }
}

