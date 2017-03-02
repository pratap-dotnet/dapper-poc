using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DapperPoc.Samples
{
    internal class SimpleSelectStatement : ISamples
    {
        class Person
        {
            public string PersonType { get; set; }
            public bool NameStyle { get; set; }
            public string Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public string Suffix { get; set; }
            public int EmailPromotion { get; set; }
            public string AdditionalContactInfo { get; set; }
            public Guid rowguid { get; set; }
            public DateTime ModifiedDate { get; set; }
        }

        public void Execute(IDbConnection dbConnection, ILogger listBoxAdapter, object[] args = null)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var query = @"SELECT * FROM PERSON.PERSON";
            var persons = dbConnection.Query<Person>(query);
            watch.Stop();
            listBoxAdapter.WriteLine($"Query \"{query}\" executed in {watch.ElapsedMilliseconds} ms and retrieved {persons.Count()} objects of type {typeof(Person)}");
        }
    }
}
