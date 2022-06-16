using JobManager.Models;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JobManager.ViewModels
{
    [QueryProperty(nameof(JobId),nameof(JobId))]
    public class JobDetailViewModel : JobManagerBase
    {   

        public AsyncCommand PageAppearingCommand { get; }
        private int jobId;
        private string name;
        private string description;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        
       public int JobId
        {
            get => jobId;
            set => SetProperty(ref jobId, value);
        }
            
        

        public JobDetailViewModel()
        {
            PageAppearingCommand = new AsyncCommand(PageAppearing);
        }

        async Task PageAppearing()
        {
            LoadJob(JobId);
        }
        
        public async Task LoadJob(int jobId)
        {
            Job job = await JobDataStore.GetJob(jobId);

            JobId = job.Id;
            Name = job.Name;
            Description = job.Description;
        }
    }
}
