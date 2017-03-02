using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperPoc.Samples;

namespace DapperPoc
{
    class SamplesFactory
    {
        private readonly ILogger listBoxAdapter;
        public SamplesFactory(ILogger listBoxAdapter)
        {
            this.listBoxAdapter = listBoxAdapter;
        }

        public void Execute(SampleTypes sampleType, object[] args = null)
        {
            listBoxAdapter.WriteSeparator();
            ISamples sample = GetSample(sampleType);
            using(var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServer"].ConnectionString))
            {
                sample.Execute(sqlConnection, listBoxAdapter, args);
            }
            listBoxAdapter.WriteSeparator();
        }

        private static ISamples GetSample(SampleTypes sampleType)
        {
            ISamples sample = null;
            switch (sampleType)
            {
                case SampleTypes.SimpleSelectStatement:
                    sample = new SimpleSelectStatement();
                    break;
                case SampleTypes.SimpleSelectStatementWithDynamicEntities:
                    sample = new SimpleSelectStatementWithDynamicEntities();
                    break;
                case SampleTypes.MultiMappingSingleEntity:
                    sample = new MultiMappingSelectStatement();
                    break;
                case SampleTypes.MultiMappingChildEntities:
                    sample = new MultiMappingOneToManyRelationSelectStatement();
                    break;
                case SampleTypes.ParameterizedSelectStatement:
                    sample = new ParameterizedSelectStatement();
                    break;
                case SampleTypes.MultipleQueries:
                    sample = new MultipleQueries();
                    break;
                case SampleTypes.SimpleStoredProcedure:
                    sample = new SimpleStoredProcedure();
                    break;
                case SampleTypes.SimpleInsertStatement:
                    sample = new SimpleInsertStatement();
                    break;
                default:
                    break;
            }

            return sample;
        }
    }
}
