using Azure.Storage.Blobs;
using JobManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Services
{
    class JobDataStoreBlobStorageJson : IJobDataStore<Job>
    {
        private static string ConnectionString =>
    "DefaultEndpointsProtocol=https;AccountName202205jobmanager;AccountKey=;EndppointSuffix=core.windows.net";
        private static string Container => "data";
        private static sstring FileName => "Jobs.json";
        private readonly BlobServiceClient service = new BlobServiceClient(ConnectionString);

        public async Task AddJob(Job job)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteJob(Job job)
        {
            throw new NotImplementedException();
        }

        public async Task<Job> GetJob(int jobId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            throw new NotImplementedException();
        }

        public Task UpdateJob(Job job)
        {
            throw new NotImplementedException();
        }
    }
}
