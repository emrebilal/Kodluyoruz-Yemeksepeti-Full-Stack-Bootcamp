using Dapper;
using DapperSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DapperSampleController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string connectionStr;
        public DapperSampleController(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionStr = _configuration.GetConnectionString("DefaultConnection");
        }

        //Select In Query
        /* Business işlemlerini yapmadan önce db ile iletişim için connection açıyoruz
         * Tabloda bulunan kayıtları istenilen şartlara göre SQL sorguları ile çekebiliriz
         * Bu metottaki sorgu Ankara şehrindeki kayıtları getiriyor
         * Query() metodunda çalıştırılan sorgu ile dönen veriler modele map ediliyor
         */
        [HttpGet]
        public IActionResult Select()
        {
            IEnumerable<UserModel> data = new List<UserModel>();

            using (var dbConnection = new SqlConnection(connectionStr))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                data = dbConnection.Query<UserModel>("SELECT * FROM DapperUser WHERE City IN ('Ankara')");
            }
            return Ok(data);
        }

        //Insert
        /* Execute() metodu veritabanında insert, update ve ya delete işlemlerinde kullanılır
         * Execute(), bir komutu birden çok kez çalıştırabilir ve etkilenen satırların sayısını döndürebilir
         * Bu metot parametre olarak verilen model nesnesini insert ediyor
         */
        [HttpPost]
        public IActionResult Insert([FromBody] UserModel user)
        {
            try
            {
                string sql = @"INSERT INTO DapperUser (Id,Name,Age,City) VALUES (@Id,@Name,@Age,@City)";
                using (var dbConnection = new SqlConnection(connectionStr))
                {
                    if (dbConnection.State == ConnectionState.Closed)
                        dbConnection.Open();

                    dbConnection.Execute(sql, user);
                    return Ok(user);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        //Update
        /* Güncelleme metodundaki sorgu body içeriğinde hangi alanlar değiştirilirse onları güncelliyor
         * Sorgu Id'ye göre kaydı döndürüyor
         * Execute() içinde model type'a göre işlemleri yazıyoruz
         * Etkilenen satır sayısı var ise işlemi dönüyoruz
         */
        [HttpPut]
        public IActionResult Update([FromBody] UserModel user)
        {
            try
            {
                string sql = @"UPDATE DapperUser SET Name=@_Name,Age=@_Age,City=@_City WHERE Id=@_Id";
                using (var dbConnection = new SqlConnection(connectionStr))
                {
                    if (dbConnection.State == ConnectionState.Closed)
                        dbConnection.Open();

                    var affected = dbConnection.Execute(sql, new
                    {
                        _Id = user.Id,
                        _Name = user.Name,
                        _Age = user.Age,
                        _City = user.City
                    });

                    if (affected == 1)
                    {
                        return Ok(user);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Delete
        /* Kayıt silme işlemi gönderilen Id'ye göre veritabanından bulunup siliniyor
         * Burada da Execute() ile gönderilen id'yi eşliyoruz, kayıt var ise siliniyor
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string sql = @"DELETE FROM DapperUser WHERE Id=@_Id";
            using (var dbConnection = new SqlConnection(connectionStr))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                var affected = dbConnection.Execute(sql, new { _Id = id });

                if (affected == 1)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }

        //Delete In Query
        /* Bu metottaki sorgu 18 yaş altındaki kayıtları siliyor
         * IN içinde ayrı bir sorgu çalıştırabiliriz
         * Sorgu içinde şartları yazdıktan sonra yine Execute() ile çalıştırıyoruz
         */
        [HttpDelete]
        public IActionResult DeleteIn()
        {
            string sql = @"DELETE FROM DapperUser WHERE Age IN (SELECT Age FROM DapperUser WHERE Age<18)";
            using (var dbConnection = new SqlConnection(connectionStr))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                var affected = dbConnection.Execute(sql);

                return Ok(affected);
            }
        }

        //Stored Procedure
        /* CREATE PROCEDURE GetAll
           AS
           BEGIN
                SELECT * FROM DapperSample
           END
         *
         * Yukarıdaki query veritabanı üzerinde bütün kayıtları getiren GetAll adında stored procedure tanımlıyor
         * Bu işlem sonrasında Query() metodu içinde sp adını yazıyoruz ve sonrasında komut türünün sp olduğunu belirtiyoruz
         */
        [HttpGet]
        public IActionResult SPSelect()
        {
            using (var dbConnection = new SqlConnection(connectionStr))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                IEnumerable<UserModel> data = dbConnection.Query<UserModel>("GetAll", commandType: CommandType.StoredProcedure);

                return Ok(data);
            }
        }

        //Transactional Insert
        /* Transaction bir veya daha fazla SQL ifadesini bir bütün olarak çalıştırır
         * Bir hata olması durumunda işlemler geri alınır
         * Bu metotta PostModel içerisindeki bir bütün olan iki modelimiz ayrı tablolara aynı anda insert edilmesi gerekiyor
         * Transaction içinde Execute() metodu ile işlemleri yapıyoruz
         * İki sorgumuz da sorunsuz çalışırsa commit ediyoruz
         */
        [HttpPost]
        public IActionResult TransactionInsert([FromBody] PostModel postModel)
        {
            string sqlUser = @"INSERT INTO DapperUser (Id,Name,Age,City) VALUES (@Id,@Name,@Age,@City)";
            string sqlContact = @"INSERT INTO DapperContact (Id,UserId,ContactName) VALUES (@Id,@UserId,@ContactName)";

            using (var dbConnection = new SqlConnection(connectionStr))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                using (var transaction = dbConnection.BeginTransaction())
                {
                    var affectedUser = dbConnection.Execute(sqlUser, postModel.User, transaction);
                    var affectedContact = dbConnection.Execute(sqlContact, postModel.Contact, transaction);

                    if (affectedUser == 0 || affectedContact == 0)
                    {
                        transaction.Rollback();
                        return BadRequest();
                    }

                    transaction.Commit();
                    return Ok(postModel);
                }
            }
        }

        //Result Mapping
        /* Bu metot get üzerinden alınan şehir bilgisini eşleyerek o şehirdeki bilgileri getiriyor
         * Query() metodunda çalıştırılan sorgu ile dönen veriler modele map ediliyor
         */
        [HttpGet("{city}")]
        public IActionResult GetByCity(string city)
        {
            string sql = @"SELECT * FROM DapperUser WHERE City = @City";
            using (var dbConnection = new SqlConnection(connectionStr))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                IEnumerable<UserModel> data = dbConnection.Query<UserModel>(sql, new { City = city });

                return Ok(data);
            }
        }

        //One to Many Mapping
        /* Query() metodu bir sql sonucunu bire çok ilişkiye sahip güçlü bir şekilde yazılmış bir listeyle eşleştirebilir
         * Sorguyu aynı sql içinde yazarak gönderebiliriz
         * Bu action id'ye göre kullanıcı bilgilerini getiriyor, kullanıcı bilgileri birbirine bağlı iki ayrı tablodan map ediliyor
         */
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            string sql = @"SELECT * FROM DapperUser WHERE Id = @Id;
				           SELECT * FROM DapperContact WHERE UserId = @Id";
            using (var dbConnection = new SqlConnection(connectionStr))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                var results = dbConnection.QueryMultiple(sql, new { Id = id });

                if (results.Read<UserModel>().Any())
                {
                    var user = results.ReadSingle<UserModel>();
                    user.Contacts = results.Read<Contact>().ToList();

                    return Ok(user);
                }

                return NotFound();
            }
        }

        //One to One Mapping
        /* Query() metodu aynı şekilde bir sql sonucunu bire bir ilişkiye sahip güçlü bir şekilde yazılmış bir listeyle de eşleştirebilir
         * Tek bir SQL sorgusundan dönen tek sonuç kümesini, birden çok nesneyle eşlemek için Dapper'da bulunan çoklu eşleme özelliği kullanalarak yapabiliriz
         * Action gönderdiğimiz id ile ilişkili verileri getirerek modelimize map ediyor
         */
        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            string sql = @"SELECT c.Id, c.UserId, c.ContactName,
                                  a.Id as AddressId, a.ContactId, a.AddressInfo
                                  FROM DapperContact c, DapperAddress a
                                  WHERE c.Id = a.ContactId AND c.Id = @Id";

            using (var dbConnection = new SqlConnection(connectionStr))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                var contact = dbConnection.Query<Contact, Address, Contact>(sql, 
                (contact, address) => {
                    contact.Address = address;
                    return contact;

                }, new { Id = id }, splitOn: "AddressId");

                return Ok(contact);
            }
        }

        //Multiple Query
        /* Aynı sorgu içerisinde birden fazla komut yazabiliriz
         * QueryMultiple() ile bu sorguyu çalıştırıyoruz
         * Veritabanından dönen verileri Read() ile okuyarak modellerimize map ediyoruz
         */
        [HttpGet]
        public IActionResult GetAll()
        {
            string sql = @"SELECT * FROM DapperUser;
				           SELECT * FROM DapperContact;
                           SELECT * FROM DapperAddress";
            using (var dbConnection = new SqlConnection(connectionStr))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                var results = dbConnection.QueryMultiple(sql);

                var user = results.Read<UserModel>();
                var contact = results.Read<Contact>();
                var address = results.Read<Address>();

                return Ok(new { DapperUser = user, DapperContact = contact, DapperAddress = address });
            }
        }
    }
}
