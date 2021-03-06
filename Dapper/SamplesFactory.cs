﻿using System;
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
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplesFactory"/> class.
        /// </summary>
        /// <param name="listBoxAdapter">The list box adapter.</param>
        public SamplesFactory(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Executes the specified sample type.
        /// </summary>
        /// <param name="sampleType">Type of the sample.</param>
        /// <param name="args">The arguments.</param>
        public void Execute(SampleTypes sampleType, object[] args = null)
        {
            logger.WriteSeparator();
            ISamples sample = GetSample(sampleType);
            using(var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServer"].ConnectionString))
            {
                sample.Execute(sqlConnection, logger, args);
            }
            logger.WriteSeparator();
        }

        /// <summary>
        /// Gets the sample.
        /// </summary>
        /// <param name="sampleType">Type of the sample.</param>
        /// <returns></returns>
        public static ISamples GetSample(SampleTypes sampleType)
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
