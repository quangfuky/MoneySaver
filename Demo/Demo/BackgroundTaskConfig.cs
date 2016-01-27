using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Storage;

namespace Demo
{
    class BackgroundTaskConfig
    {
        public const string SampleBackgroundTaskEntryPoint = "BackgroundToast.Task";
        public const string SampleBackgroundTaskName = "SampleBackgroundTask";
        public static string SampleBackgroundTaskProgress = "";
        public static bool SampleBackgroundTaskRegistered = false;

        public const string TimeTriggeredTaskName = "TimeTriggeredTask";
        public static string TimeTriggeredTaskProgress = "";
        public static bool TimeTriggeredTaskRegistered = false;

        public const string ApplicationTriggerTaskName = "ApplicationTriggerTask";
        public static string ApplicationTriggerTaskProgress = "";
        public static string ApplicationTriggerTaskResult = "";
        public static bool ApplicationTriggerTaskRegistered = false;

        public static async Task<BackgroundTaskRegistration> RegisterBackgroundTask(String taskEntryPoint, String name, IBackgroundTrigger trigger, IBackgroundCondition condition)
        {

            //foreach (var cur in BackgroundTaskRegistration.AllTasks)
            //{
            //    if (cur.Value.Name == name)
            //    {
            //        cur.Value.Unregister(true);

            //    }
            //}
            //await BackgroundExecutionManager.RequestAccessAsync();

            var builder = new BackgroundTaskBuilder();

            builder.Name = name;
            builder.TaskEntryPoint = taskEntryPoint;
            builder.SetTrigger(trigger);

            if (condition != null)
            {
                builder.AddCondition(condition);

                //
                // If the condition changes while the background task is executing then it will
                // be canceled.
                //
                builder.CancelOnConditionLoss = true;
            }
            
            var task = builder.Register();
            


            //
            // Remove previous completion status from local settings.
            //
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values.Remove(name);
            return task;

        }

        public static bool TaskRequiresBackgroundAccess(String name)
        {
            if ((name == TimeTriggeredTaskName) ||
                (name == ApplicationTriggerTaskName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
