using JobManager.Models;
using JobManager.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JobManager.ViewModels
{
    public class JobManagerBase : BaseViewModel
    {
        public IJobDataStore<Job> JobDataStore => DependencyService.Get<IJobDataStore<Job>>();
    }
}
