
// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `PortalConnection`
//     Provider:               `MySql.Data.MySqlClient`
//     Connection String:      `Database=portal;Data Source=127.0.0.1;User Id=root;password=**zapped**;port=3306`
//     Schema:                 ``
//     Include Views:          `False`

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace Model
{
	public partial class DB : Database
	{
		public DB() 
			: base("PortalConnection")
		{
			CommonConstruct();
		}

		public DB(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			DB GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static DB GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new DB();
        }

		[ThreadStatic] static DB _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        
		public class Record<T> where T:new()
		{
			public static DB repo { get { return DB.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public object Insert() { return repo.Insert(this); }
			public void Save() { repo.Save(this); }
			public int Update() { return repo.Update(this); }
			public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
			public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Update<T>(sql); }
			public int Delete() { return repo.Delete(this); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }
		}
	}
	

    
	[TableName("portal.cart")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class cart : DB.Record<cart>  
    {
		[Column] public string ID { get; set; }
		[Column] public string UserID { get; set; }
		[Column] public string ProductID { get; set; }
		[Column] public string UnutID { get; set; }
		[Column] public int Amount { get; set; }
		[Column] public string UserName { get; set; }
		[Column] public string ProductName { get; set; }
		[Column] public string UnitName { get; set; }
		[Column] public DateTime CreatDate { get; set; }
	}
    
	[TableName("portal.favorite")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class favorite : DB.Record<favorite>  
    {
		[Column] public string ID { get; set; }
		[Column] public string UserID { get; set; }
		[Column] public string ProductID { get; set; }
		[Column] public string UnutID { get; set; }
		[Column] public int Amount { get; set; }
		[Column] public string UserName { get; set; }
		[Column] public string ProductName { get; set; }
		[Column] public string UnitName { get; set; }
		[Column] public DateTime CreatDate { get; set; }
	}
    
	[TableName("portal.meal")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class meal : DB.Record<meal>  
    {
		[Column] public string ID { get; set; }
		[Column] public string Code { get; set; }
		[Column] public string Name { get; set; }
		[Column] public string StationID { get; set; }
		[Column] public int IsActive { get; set; }
		[Column] public DateTime CreateDate { get; set; }
		[Column] public string ImgUrl { get; set; }
		[Column] public string StationName { get; set; }
		[Column] public string StaffCode { get; set; }
		[Column] public string StaffName { get; set; }
		[Column] public decimal Price { get; set; }
		[Column] public decimal OriginalPrice { get; set; }
		[Column] public int IsMember { get; set; }
	}
    
	[TableName("portal.mealdetail")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class mealdetail : DB.Record<mealdetail>  
    {
		[Column] public string ID { get; set; }
		[Column] public string PID { get; set; }
		[Column] public string ProductID { get; set; }
		[Column] public string ProductName { get; set; }
		[Column] public decimal Price { get; set; }
		[Column] public string ImgUrl { get; set; }
		[Column] public decimal Amount { get; set; }
		[Column] public decimal OriginalPrice { get; set; }
		[Column] public string ProductCode { get; set; }
	}
    
	[TableName("portal.oldprice")]
	[ExplicitColumns]
    public partial class oldprice : DB.Record<oldprice>  
    {
		[Column] public string ID { get; set; }
		[Column] public string ProductID { get; set; }
		[Column] public decimal Price { get; set; }
		[Column] public string StaffCode { get; set; }
		[Column] public string StaffName { get; set; }
		[Column] public DateTime CreateDate { get; set; }
	}
    
	[TableName("portal.order")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class order : DB.Record<order>  
    {
		[Column] public string ID { get; set; }
		[Column] public string OrderNo { get; set; }
		[Column] public int State { get; set; }
		[Column] public string UserID { get; set; }
		[Column] public string UserName { get; set; }
		[Column] public decimal PMoney { get; set; }
		[Column] public decimal Money { get; set; }
		[Column] public decimal Amount { get; set; }
		[Column] public DateTime CreateDate { get; set; }
		[Column] public string Remark { get; set; }
	}
    
	[TableName("portal.orderdetail")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class orderdetail : DB.Record<orderdetail>  
    {
		[Column] public string ID { get; set; }
		[Column] public string OrderNo { get; set; }
		[Column] public string ProductID { get; set; }
		[Column] public string ProductName { get; set; }
		[Column] public string UnitID { get; set; }
		[Column] public string UnitName { get; set; }
		[Column] public decimal Amount { get; set; }
		[Column] public decimal Money { get; set; }
		[Column] public decimal PMoney { get; set; }
		[Column] public decimal Price { get; set; }
		[Column] public decimal PPrice { get; set; }
	}
    
	[TableName("portal.price")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class price : DB.Record<price>  
    {
		[Column] public string ID { get; set; }
		[Column] public string ProductID { get; set; }
		[Column] public decimal Price { get; set; }
		[Column] public decimal MemberPrice { get; set; }
		[Column] public string UnitCode { get; set; }
		[Column] public string UnitName { get; set; }
		[Column] public string StaffCode { get; set; }
		[Column] public string StaffName { get; set; }
		[Column] public DateTime CreateDate { get; set; }
	}
    
	[TableName("portal.product")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class product : DB.Record<product>  
    {
		[Column] public string ID { get; set; }
		[Column] public string Code { get; set; }
		[Column] public string Name { get; set; }
		[Column] public string TypeCode { get; set; }
		[Column] public string TypeName { get; set; }
		[Column] public string UnitCode { get; set; }
		[Column] public string UnitName { get; set; }
		[Column] public DateTime CreateDate { get; set; }
		[Column] public int IsActive { get; set; }
		[Column] public string ImgUrl { get; set; }
		[Column] public string ImgUrl2 { get; set; }
		[Column] public string ImgUrl3 { get; set; }
		[Column] public string Detail { get; set; }
		[Column] public int Sales { get; set; }
	}
    
	[TableName("portal.producttype")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class producttype : DB.Record<producttype>  
    {
		[Column] public string ID { get; set; }
		[Column] public string Code { get; set; }
		[Column] public string Name { get; set; }
		[Column] public int IsActive { get; set; }
		[Column] public DateTime CreateDate { get; set; }
		[Column] public string StaffID { get; set; }
		[Column] public string StaffName { get; set; }
		[Column] public string ImgUrl { get; set; }
		[Column] public string TopCategoryID { get; set; }
		[Column] public string TopCategoryName { get; set; }
	}
    
	[TableName("portal.productunit")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class productunit : DB.Record<productunit>  
    {
		[Column] public string ID { get; set; }
		[Column] public string Name { get; set; }
		[Column] public string Code { get; set; }
		[Column] public int IsActive { get; set; }
		[Column] public DateTime CreateDate { get; set; }
		[Column] public string StaffID { get; set; }
		[Column] public string StaffName { get; set; }
	}
    
	[TableName("portal.promot")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class promot : DB.Record<promot>  
    {
		[Column] public string ID { get; set; }
		[Column] public string Code { get; set; }
		[Column] public string Name { get; set; }
		[Column] public string StationID { get; set; }
		[Column] public int IsActive { get; set; }
		[Column] public DateTime CreateDate { get; set; }
		[Column] public string ImgUrl { get; set; }
		[Column] public string StationName { get; set; }
		[Column] public string StaffCode { get; set; }
		[Column] public string StaffName { get; set; }
		[Column] public string PromotType { get; set; }
		[Column] public int IsMember { get; set; }
	}
    
	[TableName("portal.promotdetail")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class promotdetail : DB.Record<promotdetail>  
    {
		[Column] public string ID { get; set; }
		[Column] public string PID { get; set; }
		[Column] public string ProductID { get; set; }
		[Column] public decimal OriginalPrice { get; set; }
	}
    
	[TableName("portal.promotrule")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class promotrule : DB.Record<promotrule>  
    {
		[Column] public string ID { get; set; }
		[Column] public string PID { get; set; }
		[Column] public string Text { get; set; }
		[Column] public decimal ExePrice { get; set; }
		[Column] public decimal Condition { get; set; }
	}
    
	[TableName("portal.staff")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class staff : DB.Record<staff>  
    {
		[Column] public string ID { get; set; }
		[Column] public string UserCode { get; set; }
		[Column] public string UserName { get; set; }
		[Column] public string PassWord { get; set; }
		[Column] public string StationID { get; set; }
		[Column] public int IsActive { get; set; }
		[Column] public string Phone { get; set; }
		[Column] public DateTime CreateDate { get; set; }
		[Column] public string StationName { get; set; }
		[Column] public string StationCode { get; set; }
	}
    
	[TableName("portal.station")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class station : DB.Record<station>  
    {
		[Column] public string ID { get; set; }
		[Column] public string Code { get; set; }
		[Column] public string Name { get; set; }
		[Column] public string Address { get; set; }
		[Column] public decimal Latitude { get; set; }
		[Column] public decimal Longitude { get; set; }
		[Column] public int IsActive { get; set; }
		[Column] public DateTime CreateDate { get; set; }
	}
    
	[TableName("portal.swiper")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class swiper : DB.Record<swiper>  
    {
		[Column] public string ID { get; set; }
		[Column] public string ProductName { get; set; }
		[Column] public string ProductID { get; set; }
		[Column] public string ImgUrl { get; set; }
		[Column] public int Index { get; set; }
		[Column] public int IsActive { get; set; }
		[Column] public DateTime CreateDate { get; set; }
	}
    
	[TableName("portal.userinfo")]
	[PrimaryKey("ID", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class userinfo : DB.Record<userinfo>  
    {
		[Column] public string ID { get; set; }
		[Column] public string UserName { get; set; }
		[Column] public string UserCode { get; set; }
		[Column] public string PassWord { get; set; }
		[Column] public int IsMember { get; set; }
		[Column] public int IsActive { get; set; }
		[Column] public DateTime CreateDate { get; set; }
		[Column] public string Phone { get; set; }
		[Column] public string ImgUrl { get; set; }
		[Column] public DateTime? MemberDate { get; set; }
	}
}
