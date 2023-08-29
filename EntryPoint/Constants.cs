namespace EntryPoint
{
    public class Constants
    {
        public const string ShowAllTasksWithFilter = "1. Просмотреть все задачи с возможностью фильтрации по приоритету и статусу, сортировки по дэдлайну и статусу;";
        public const string ChangeTaskDeadlineById = "2. Изменить дедлайн задачи по идентификатору задачи;";
        public const string ChangeTaskStatusById = "3. Изменить статус задачи по идентификатору задачи;";
        public const string AddNewTask = "4. Добавить новую задачу;";
        public const string DeleteTaskById = "5. Удалить задачу по идентификатору.";
        public const string PleaseEnterCorrectNumber = "Вы ввели некорректный номер меню. Пожалуйста введите корректный номер в меню!";
        public const string ExitButton = "Нажмите escape(esc), если вы хотите завершить выполнение приложения,";
        public const string AnyOtherKey = "Для продолжения выполения программы нажмите любую другую клавишу:";
        public const string EnterMenuNumber = "Введите пункт меню:";
        public const string ChooseOneOfPointsMenu = "Выберите один из пунктов меню для выбора действия:";

        public const string Path = "C:\\repos\\Scheduler\\Scheduler\\Tasks.json";
        public const string PathToLogFile = @"C:\repos\Scheduler\EntryPoint\LogInformation.txt";
       
        public class TaskProperty
        {
            public const string EnterName = "Введите имя задачи, которую хотите добавить:";
            public const string EnterDescription = "Введите описание задачи, которую хотите добавить:";
            public const string EnterDeadLine = "Введите крайний срок задачи, в таком формате \"2009-05-08 14:40:52\"";
            public const string EnterPriority = "Введите приоритет задачи: Low, Medium, High:";
            public const string EnterStatus = "Введите статус задачи: MustBeDone, InProcess, AlreadyDone:";
            public const string EnterTaskId = "Введите идентификатор задачи:";
            public const string FoundedTaskById = "Найденая задача по его идентифактору:";
            public const string EnterChangedDeadline = "Введите крайний срок на который хотите поменять, в таком формате \"2009-05-08 14:40:52\":";
            public const string EnterChangedStatus = "Введите новый статус задачи на который хотите поменять MustBeDone,InProcess,AlreadyDone:";
            public const string EnterFilterByPriority = "Введите фильтр приоритета: Low,Medium,High. Если он вам нужен, иначе нажмите Enter.";
            public const string EnterFilterByStatus = "Введите фильтр статуса: MustBeDone,InProcess,AlreadyDone. Если он вам нужен, иначе нажмите Enter.";
            public const string EnterSortingByPriority = "Введите параметр сортировки приоритета: ASC|DESC. Если он вам нужен, иначе нажмите Enter.";
            public const string EnterSortingByStatus = "Введите параметр сортировки статуса: ASC|DESC. Если он вам нужен, иначе нажмите Enter.";
            public const string EnterSortingByDeadline = "Введите параметр сортировки крайнего срока:ASC|DESC. Если он вам нужен, иначе нажмите Enter.";

        }

        public class PriorityOptions
        {
            public const string Low = "Low";
            public const string Medium = "Medium";
            public const string High = "High";
        }

        public class StatusOptions
        {
            public const string MustBeDone = "MustBeDone";
            public const string InProcess = "InProcess";
            public const string AlreadyDone = "AlreadyDone";
        }

        public class SortingOptions
        {
            public const string Desc = "Desc";
            public const string Asc = "Asc";
        }
    }
}
