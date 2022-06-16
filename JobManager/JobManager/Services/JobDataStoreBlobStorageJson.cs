using Azure.Storage.Blobs;
using JobManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Services
{
    class JobDataStoreBlobStorageJson : IJobDataStore<Job>
    {
        private static string ConnectionString =>
    "DefaultEndpointsProtocol=https;AccountName202205jobmanager;AccountKey=;EndppointSuffix=core.windows.net";
        private static string Container => "data";
        private static string FileName => "Jobs.json";
        private readonly BlobServiceClient service = new BlobServiceClient(ConnectionString);

        public async Task WriteFile(List<Job> jobs)
        {
            var jsonstring = JsonConvert.SerializeObject(jobs);
            
            var stream = new MemoryStream();
            
            var writer = new StreamWriter(stream);
            
            writer.Write(jsonstring);
            
            writer.Flush();
            
            stream.Position = 0;

            BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(FileName);
            await blob.UploadAsync(stream);
        }

        public async Task<List<Job>> ReadFile()
        {
            BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(FileName);
            if (blob.Exists())
            {
                var stream = new MemoryStream();
                await blob.DownloadToAsync(stream);

                stream.Position = 0;

                var jsonString = new StreamReader(stream).ReadToEnd();

                var jobs = JsonConvert.DeserializeObject<List<Job>>(jsonString);
                return jobs;
            }
            else
            {
                var defaultJobs = GetDefaultJobs();

                await WriteFile(defaultJobs);

                return defaultJobs;
            }

        }
        private List<Job> GetDefaultJobs()
        {
            var jobs = new List<Job>()
           {
            new Job{Id = 1, Name = "Job A Local Json File", Description = "This is job a."},
            new Job{Id = 2, Name = "Job B Local Json File", Description = "This is job b."},
            new Job{Id = 3, Name = "Job C Local Json File", Description = "This is job c."},
            new Job{Id = 4, Name = "Job D Local Json File", Description = "This is job d."}
           };
            return jobs;
        }

        public async Task AddJob(Job job)
        {
            var jobs = await ReadFile();
            jobs.Add(job);
            await WriteFile(jobs);
        }

       

        public async Task<Job> GetJob(int jobId)
        {
            var jobs = await ReadFile();
            var job = jobs.Find(p => p.Id == jobId);
            return job;
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {

            var jobs = await ReadFile();

            return jobs;
        }

        public async Task UpdateJob(Job job)
        {
            var jobs = await ReadFile();
            jobs[jobs.FindIndex(p => p.Id == job.Id)] = job;
            await WriteFile(jobs);
        }

        public async Task DeleteJob(Job job)
        {
            var jobs =await ReadFile();
            var remove = jobs.Find(p => p.Id == job.Id);
            jobs.Remove(remove);
        }

    }
}
