using DapperPoc.Samples;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperPoc.UnitTests
{
    [TestFixture]
    class SamplesFactoryTests
    {
        /// <summary>
        /// Getses the sample sample type is multi mapping child entities returns i sample.
        /// </summary>
        [Test]
        public void getsSample_sampleTypeIsMultiMappingChildEntities_returnsISample()
        {
            ISamples sample = SamplesFactory.GetSample(Samples.SampleTypes.MultiMappingChildEntities);
            Assert.IsTrue(sample != null);
        }

        /// <summary>
        /// Getses the sample sample type is multi mapping single entity returns i sample.
        /// </summary>
        [Test]
        public void getsSample_sampleTypeIsMultiMappingSingleEntity_returnsISample()
        {
            ISamples sample = SamplesFactory.GetSample(Samples.SampleTypes.MultiMappingSingleEntity);
            Assert.IsTrue(sample != null);
        }

        /// <summary>
        /// Getses the sample sample type is multiple queries returns i sample.
        /// </summary>
        [Test]
        public void getsSample_sampleTypeIsMultipleQueries_returnsISample()
        {
            ISamples sample = SamplesFactory.GetSample(Samples.SampleTypes.MultipleQueries);
            Assert.IsTrue(sample != null);
        }

        /// <summary>
        /// Getses the sample sample type is parameterized select statement returns i sample.
        /// </summary>
        [Test]
        public void getsSample_sampleTypeIsParameterizedSelectStatement_returnsISample()
        {
            ISamples sample = SamplesFactory.GetSample(Samples.SampleTypes.ParameterizedSelectStatement);
            Assert.IsTrue(sample != null);
        }

        /// <summary>
        /// Getses the sample sample type is simple insert statement returns i sample.
        /// </summary>
        [Test]
        public void getsSample_sampleTypeIsSimpleInsertStatement_returnsISample()
        {
            ISamples sample = SamplesFactory.GetSample(Samples.SampleTypes.SimpleInsertStatement);
            Assert.IsTrue(sample != null);
        }

        /// <summary>
        /// Getses the sample sample type is simple select statement returns i sample.
        /// </summary>
        [Test]
        public void getsSample_sampleTypeIsSimpleSelectStatement_returnsISample()
        {
            ISamples sample = SamplesFactory.GetSample(Samples.SampleTypes.SimpleSelectStatement);
            Assert.IsTrue(sample != null);
        }

        /// <summary>
        /// Getses the sample sample type is simple select statement with dynamic entities returns i sample.
        /// </summary>
        [Test]
        public void getsSample_sampleTypeIsSimpleSelectStatementWithDynamicEntities_returnsISample()
        {
            ISamples sample = SamplesFactory.GetSample(Samples.SampleTypes.SimpleSelectStatementWithDynamicEntities);
            Assert.IsTrue(sample != null);
        }

        /// <summary>
        /// Getses the sample sample type is simple stored procedure returns i sample.
        /// </summary>
        [Test]
        public void getsSample_sampleTypeIsSimpleStoredProcedure_returnsISample()
        {
            ISamples sample = SamplesFactory.GetSample(Samples.SampleTypes.SimpleStoredProcedure);
            Assert.IsTrue(sample != null);
        }
    }
}
