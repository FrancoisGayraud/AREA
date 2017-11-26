namespace Area_Net
{
    public class Action
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActionAPI { get; set; }
        public string TriggerAPI { get; set; }
        public string ActionData { get; set; }
        public string TriggerData { get; set; }
        public string UserId { get; set; }
        public string toString()
        {
            return (
                "Action = Id : " + Id +
                " UserID : " + UserId +
                " Name : " + Name + 
                " ActionAPI : " + ActionAPI + 
                " TriggerAPI : " + TriggerAPI + 
                " ActionData : " + ActionData + 
                " TriggerData : " + TriggerData
                );
        }
    }
}