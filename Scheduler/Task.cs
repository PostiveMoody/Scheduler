using Newtonsoft.Json;

namespace Scheduler
{
    public class Task
    {
        [JsonProperty("id")]
        public long Id { get; private set; }
        [JsonProperty("name")]
        public string Name { get; private set; }
        [JsonProperty("description")]
        public string Description { get; private set; }
        [JsonProperty("deadline")]
        public DateTime Deadline { get; private set; }
        [JsonProperty("priority")]
        public Priority Priority { get; private set; }
        [JsonProperty("status")]
        public Status Status { get; private set; }

        public static Task Create(long id,
            string name,
            string description,
            DateTime deadline,
            Priority priority = Priority.Low,
            Status status = Status.MustBeDone)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if(deadline == default)
                throw new ArgumentNullException(nameof(deadline));

            return new Task()
            {
                Id = id,
                Name = name,
                Description = description,
                Deadline = deadline,
                Priority = priority,
                Status = status
            };
        }

        public void Update(string name,
           string description,
           DateTime deadline,
           Priority priority = Priority.Low,
           Status status = Status.MustBeDone)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (deadline == default)
                throw new ArgumentNullException(nameof(deadline));

            Name = name;
            Description = description;
            Deadline = deadline;
            Priority = priority;
            Status = status;
        }
    }
}