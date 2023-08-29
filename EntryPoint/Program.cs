using Instrumentation;
using Newtonsoft.Json;
using Scheduler;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    public class Program
    {
        private static ILogger _logger;
        
        public static void Main(string[] args)
        {
            _logger = new FileLogger(Constants.PathToLogFile);
            ShowMenu();
            do
            {
                Console.WriteLine(Constants.EnterMenuNumber);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && Int32.TryParse(input, out int inputNumber))
                {
                    ChooseAction(inputNumber);
                }

                Console.WriteLine(Constants.ExitButton);
                Console.WriteLine(Constants.AnyOtherKey);
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        public static void ShowMenu()
        {
            Console.WriteLine(Constants.ChooseOneOfPointsMenu);
            StringBuilder sb = new StringBuilder();
            sb.Append(Constants.ShowAllTasksWithFilter);
            sb.Append(Environment.NewLine);
            sb.Append(Constants.ChangeTaskDeadlineById);
            sb.Append(Environment.NewLine);
            sb.Append(Constants.ChangeTaskStatusById);
            sb.Append(Environment.NewLine);
            sb.Append(Constants.AddNewTask);
            sb.Append(Environment.NewLine);
            sb.Append(Constants.DeleteTaskById);
            sb.Append(Environment.NewLine);

            Console.WriteLine(sb);
        }
        public static void ChooseAction(int numberAction)
        {
            string jsonText = String.Empty;
            List<Scheduler.Task> Tasks = new List<Scheduler.Task>();
            if (File.Exists(Constants.Path))
                jsonText = File.ReadAllText(Constants.Path);
            Tasks = GetTasks(jsonText);

            switch (numberAction)
            {
                case (int)Actions.ShowAllTasks:
                    _logger
                    .SetMessage(nameof(Actions.ShowAllTasks))
                        .SetMonitoringItems((nameof(Tasks), JsonConvert.SerializeObject(Tasks, Formatting.Indented)))
                        .Handle(() =>
                        {
                            return ShowAllTasks(Tasks); 
                        });
                    break;
                case (int)Actions.ChangeTaskDeadlineById:
                    _logger
                        .SetMessage(nameof(Actions.ChangeTaskDeadlineById))
                        .SetMonitoringItems((nameof(Tasks), JsonConvert.SerializeObject(Tasks, Formatting.Indented)))
                        .Handle(() =>
                        {
                            return ChangeTaskDeadlineById(Tasks);
                        });
                    break;
                case (int)Actions.ChangeTaskStatusById:
                    _logger
                       .SetMessage(nameof(Actions.ChangeTaskStatusById))
                       .SetMonitoringItems((nameof(Tasks), JsonConvert.SerializeObject(Tasks, Formatting.Indented)))
                       .Handle(() =>
                       {
                           return ChangeTaskStatusById(Tasks); 
                       });
                    break;

                case (int)Actions.AddNewTask:
                    _logger
                      .SetMessage(nameof(Actions.AddNewTask))
                      .SetMonitoringItems((nameof(Tasks), JsonConvert.SerializeObject(Tasks, Formatting.Indented)))
                      .Handle(() =>
                      {
                          return AddNewTask(Tasks); 
                      });
                    break;

                case (int)Actions.DeleteTaskById:
                    _logger
                     .SetMessage(nameof(Actions.DeleteTaskById))
                     .SetMonitoringItems((nameof(Tasks), JsonConvert.SerializeObject(Tasks, Formatting.Indented)))
                     .Handle(() =>
                     {
                         return DeleteTaskById(Tasks); ;
                     });
                    break;
                default:
                    Console.WriteLine(Constants.PleaseEnterCorrectNumber);
                    break;
            }
        }

        public static List<Scheduler.Task> GetTasks(string jsonText) => JsonConvert.DeserializeObject<List<Scheduler.Task>>(jsonText);

        public static Scheduler.Task CreateTask(long id)
        {
            Console.WriteLine(Constants.TaskProperty.EnterName);
            var name = Console.ReadLine();

            Console.WriteLine(Constants.TaskProperty.EnterDescription);
            var description = Console.ReadLine();

            Console.WriteLine(Constants.TaskProperty.EnterDeadLine);
            var deadline = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);

            Console.WriteLine(Constants.TaskProperty.EnterPriority);
            var priority = Priority.Low;
            string inputPriority = Console.ReadLine();
            if (inputPriority.ToLower() == Constants.PriorityOptions.Medium.ToLower())
            {
                priority = Priority.Medium;
            }
            else if(inputPriority.ToLower() == Constants.PriorityOptions.High.ToLower())
            {
                priority = Priority.High;
            }

            Console.WriteLine(Constants.TaskProperty.EnterStatus);
            var status = Status.MustBeDone;
            string inputStatus = Console.ReadLine();
            if (inputStatus.ToLower() == Constants.StatusOptions.InProcess.ToLower())
            {
                status = Status.InProcess;
            }
            else if (inputStatus.ToLower() == Constants.StatusOptions.AlreadyDone.ToLower())
            {
                status = Status.AlreadyDone;
            }


            return Scheduler.Task.Create(id, name, description, deadline, priority, status);
        }

        public static int ChangeTaskDeadlineById(List<Scheduler.Task> Tasks)
        {
            Console.WriteLine(Constants.TaskProperty.EnterTaskId);
            var inputTaskId = Console.ReadLine();
            if (Int32.TryParse(inputTaskId, out int taskId))
            {
                var task = Tasks.Where(it => it.Id == taskId).FirstOrDefault();
                Console.WriteLine(Constants.TaskProperty.FoundedTaskById);

                var outPutTaskFoundedById = JsonConvert.SerializeObject(task, Formatting.Indented);
                Console.WriteLine(outPutTaskFoundedById);

                Console.WriteLine(Constants.TaskProperty.EnterChangedDeadline);
                var deadline = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm:ss",
                                      System.Globalization.CultureInfo.InvariantCulture);
                
                task.Update(task.Name,task.Description, deadline,task.Priority,task.Status);

                outPutTaskFoundedById = JsonConvert.SerializeObject(task, Formatting.Indented);
                Console.WriteLine(outPutTaskFoundedById);
            }
            return 0;
        }

        public static List<Scheduler.Task> ChangeTaskStatusById(List<Scheduler.Task> Tasks)
        {
            Console.WriteLine(Constants.TaskProperty.EnterTaskId);
            var inputTaskId = Console.ReadLine();
            if (Int32.TryParse(inputTaskId, out int taskId))
            {
                var task = Tasks.Where(it => it.Id == taskId).FirstOrDefault();
                Console.WriteLine(Constants.TaskProperty.FoundedTaskById);

                var outPutTaskFoundedById = JsonConvert.SerializeObject(task, Formatting.Indented);
                Console.WriteLine(outPutTaskFoundedById);

                Console.WriteLine(Constants.TaskProperty.EnterChangedStatus);
                var status = Status.MustBeDone;
                string inputStatus = Console.ReadLine();
                if (inputStatus.ToLower() == Constants.StatusOptions.InProcess.ToLower())
                {
                    status = Status.InProcess;
                }
                else if (inputStatus.ToLower() == Constants.StatusOptions.AlreadyDone.ToLower())
                {
                    status = Status.AlreadyDone;
                }

                task.Update(task.Name, task.Description, task.Deadline, task.Priority, status);

                outPutTaskFoundedById = JsonConvert.SerializeObject(task, Formatting.Indented);
                Console.WriteLine(outPutTaskFoundedById);
            }
            return Tasks;
        }

        public static List<Scheduler.Task> AddNewTask(List<Scheduler.Task> Tasks)
        {
            var actualId = Tasks.Select(it => it.Id).Max() + 1;

            var task = CreateTask(actualId);
            Tasks.Add(task);
            var taskText = JsonConvert.SerializeObject(Tasks, Formatting.Indented);
            File.WriteAllText(Constants.Path, taskText);

            return Tasks;
        }

        public static List<Scheduler.Task> DeleteTaskById(List<Scheduler.Task> Tasks)
        {
            Console.WriteLine(Constants.TaskProperty.EnterTaskId);
            var inputTaskId = Console.ReadLine();
            if (Int32.TryParse(inputTaskId, out int taskId))
            {
                var task = Tasks.Where(it => it.Id == taskId).FirstOrDefault();
                Tasks.Remove(task);
            }
            var taskText = JsonConvert.SerializeObject(Tasks, Formatting.Indented);
            File.WriteAllText(Constants.Path, taskText);

            return Tasks;
        }

        public static List<Scheduler.Task> ShowAllTasks(List<Scheduler.Task> Tasks)
        {
            Console.WriteLine(Constants.TaskProperty.EnterFilterByPriority);
            var inputFilterByPriority = Console.ReadLine();
            var filtersByPriority = inputFilterByPriority.Split(',');
            List<Priority> filterPriorities = new List<Priority>();
            foreach (var filter in filtersByPriority)
            {
                if (filter.ToLower() == Constants.PriorityOptions.Low.ToLower())
                    filterPriorities.Add(Priority.Low);
                if (filter.ToLower() == Constants.PriorityOptions.Medium.ToLower())
                    filterPriorities.Add(Priority.Medium);
                if (filter.ToLower() == Constants.PriorityOptions.High.ToLower())
                    filterPriorities.Add(Priority.High);
            }

            Console.WriteLine(Constants.TaskProperty.EnterFilterByStatus);
            var inputFilterByStatus = Console.ReadLine();
            var filtersByStatus = inputFilterByStatus.Split(',');
            List<Status> filterStatuses = new List<Status>();
            foreach (var filter in filtersByStatus)
            {
                if (filter.ToLower() == Constants.StatusOptions.MustBeDone.ToLower())
                    filterStatuses.Add(Status.MustBeDone);
                if (filter.ToLower() == Constants.StatusOptions.InProcess.ToLower())
                    filterStatuses.Add(Status.InProcess);
                if (filter.ToLower() == Constants.StatusOptions.AlreadyDone.ToLower())
                    filterStatuses.Add(Status.AlreadyDone);
            }

            Console.WriteLine(Constants.TaskProperty.EnterSortingByPriority);
            var inputSortingByPriority = Console.ReadLine();

            Console.WriteLine(Constants.TaskProperty.EnterSortingByStatus);
            var inputSortingByStatus = Console.ReadLine();

            Console.WriteLine(Constants.TaskProperty.EnterSortingByDeadline);
            var inputSortingByDeadline = Console.ReadLine();

            if (filterPriorities.Count > 0 || filterStatuses.Count > 0)
                Tasks = Tasks.Where(it => filterPriorities.Contains(it.Priority) || filterStatuses.Contains(it.Status)).ToList();

            if (inputSortingByPriority.ToLower() == Constants.SortingOptions.Desc.ToLower())
            {
                Tasks = Tasks.OrderByDescending(it => it.Priority).ToList();
            }
            else if (inputSortingByPriority.ToLower() == Constants.SortingOptions.Asc.ToLower())
            {
                Tasks = Tasks.OrderBy(it => it.Priority).ToList();
            }

            if (inputSortingByStatus.ToLower() == Constants.SortingOptions.Desc.ToLower())
            {
                Tasks = Tasks.OrderByDescending(it => it.Status).ToList();
            }
            else if (inputSortingByStatus.ToLower() == Constants.SortingOptions.Asc.ToLower())
            {
                Tasks = Tasks.OrderBy(it => it.Status).ToList();
            }

            if (inputSortingByDeadline.ToLower() == Constants.SortingOptions.Desc.ToLower())
            {
                Tasks = Tasks.OrderByDescending(it => it.Deadline).ToList();
            }
            else if (inputSortingByDeadline.ToLower() == Constants.SortingOptions.Asc.ToLower())
            {
                Tasks = Tasks.OrderBy(it => it.Deadline).ToList();
            }

            var taskTextWithFilterAndSorting = JsonConvert.SerializeObject(Tasks, Formatting.Indented);

            Console.WriteLine(taskTextWithFilterAndSorting);

            return Tasks;
        }
    }
}
