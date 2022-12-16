using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;

namespace AeroSalesTests
{
    [TestClass]
    public class postPageTests
    {
        int id = 0;
        string name_of_post = "";
        float salary = 0;
        NpgsqlConnection connect = new NpgsqlConnection("Host=localhost;Port=5432;Database=AeroSales;Username=postgres;Password=a;");

        [TestMethod]
        public void aPostInsertTest_Uborshchikand15000_1returned()
        {
            //arrange
            connect.Open();
            name_of_post = "Uborshchik";
            salary = 15000;
            string expected = "1";
            //act
            new NpgsqlCommand($@"call post_insert('{name_of_post}','{salary}')", connect).ExecuteNonQuery();
            string actual = new NpgsqlCommand($"select count(*) from post where name_of_post='{name_of_post}' and salary='{salary}'", connect).ExecuteScalar().ToString();
            //assert
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void bPostUpdateTest_2andWebdesignerand100000_1returned()
        {
            //arrange
            connect.Open();
            id = 0;
            name_of_post = "Uborshchik";
            salary = 15000;
            NpgsqlDataReader dataReader = null;
            dataReader = new NpgsqlCommand($"select id_post from post where name_of_post='{name_of_post}' and salary='{salary}'", connect).ExecuteReader();
            while (dataReader.Read())
                id = (int)dataReader["id_post"];
            dataReader.Close();

            name_of_post = "Webdesigner";
            salary = 100000;
            string expected = "1";
            //act
            new NpgsqlCommand($@"call post_update('{id}','{name_of_post}','{salary}')", connect).ExecuteNonQuery();
            string actual = new NpgsqlCommand($"select count(*) from post where name_of_post='{name_of_post}' and salary='{salary}'", connect).ExecuteScalar().ToString();
            //assert
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void cPostDeleteTest_NameofPostandSalary_0returned()
        {
            //arrange
            connect.Open();
            id = 0;
            name_of_post = "Webdesigner";
            salary = 100000;
            NpgsqlDataReader dataReader = null;
            dataReader = new NpgsqlCommand($"select id_post from post where name_of_post='{name_of_post}' and salary='{salary}'", connect).ExecuteReader();
            while (dataReader.Read())
                id = (int)dataReader["id_post"];
            dataReader.Close();

            string expected = "0";
            new NpgsqlCommand($@"call post_delete('{id}')", connect).ExecuteNonQuery();
            string actual = new NpgsqlCommand($"select count(*) from post where name_of_post='{name_of_post}' and salary='{salary}'", connect).ExecuteScalar().ToString();
            //assert
            Assert.AreEqual(expected, actual);

        }
    }
}
